using System;
using System.IO;
using System.Runtime.CompilerServices;
using GTA;

namespace Cubicon5.Helper
{
    static public class Logger
    {

        private static readonly string FileName = $"scripts\\{Globals.PluginName}.log";

        public static void LogToFile(string PluginName, Exception exception, [CallerMemberName] string CallingClassName = "")
        {
            try
            {
                var message = PluginName + Environment.NewLine + exception.Message + Environment.NewLine + exception.StackTrace + Environment.NewLine + exception.Source;

                File.AppendAllText(FileName, $"{GetDateTime()}{CallingClassName} | {message}{Environment.NewLine}");
                UI.Notify($"Wrote Error to Log: {FileName}", true);
            }
            catch (Exception exc)
            {
                LogToUiNotify($"Error writing log: {exc.Message}{Environment.NewLine}{exc.StackTrace}");
            }

        }

        public static void LogToFile(string message, [CallerMemberName] string CallingClassName = "")
        {
            try
            {
                File.AppendAllText(FileName, $"{GetDateTime()}{CallingClassName} | {message}{Environment.NewLine}");
                UI.Notify($"Wrote Error to Log: {FileName}", true);
            }
            catch (Exception exc)
            {
                LogToUiNotify($"Error writing log: {exc.Message}{Environment.NewLine}{exc.StackTrace}");
            }

        }

        public static void LogToUiNotify(string message, [CallerMemberName] string CallingClassName = "")
        {
            UI.Notify($"{CallingClassName} | {message}");
        }

        public static void LogToUiNotifyWithoutMemberName(string message)
        {
            UI.Notify($"{message}");
        }

        public static string GetDateTime()
        {
            return DateTime.Now.ToString() + " | ";
        }
    }
}
