using Cubicon5.Settings;
using NativeUI;

namespace Cubicon5.Menus
{
    public static class SpeedometerMenuItem
    {
        public static void Add_Option_Speedometer(NativeUI.UIMenu Menu)
        {
            UIMenuCheckboxItem newMenu = new UIMenuCheckboxItem("Speedometer", Helper.Globals.Settings.SpeedometerEnabled);
            newMenu.CheckboxEvent += Option_Speedometer_OnCheckboxChange;
            Menu.AddItem(newMenu);
        }

        private static void Option_Speedometer_OnCheckboxChange(UIMenuCheckboxItem sender, bool Checked)
        {
            Helper.Globals.Settings.SpeedometerEnabled = Checked;
        }
    }
}
