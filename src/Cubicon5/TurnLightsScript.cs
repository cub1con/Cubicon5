using System;
using GTA;
using Cubicon5.Settings;

namespace Cubicon5
{
    public class TurnLightsScript : Script
    {
        private bool LeftIndicator = false;
        private bool RightIndicator = false;
        private Vehicle Vehicle = null;

        private static readonly string PluginName = "TurnLights";
        public TurnLightsScript()
        {
            this.Tick += OnTick;
            this.Interval += 100;
            this.Vehicle = Game.Player.Character.CurrentVehicle;
            if (this.Vehicle != null)
            {
                GTA.Game.Player.Character.CurrentVehicle.LeftIndicatorLightOn = false;
                GTA.Game.Player.Character.CurrentVehicle.RightIndicatorLightOn = false;
            }

            UI.Notify($"{PluginName} started");
        }

        private void OnTick(object sender, EventArgs e)
        {
            if (!MenuSettings.TurnLightsEnabled)
            {
                //Resetting script
                if (this.LeftIndicator || this.RightIndicator)
                {
                    this.LeftIndicator = false;
                    this.RightIndicator = false;
                    this.Vehicle.LeftIndicatorLightOn = false;
                    this.Vehicle.RightIndicatorLightOn = false;
                }
                return;
            }
            if (!Game.IsPaused && this.Vehicle != null)
            {
                if (!Game.IsControlPressed(Globals.GameInputMethod, GTA.Control.VehicleMoveLeftOnly) && this.LeftIndicator == true)
                {
                    this.Vehicle.LeftIndicatorLightOn = false;
                    this.LeftIndicator = false;
                }
                if (!Game.IsControlPressed(Globals.GameInputMethod, GTA.Control.VehicleMoveRightOnly) && this.RightIndicator == true)
                {
                    this.Vehicle.RightIndicatorLightOn = false;
                    this.RightIndicator = false;
                }

                if (Game.IsControlPressed(Globals.GameInputMethod, GTA.Control.VehicleMoveLeftOnly) && this.LeftIndicator == false)
                {
                    this.Vehicle.LeftIndicatorLightOn = true;
                    this.LeftIndicator = true;
                }
                if (Game.IsControlPressed(Globals.GameInputMethod, GTA.Control.VehicleMoveRightOnly) && this.RightIndicator == false)
                {
                    this.Vehicle.RightIndicatorLightOn = true;
                    this.RightIndicator = true;
                }
            }


        }
    }
}
