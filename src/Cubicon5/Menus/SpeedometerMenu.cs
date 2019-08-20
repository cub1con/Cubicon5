using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cubicon5.Settings;
using NativeUI;

namespace Cubicon5.Menus
{
    public static class SpeedometerMenuItem
    {
        public static void Option_Speedometer(NativeUI.UIMenu Menu)
        {
            UIMenuCheckboxItem newMenu = new UIMenuCheckboxItem("Speedometer", MenuSettings.SpeedometerEnabled);
            newMenu.CheckboxEvent += Option_Speedometer_OnCheckboxChange;
            Menu.AddItem(newMenu);
        }

        private static void Option_Speedometer_OnCheckboxChange(UIMenuCheckboxItem sender, bool Checked)
        {
            MenuSettings.SpeedometerEnabled = Checked;
        }
    }
}
