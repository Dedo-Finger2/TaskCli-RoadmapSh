using TaskCli.entities;
using TaskCli.exceptions;

namespace TaskCli.repositories
{
    internal class InMemoryTaskRepositoryImpl : ITaskRepository
    {
        private List<TaskEntity> database = [];

        private int GetLastInsertedId()
        {
            try
            {
                TaskEntity lastTask = database.Last();

                return lastTask.id;
            } catch (InvalidOperationException e)
            {
                return 0;
            }
        }

        public int Create(string description)
        {
            int id = GetLastInsertedId() + 1;

            TaskEntity task = new(id, description);

            database.Insert(id - 1, task);

            return task.id;
        }

        public void DeleteById(int id)
        {
            try
            {
                TaskEntity task = database[id - 1];

                database.Remove(task);

                return;
            } catch (ArgumentOutOfRangeException e)
            {
                throw new TaskNotFoundException($"task with id '{id}' was not found");
            }
        }

        public List<TaskEntity> FindAll()
        {
            return database;
        }

        public List<TaskEntity> FindAllByStatus(entities.TaskStatus status)
        {
            return database.Where(task => task.status == status).ToList();
        }

        public int UpdateDescriptionById(int id, string newDescription)
        {
            try
            {
                TaskEntity task = database[id - 1];

                task.description = newDescription;

                database.Append(task);

                return id;
            }
            catch (ArgumentOutOfRangeException e)
            {
                throw new TaskNotFoundException($"task with id '{id}' was not found");
            }
        }

        public int UpdateStatusById(int id, entities.TaskStatus status)
        {
            try
            {
                TaskEntity task = database[id - 1];

                task.status = status;

                database.Append(task);

                return id;
            } catch (ArgumentOutOfRangeException e)
            {
                throw new TaskNotFoundException($"task with id '{id}' was not found");
            }
        }
    }
}
