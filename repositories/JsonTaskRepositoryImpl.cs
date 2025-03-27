using System.Text.RegularExpressions;
using TaskCli.entities;
using TaskCli.exceptions;

namespace TaskCli.repositories
{
    internal class JsonTaskRepositoryImpl : ITaskRepository
    {
        public JsonTaskRepositoryImpl()
        {
            if (!File.Exists("./database.json"))
            {
                CreateDatabase();
            }
        }

        private void CreateDatabase()
        {
            File.WriteAllText("./database.json", "{ \n\t\"tasks\": [] \n}");
        }

        private IEnumerable<string> GetDatabaseContentUsingLines()
        {
            return File.ReadLines("./database.json");
        }


        private int GetLastInsertedIdUsingLines()
        {
            IEnumerable<string> lines = GetDatabaseContentUsingLines();
            List<string> ids = [];

            foreach (string line in lines)
            {
                if (line.Contains("\"id\": "))
                {
                    string id = line.Split(":")[1].Trim();
                    string idWithoutComma = id.EndsWith(",") ? id.Remove(id.Length - 1) : id;

                    ids.Add(idWithoutComma);
                }
            }

            return int.Parse(ids.Last());
        }

        private string GetDatabaseContentString()
        {
            return File.ReadAllText("./database.json");
        }

        private int GetLastInsertedIdUsingString()
        {
            string content = GetDatabaseContentString();

            string[] contentIdSplitted = content.Split("\"id\":");

            List<string> ids = [];

            foreach (string line in contentIdSplitted)
            {
                if (line.StartsWith("{")) continue;

                string id = line.Trim().Split(" ")[0];
                string idWithoutComma = id.EndsWith(",") ? id[0].ToString() : id;

                ids.Add(idWithoutComma);
            }

            return int.Parse(ids[^1]);
        }

        public int Create(string description)
        {
            int lastInsertedId = GetLastInsertedIdUsingString();
            int newId = lastInsertedId + 1;

            string content = GetDatabaseContentString();

            TaskEntity task = new(newId, description);

            string taskJsonObject = $" \"id\": {newId}, \"description\": \"{description}\", \"status\": \"{task.status}\", \"createdAt\": \"{task.createdAt}\", \"updatedAt\": \"{task.updatedAt}\"";

            string newJsonContent = content.Split("] }")[0];

            newJsonContent += ",";
            newJsonContent += " {";
            newJsonContent += taskJsonObject;
            newJsonContent += " }";
            newJsonContent += "] }";

            File.WriteAllText("./database.json", newJsonContent);

            return newId;
        }

        public void DeleteById(int id)
        {
            string content = GetDatabaseContentString();

            string taskFound = Regex.Match(content, @"\{[^{}]*""id""\s*:\s*1[^{}]*\}".Replace("1", id.ToString())).ToString();

            if (taskFound == string.Empty) throw new TaskNotFoundException($"task with id '{id}' was not found");

            string contentWithoutTaskFound = content.Replace(taskFound, string.Empty);
            string contentWithoutBrokenCommas = contentWithoutTaskFound.Replace("[, ", "[").Replace("}, , ", "}, ");

            File.WriteAllText("./database.json", contentWithoutBrokenCommas);

            return;
        }

        public List<TaskEntity> FindAll()
        {
            List<TaskEntity> tasks = [];

            string content = GetDatabaseContentString();

            var jsonObjects = Regex.Matches(content, @"\{[^{}]+\}");

            foreach (var obj in jsonObjects)
            {
                var id = Regex.Match(obj.ToString(), @"id""\s*:\s*(\d+)");
                var description = Regex.Match(obj.ToString(), @"""description""\s*:\s*""([^""]*)""");
                var status = Regex.Match(obj.ToString(), @"""status""\s*:\s*""([^""]*)""");
                var createdAt = Regex.Match(obj.ToString(), @"""createdAt""\s*:\s*""([^""]*)""");
                var updatedAt = Regex.Match(obj.ToString(), @"""updatedAt""\s*:\s*""([^""]*)""");

                int idValue = int.Parse(id.ToString().Split(":")[1].Trim());
                string descriptionValue = description.ToString().Split(":")[1].Trim().Replace('"'.ToString(), string.Empty);
                string statusValue = status.ToString().Split(":")[1].Trim().Replace('"'.ToString(), string.Empty);
                entities.TaskStatus taskStatus = statusValue == "Todo" ? entities.TaskStatus.Todo : statusValue == "InProgress" ? entities.TaskStatus.InProgress : entities.TaskStatus.Done;
                string createdAtValue = createdAt.ToString().Split("\":")[1].Trim().Replace('"'.ToString(), string.Empty);
                string updatedAtValue = updatedAt.ToString().Split("\":")[1].Trim().Replace('"'.ToString(), string.Empty);

                TaskEntity task = new(idValue, descriptionValue, taskStatus, DateTime.Parse(createdAtValue), updatedAtValue.Length > 0 ? DateTime.Parse(updatedAtValue) : null);

                tasks.Add(task);
            }

            return tasks;
        }

