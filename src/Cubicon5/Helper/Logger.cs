using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using GTA;
using GTA.Native;
using GTA.Math;
using GTA.NaturalMotion;

namespace Cubicon5
{
    static public class Logger
    {
        public static void LogToFile(string message, [CallerMemberName] string CallingClassName = "")
        {
            string FileName = "scripts\\MyModLogFile.log";
            try
            {
                if (!File.Exists(FileName))
                {
                    File.Create(FileName);
                }

                File.AppendAllText(FileName, $"{GetDateTime()}{CallingClassName} | {message}{Environment.NewLine}");
            }
            catch (Exception exc)
            {
                LogToUiNotifyWithoutMemberName(exc.Message);
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
