﻿using Cubicon5.Settings;
using NativeUI;

namespace Cubicon5.Menus
{
    public static class HeadlightFlasherMenuItem
    {
        public static void Add_Option_HeadlightFlasher(NativeUI.UIMenu Menu)
        {
            UIMenuCheckboxItem newMenu = new UIMenuCheckboxItem("Headlight Flash on Horn", Helper.Globals.Settings.HeadlightFlasherEnabled);
            newMenu.CheckboxEvent += Option_HeadlightFlasher_OnCheckboxChange;
            Menu.AddItem(newMenu);
        }

        private static void Option_HeadlightFlasher_OnCheckboxChange(UIMenuCheckboxItem sender, bool Checked)
        {
            Helper.Globals.Settings.HeadlightFlasherEnabled = Checked;
        }
    }
}