        public List<TaskEntity> FindAllByStatus(entities.TaskStatus status)
        {
            List<TaskEntity> tasks = [];

            string content = GetDatabaseContentString();

            var jsonObjects = Regex.Matches(content, @"\{[^{}]+\}");

            foreach (var obj in jsonObjects)
            {
                var statusString = Regex.Match(obj.ToString(), @"""status""\s*:\s*""([^""]*)""");
                string statusValue = statusString.ToString().Split(":")[1].Trim().Replace('"'.ToString(), string.Empty);
                
                entities.TaskStatus taskStatus = statusValue == "Todo" ? entities.TaskStatus.Todo : statusValue == "InProgress" ? entities.TaskStatus.InProgress : entities.TaskStatus.Done;

                if (taskStatus == status)
                {
                    var id = Regex.Match(obj.ToString(), @"id""\s*:\s*(\d+)");
                    var description = Regex.Match(obj.ToString(), @"""description""\s*:\s*""([^""]*)""");
                    var createdAt = Regex.Match(obj.ToString(), @"""createdAt""\s*:\s*""([^""]*)""");
                    var updatedAt = Regex.Match(obj.ToString(), @"""updatedAt""\s*:\s*""([^""]*)""");

                    int idValue = int.Parse(id.ToString().Split(":")[1].Trim());
                    string descriptionValue = description.ToString().Split(":")[1].Trim().Replace('"'.ToString(), string.Empty);
                    string createdAtValue = createdAt.ToString().Split("\":")[1].Trim().Replace('"'.ToString(), string.Empty);
                    string updatedAtValue = updatedAt.ToString().Split("\":")[1].Trim().Replace('"'.ToString(), string.Empty);

                    TaskEntity task = new(idValue, descriptionValue, taskStatus, DateTime.Parse(createdAtValue), updatedAtValue.Length > 0 ? DateTime.Parse(updatedAtValue) : null);

                    tasks.Add(task);
                }
            }

            return tasks;
        }

        public int UpdateDescriptionById(int id, string newDescription)
        {
            string content = GetDatabaseContentString();

            string taskFound = Regex.Match(content, @"\{[^{}]*""id""\s*:\s*1[^{}]*\}".Replace("1", id.ToString())).ToString();

            if (taskFound == string.Empty) throw new TaskNotFoundException($"task with id '{id}' was not found");

            string currentTaskDescription = '"' + Regex.Match(taskFound, @"description""\s*:\s*""([^""]+)").ToString() + '"';

            string updatedTask = taskFound.Replace(currentTaskDescription, $"\"description\": \"{newDescription}\"");

            string newContent = content.Replace(taskFound, updatedTask);

            File.WriteAllText("./database.json", newContent);

            return id;
        }

        public int UpdateStatusById(int id, entities.TaskStatus status)
        {
            string content = GetDatabaseContentString();

            string taskFound = Regex.Match(content, @"\{[^{}]*""id""\s*:\s*1[^{}]*\}".Replace("1", id.ToString())).ToString();

            if (taskFound == string.Empty) throw new TaskNotFoundException($"task with id '{id}' was not found");

            string currentTaskStatus = '"' + Regex.Match(taskFound, @"status""\s*:\s*""([^""]+)").ToString() + '"';

            string updatedTask = taskFound.Replace(currentTaskStatus, $"\"status\": \"{status}\"");

            string newContent = content.Replace(taskFound, updatedTask);

            File.WriteAllText("./database.json", newContent);

            return id;
        }
    }
}
