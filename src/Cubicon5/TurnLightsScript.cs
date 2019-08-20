using System;
using System.Windows.Forms;
using GTA;
using Cubicon5.Settings;

namespace Cubicon5
{
    public class TurnLightsScript : Script
    {
        private bool Links = false;
        private bool Rechts = false;

        private static readonly string PluginName = "TurnLights";
        public TurnLightsScript()
        {
            this.Tick += OnTick;
            this.Interval += 100;
            GTA.Game.Player.Character.CurrentVehicle.LeftIndicatorLightOn = false;
            GTA.Game.Player.Character.CurrentVehicle.RightIndicatorLightOn = false;

            UI.Notify($"{PluginName} started");
        }

        private void OnTick(object sender, EventArgs e)
        {
            if (MenuSettings.TurnLightsEnabled)
            {
                if (!Game.IsPaused && Game.Player.Character.CurrentVehicle != null)
                {
                    if (!Game.IsControlPressed(Globals.GameInputMethod, GTA.Control.VehicleMoveLeftOnly) && this.Links == true)
                    {
                        GTA.Game.Player.Character.CurrentVehicle.LeftIndicatorLightOn = false;
                        this.Links = false;
                    }
                    if (!Game.IsControlPressed(Globals.GameInputMethod, GTA.Control.VehicleMoveRightOnly) && this.Rechts == true)
                    {
                        GTA.Game.Player.Character.CurrentVehicle.RightIndicatorLightOn = false;
                        this.Rechts = false;
                    }

                    if (Game.IsControlPressed(Globals.GameInputMethod, GTA.Control.VehicleMoveLeftOnly) && this.Links == false)
                    {
                        GTA.Game.Player.Character.CurrentVehicle.LeftIndicatorLightOn = true;
                        this.Links = true;
                    }
                    if (Game.IsControlPressed(Globals.GameInputMethod, GTA.Control.VehicleMoveRightOnly) && this.Rechts == false)
                    {
                        GTA.Game.Player.Character.CurrentVehicle.RightIndicatorLightOn = true;
                        this.Rechts = true;
                    }
                }
            }
            //Resetting script
            else if (this.Links || this.Rechts)
            {
                this.Links = false;
                this.Rechts = false;
                GTA.Game.Player.Character.CurrentVehicle.LeftIndicatorLightOn = false;
                GTA.Game.Player.Character.CurrentVehicle.RightIndicatorLightOn = false;
            }
        }
    }
}
