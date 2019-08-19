using GTA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cubicon5
{
   static class Globals
    {
        public static int GameInputMethod { get { return Convert.ToInt32(Game.CurrentInputMode); } }

        public static string PluginName = "Cubicon5";
        public static string AssemblyVersion
        {
            get
            {
                return System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
        }
    }
}
