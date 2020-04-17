using NativeUI;

namespace Cubicon5.Menus
{
    public static class TempomatMenuMenuItem
    {
        public static UIMenuItem TempomatEnableMenuItem()
        {
            var tempomatEnableMenuItem = new UIMenuCheckboxItem("Enabled", Helper.Globals.Settings.TempomatEnabled);
            tempomatEnableMenuItem.CheckboxEvent += (sender, Checked) =>
            {
                Helper.Globals.Settings.TempomatEnabled = Checked;
            };
            return tempomatEnableMenuItem;

        }

        public static UIMenuItem TempomatIgnoreVehicleInAirMenuItem() 
        { 

            var tempomatIgnoreVehicleInAirMenuItem = new UIMenuCheckboxItem("Ignore Vehicle in Air", Helper.Globals.Settings.TempomatIgnoreVehicleInAir);
            tempomatIgnoreVehicleInAirMenuItem.CheckboxEvent += (sender, Checked) =>
            {
                Helper.Globals.Settings.TempomatIgnoreVehicleInAir = Checked;
            };
            return tempomatIgnoreVehicleInAirMenuItem;
        }
    }
}
