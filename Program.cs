using TaskCli.entities;
using TaskCli.repositories;
using TaskCli.utils;

namespace TaskCli
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Hello World!");

                InMemoryTaskRepositoryImpl repo = new();

                _ = repo.Create("testing 1");
                _ = repo.Create("testing 2");
                _ = repo.Create("testing 3");
                _ = repo.Create("testing 4");
                _ = repo.Create("testing 5");
                _ = repo.Create("testing 6");

                repo.UpdateStatusById(2, entities.TaskStatus.Done);
                repo.UpdateStatusById(4, entities.TaskStatus.Done);
                repo.UpdateStatusById(5, entities.TaskStatus.InProgress);
                repo.UpdateStatusById(3, entities.TaskStatus.InProgress);

                TaskCliApp app = new(repo);

                string command = args[0];

                switch(command)
                {
                    case "add": app.Add(args); break;
                    case "update": app.Update(args); break;
                    case "delete": app.Delete(args); break;
                    case "mark-in-progress": app.MarkInProgress(args); break;
                    case "mark-done": app.MarkDone(args); break;
                    case "list": app.List(args); break;
                    default: Console.WriteLine("command not found");  break;
                }

            }
            catch (IndexOutOfRangeException e)
            {
                Console.WriteLine("expected a command");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            } 
            
        }
    }
}