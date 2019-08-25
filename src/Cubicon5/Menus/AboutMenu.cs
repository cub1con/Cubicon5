using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cubicon5.Settings;
using NativeUI;
using Cubicon5.Helper;

namespace Cubicon5.Menus
{
    public static class AboutMenuItem
    {
        public static void Add_Item_AboutMenu(NativeUI.UIMenu Menu)
        {
            UIMenuItem newMenu = new UIMenuItem("Info", "Mod Information");
            Menu.AddItem(newMenu);
            Menu.OnItemSelect += delegate (UIMenu sender, UIMenuItem item, int index)
            {
                bool flag = item == newMenu;
                if (flag)
                {
                    BigMessageThread.MessageInstance.ShowSimpleShard("Info", $"{Globals.PluginName}{Environment.NewLine}V. {Globals.AssemblyVersion}{Environment.NewLine}By Cubicon", 5000);
                }
            };
        }
    }
}
