using Cubicon5.Settings;
using GTA;
using System;

namespace Cubicon5.Helper
{
    public static class Globals
    {
        public static int GameInputMethod { get { return Convert.ToInt32(Game.CurrentInputMode); } }

        public static string PluginName = "Cubicon5";

        public static bool NativeUiIsAnyMenuOpen = false;

        public static ISettings Settings;

        public static string AssemblyVersion
        {
            get
            {
                return System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
        }
    }
}
