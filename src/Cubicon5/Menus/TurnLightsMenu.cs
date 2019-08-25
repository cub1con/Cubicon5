using Cubicon5.Settings;
using NativeUI;
using GTA;

namespace Cubicon5.Menus
{
    public static class TurnLightsMenuItem
    {
        public static void Add_Option_TurnLights(NativeUI.UIMenu Menu)
        {
            UIMenuCheckboxItem newMenu = new UIMenuCheckboxItem("Turn Lights", MenuSettings.TurnLightsEnabled);
            newMenu.CheckboxEvent += Option_TurnLights_OnCheckboxChange;
            Menu.AddItem(newMenu);
        }

        private static void Option_TurnLights_OnCheckboxChange(UIMenuCheckboxItem sender, bool Checked)
        {
            MenuSettings.TurnLightsEnabled = Checked;
            if (Game.Player.Character.IsInVehicle())
            {
                GTA.Game.Player.Character.CurrentVehicle.LeftIndicatorLightOn = false;
                GTA.Game.Player.Character.CurrentVehicle.RightIndicatorLightOn = false;
            }
        }

    }
}
