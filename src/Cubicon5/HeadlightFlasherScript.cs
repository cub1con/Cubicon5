using GTA;
using System;
using Cubicon5.Helper;

namespace Cubicon5
{
    public class HeadlightFlasherScript : Script
    {
        bool VhLightsOn;
        bool VhHighBeamsOn;
        bool resetLights;
        private Ped Character => Game.Player.Character;

        private const string PluginName = "HeadlightFlasher";

        public HeadlightFlasherScript()
        {
            this.Tick += OnTick;
            this.Interval = 50;
        }

        private void OnTick(object sender, EventArgs e)
        {
            var PlayerIsNull = !PlayerHelper.PlayerIsNotNull();
            if (!Globals.Settings.HeadlightFlasherEnabled || PlayerIsNull)
            {
                //Resetting script
                if (resetLights && Character.IsInVehicle() && !PlayerIsNull)
                {
                    Game.Player.Character.CurrentVehicle.HighBeamsOn = VhHighBeamsOn;
                    Game.Player.Character.CurrentVehicle.LightsOn = VhLightsOn;
                    resetLights = false;
                }
                return;
            }
            try
            {
                if (Game.IsPaused || !Character.IsInVehicle())
                {
                    return;
                }

                if (resetLights)
                {
                    Character.CurrentVehicle.HighBeamsOn = VhHighBeamsOn;
                    Character.CurrentVehicle.LightsOn = VhLightsOn;
                    VhHighBeamsOn = false;
                    VhLightsOn = false;
                    resetLights = false;
                }

                if (!(Game.IsControlPressed(Globals.GameInputMethod, GTA.Control.VehicleHorn) && !Globals.NativeUiIsAnyMenuOpen))
                {
                    return;
                }


                VhLightsOn = Character.CurrentVehicle.LightsOn;
                VhHighBeamsOn = Character.CurrentVehicle.HighBeamsOn;

                if (VhHighBeamsOn == false)
                {
                    Character.CurrentVehicle.LightsOn = true;
                    Character.CurrentVehicle.HighBeamsOn = true;
                }
                else if (VhHighBeamsOn == true)
                {
                    Character.CurrentVehicle.LightsOn = false;
                    Character.CurrentVehicle.HighBeamsOn = false;
                    Wait(100);
                    Character.CurrentVehicle.LightsOn = true;
                    Character.CurrentVehicle.HighBeamsOn = true;
                    Wait(100);
                    Character.CurrentVehicle.LightsOn = false;
                    Character.CurrentVehicle.HighBeamsOn = false;
                    Wait(100);
                    Character.CurrentVehicle.LightsOn = true;
                    Character.CurrentVehicle.HighBeamsOn = true;
                }
                resetLights = true;
            }
            catch (Exception exc)
            {
                Logger.LogToFile(PluginName, exc);
            }
        }
    }
}