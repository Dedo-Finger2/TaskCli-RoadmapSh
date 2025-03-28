namespace TaskCli.utils
{
    internal class Logger
    {

        private static readonly string logFilePath = "./taskcli-error-log.log";

        private static bool IsLogFileCreated()
        {
            return File.Exists(logFilePath);
        }

        private static void CreateLogFile()
        {
            File.Create(logFilePath);
        }

        private static void WriteInLogFile(string content)
        {
            File.AppendAllText(logFilePath, content);
        }

        public static void Info(Exception e, string context)
        {
            DateTime dateTime = DateTime.UtcNow;
            string log = $"-----[INFO]-----\n[DATETIME]:{dateTime}\n[CONTEXT]:{context}\n[STACK TRACE]:{e.StackTrace}\n[MESSAGE]:{e.Message}\n\n";

            if (!IsLogFileCreated()) CreateLogFile();

            WriteInLogFile(log);
        }

        public static void Error(Exception e, string context)
        {
            DateTime dateTime = DateTime.UtcNow;
            string log = $"-----[ERROR]-----\n[DATETIME]:{dateTime}\n[CONTEXT]:{context}\n[STACK TRACE]:{e.StackTrace}\n[MESSAGE]:{e.Message}\n\n";

            if (!IsLogFileCreated()) CreateLogFile();

            WriteInLogFile(log);

            Console.WriteLine($"something went wrong. check the log file at '{logFilePath}'");
        }
        
        public static void Warn(Exception e, string context)
        {
            DateTime dateTime = DateTime.UtcNow;
            string log = $"-----[WARN]-----\n[DATETIME]:{dateTime}\n[CONTEXT]:{context}\n[STACK TRACE]:{e.StackTrace}\n[MESSAGE]:{e.Message}\n\n";

            if (!IsLogFileCreated()) CreateLogFile();

            WriteInLogFile(log);

            Console.WriteLine($"something went wrong. check the log file at '{logFilePath}'");
        }
    }
}
