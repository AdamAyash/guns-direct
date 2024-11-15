using Serilog;

namespace Common
{
    public static class Utilities
    {
        public static T ConvertObject<T>(object input)
            where T : class
        {
            return (T)Convert.ChangeType(input, typeof(T));
        }

        public static DateTime GetCurrentDateTime()
        {
            return DateTime.Now;
        }

        public static DateTime GetCurrentDate()
        {
            return DateTime.Now.Date;
        }

        public static string GetCurrentDateString()
        {
            return DateTime.Now.Date.ToString("dd-MM-yyyy");
        }

        public static void ShutDownApplication()
        {
            Log.Logger.Fatal(Messages.APPLICATION_SHUT_DOWN_WITH_ERROR_MESSAGE);
            Log.CloseAndFlush();
            System.Threading.Thread.Sleep(1000);
            Environment.Exit(0);
        }
    }
}
