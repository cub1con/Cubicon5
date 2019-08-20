using GTA;
using System;

namespace Cubicon5
{
   static class Globals
    {
        public static int GameInputMethod { get { return Convert.ToInt32(Game.CurrentInputMode); } }

        public static string PluginName = "Cubicon5";

        public static bool NativeUiIsAnyMenuOpen = false;
        public static string AssemblyVersion
        {
            get
            {
                return System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
        }
    }
}
