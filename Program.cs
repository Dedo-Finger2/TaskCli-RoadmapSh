using TaskCli.exceptions;
using TaskCli.repositories;
using TaskCli.utils;

namespace TaskCli
{
    internal class Program
    {
        static void Main(string[] args)
        {
            JsonTaskRepositoryImpl repo = new();

            TaskCliApp app = new(repo);

            try
            {
                if (args.Length < 1) throw new NoCommandFoundException("no command found");

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
            catch (EmptyRequiredFieldException e)
            {
                Console.WriteLine(e.Message);
                Logger.Warn(e, "it was expected a field but none was providen");
            }
            catch (NoCommandFoundException e)
            {
                Logger.Info(e, "user executed the cli without a command");
                app.Help();
            }
            catch (IndexOutOfRangeException e)
            {
                Logger.Error(e, "something went wrong");
            }
            catch (Exception e)
            {
                Logger.Error(e, "unkown exception happened");
            }             
        }
    }
}