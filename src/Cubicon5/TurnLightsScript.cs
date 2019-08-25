using System;
using GTA;
using Cubicon5.Settings;
using Cubicon5.Helper;

namespace Cubicon5
{
    public class TurnLightsScript : Script
    {
        private bool LeftIndicator = false;
        private bool RightIndicator = false;
        private Vehicle Vehicle => Game.Player.Character.CurrentVehicle;

        private static readonly string PluginName = "TurnLights";
        public TurnLightsScript()
        {
            this.Tick += OnTick;
            this.Interval += 100;

            if (PlayerHelper.PlayerIsNotNull() && this.Vehicle != null)
            {
                Vehicle.LeftIndicatorLightOn = false;
                Vehicle.RightIndicatorLightOn = false;
            }
        }

        private void OnTick(object sender, EventArgs e)
        {
            var PlayerIsNull = !PlayerHelper.PlayerIsNotNull();
            if (!MenuSettings.TurnLightsEnabled || PlayerIsNull)
            {
                //Resetting script
                if (!PlayerIsNull && (this.LeftIndicator || this.RightIndicator))
                {
                    this.LeftIndicator = false;
                    this.RightIndicator = false;
                    this.Vehicle.LeftIndicatorLightOn = false;
                    this.Vehicle.RightIndicatorLightOn = false;
                }
                return;
            }
            try
            {
                if (Game.IsPaused || this.Vehicle == null)
                {
                    return;
                }

                //Turn off LeftIndicator
                if (this.LeftIndicator == true && !Game.IsControlPressed(Globals.GameInputMethod, GTA.Control.VehicleMoveLeftOnly))
                {
                    this.Vehicle.LeftIndicatorLightOn = false;
                    this.LeftIndicator = false;
                }
                //Turn off RightIndicator
                if (this.RightIndicator == true && !Game.IsControlPressed(Globals.GameInputMethod, GTA.Control.VehicleMoveRightOnly))
                {
                    this.Vehicle.RightIndicatorLightOn = false;
                    this.RightIndicator = false;
                }

                //Turn on LeftIndicator
                if (this.LeftIndicator == false && Game.IsControlPressed(Globals.GameInputMethod, GTA.Control.VehicleMoveLeftOnly))
                {
                    this.Vehicle.LeftIndicatorLightOn = true;
                    this.LeftIndicator = true;
                }
                //Turn on RightIndicator
                if (this.RightIndicator == false && Game.IsControlPressed(Globals.GameInputMethod, GTA.Control.VehicleMoveRightOnly))
                {
                    this.Vehicle.RightIndicatorLightOn = true;
                    this.RightIndicator = true;
                }
            }
            catch (Exception exc)
            {
                Logger.LogToFile(PluginName, exc);
            }
        }
    }
}
