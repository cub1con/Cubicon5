using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cubicon5.Settings;
using GTA;
using NativeUI;

namespace Cubicon5.Menus
{
    public static class RecreateSettingsMenuItem
    {
        public static void Add_Button_RecreateSettings(NativeUI.UIMenu Menu)
        {
            UIMenuItem newMenuItem = new UIMenuItem("Reset Settings", "Recreate the setting file (good after Updates)");
            newMenuItem.Activated += Button_RecreateSettings_Activated;
            Menu.AddItem(newMenuItem);
        }

        private static void Button_RecreateSettings_Activated(UIMenu sender, UIMenuItem selectedItem)
        {
            UI.Notify("Recreating Settings");
            MenuSettings.CreateSettings();
            UI.Notify("Loading Settings");
            MenuSettings.LoadSettings();
        }
    }
}
