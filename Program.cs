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
                JsonTaskRepositoryImpl repo = new();

                TaskCliApp app = new(repo);

                string command = args[0];

                switch(command)
                {
                    case "help": app.Help(); break;
                    case "add": app.Add(args); break;
                    case "update": app.Update(args); break;
                    case "delete": app.Delete(args); break;
                    case "mark-in-progress": app.MarkInProgress(args); break;
                    case "mark-done": app.MarkDone(args); break;
                    case "list": app.List(args); break;
                    default: app.Help();  break;
                }

            }
            catch (IndexOutOfRangeException e)
            {
                
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            } 
            
        }
    }
}