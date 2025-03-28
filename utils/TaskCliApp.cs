using TaskCli.entities;
using TaskCli.exceptions;
using TaskCli.repositories;

namespace TaskCli.utils
{
    internal class TaskCliApp(ITaskRepository repository)
    {
        private readonly ITaskRepository repository = repository;

        public void Help()
        {
            string[] commands = { "add [description]", "update [id] [newDescription]", "mark-in-progress [id]", "mark-done [id]", "list", "list done", "list todo", "list in-progress", "delete [id]" };
            string[] commandsDetails = { "Adds a new task", "Updates a task description using it's id", "Marks a task as in-progress using it's id", "Marks a task as done using it's id", "List all tasks", "List all tasks marked as done", "List all tasks marked as todo", "List all tasks marked as in-progress", "Deletes a task using it's id" };

            Console.WriteLine("Task CLI (1.0)\n");

            Console.WriteLine("Usage: taskcli [command] [argument #1] [argument #2]\n");
            
            Console.WriteLine("commands:\n");
            for (int i = 0; i < commands.Length; i++)
            {
                Console.WriteLine($"{commands[i].PadRight(35, ' ')}\t{commandsDetails[i]}");
            }
        }

        public void Add(string[] args)
        {
            try
            {
                string description = args[1];

                int insertedId = repository.Create(description);

                Console.WriteLine($"Task added successfully (ID: {insertedId})");
            } catch (IndexOutOfRangeException e)
            {
                throw new EmptyRequiredFieldException("description");
            }
        }

        public void Update(string[] args)
        {
            try
            {
                string id = args[1];
                string description = args[2];

                int updatedId = repository.UpdateDescriptionById(int.Parse(id), description);

                Console.WriteLine($"Task updated successfully (ID: {updatedId})");
            } catch (IndexOutOfRangeException e)
            {
                throw new EmptyRequiredFieldException("id or description");
            }
        }

        public void Delete(string[] args)
        {
            try
            {
                string id = args[1];

                repository.DeleteById(int.Parse(id));

                Console.WriteLine($"Task deleted successfully (ID: {id})");
            }
            catch (IndexOutOfRangeException e)
            {
                throw new EmptyRequiredFieldException("id");
            }
        }

        public void MarkInProgress(string[] args)
        {
            try
            {
                string id = args[1];

                repository.UpdateStatusById(int.Parse(id), entities.TaskStatus.InProgress);

                Console.WriteLine($"Task status updated successfully (ID: {id})");
            }
            catch (IndexOutOfRangeException e)
            {
                throw new EmptyRequiredFieldException("id");
            }
        }

        public void MarkDone(string[] args)
        {
            try
            {
                string id = args[1];

                repository.UpdateStatusById(int.Parse(id), entities.TaskStatus.Done);

                Console.WriteLine($"Task status updated successfully (ID: {id})");
            }
            catch (IndexOutOfRangeException e)
            {
                throw new EmptyRequiredFieldException("id");
            }
        }
        
        public void List(string[] args)
        {
            try
            {
                string status = args[1];
                entities.TaskStatus taskStatus;

                switch (status)
                {
                    case "done": taskStatus = entities.TaskStatus.Done; break;
                    case "todo": taskStatus = entities.TaskStatus.Todo; break;
                    case "in-progress": taskStatus = entities.TaskStatus.InProgress; break;
                    default: taskStatus = entities.TaskStatus.Todo; break;
                }

                List<TaskEntity> tasks = repository.FindAllByStatus(taskStatus);

                foreach (TaskEntity task in tasks)
                {
                    Console.WriteLine($"ID: {task.id} - {task.description}");
                }
            } catch (IndexOutOfRangeException e)
            {
                Logger.Info(e, "user used list command without a 'status'");

                List<TaskEntity> tasks = repository.FindAll();

                foreach (TaskEntity task in tasks)
                {
                    Console.WriteLine($"ID: {task.id} [{task.status}] - {task.description}");
                }
            }
        }
    }
}
