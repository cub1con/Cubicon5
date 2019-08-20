using GTA;
using System;
using Cubicon5.Settings;

namespace Cubicon5
{
    public class HeadlightFlasherScript : Script
    {
        bool VhLightsOn;
        bool VhHighBeamsOn;
        bool resetLights;

        private static readonly string PluginName = "HeadlightFlasher";

        public HeadlightFlasherScript()
        {
            this.Tick += OnTick;
            this.Interval = 50;

            UI.Notify($"{PluginName} started");
        }

        private void OnTick(object sender, EventArgs e)
        {
            if (MenuSettings.HeadlightFlasherEnabled)
            {
                if (!Game.IsPaused && Game.Player.Character.IsInVehicle())
                {
                    var Vh = GTA.Game.Player.Character.CurrentVehicle;

                    if (Game.IsControlPressed(Globals.GameInputMethod, GTA.Control.VehicleHorn) && !Globals.NativeUiIsAnyMenuOpen)
                    {
                        if (resetLights)
                        {
                            return;
                        }
                        VhLightsOn = Vh.LightsOn;
                        VhHighBeamsOn = Vh.HighBeamsOn;

                        if (VhHighBeamsOn == false && VhLightsOn == false)
                        {
                            Vh.LightsOn = true;
                            Vh.HighBeamsOn = true;

                        }
                        else if (VhHighBeamsOn == false && VhLightsOn == true)
                        {
                            Vh.LightsOn = true;
                            Vh.HighBeamsOn = true;
                        }
                        else if (VhHighBeamsOn == true && VhLightsOn == true)
                        {
                            Vh.LightsOn = false;
                            Vh.HighBeamsOn = false;
                            Script.Wait(100);
                            Vh.LightsOn = true;
                            Vh.HighBeamsOn = true;
                            Script.Wait(100);
                            Vh.LightsOn = false;
                            Vh.HighBeamsOn = false;
                            Script.Wait(100);
                            Vh.LightsOn = true;
                            Vh.HighBeamsOn = true;
                        }
                        resetLights = true;
                        return;
                    }

                    if (resetLights)
                    {
                        Vh.HighBeamsOn = VhHighBeamsOn;
                        Vh.LightsOn = VhLightsOn;
                        resetLights = false;
                    }
                }
            }
            //Resetting script
            else if (resetLights && Game.Player.Character.IsInVehicle())
            {
                Game.Player.Character.CurrentVehicle.HighBeamsOn = VhHighBeamsOn;
                Game.Player.Character.CurrentVehicle.LightsOn = VhLightsOn;
                resetLights = true;
            }
        }
        
    }
}