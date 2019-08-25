using System;
using GTA;
using Cubicon5.Settings;
using Cubicon5.Helper;

namespace Cubicon5
{
    public class TempomatScript : Script
    {

        System.Drawing.Point SpeedPoint = new System.Drawing.Point(1280 - 100, 100);
        private readonly UIText UISpeedometer;

        private Vehicle Vehicle => GTA.Game.Player.Character.CurrentVehicle;
        private readonly int VehicleEngineHealth = 150;
        private readonly int VehicleCarHealth = 100;

        private bool TempomatEnabled;
        private float TempomatAccelerationRate = 0f;
        private float TempomatMaxSpeed = 2.777778f;

        private static readonly string PluginName = "HeadlightFlasher";

        public TempomatScript()
        {
            this.UISpeedometer = new UIText("", SpeedPoint, 0.65f, System.Drawing.Color.White, Font.ChaletComprimeCologne, true);
            this.Tick += OnTick;
        }

        private void OnTick(object sender, EventArgs e)
        {
            if (!MenuSettings.TempomatEnabled || !PlayerHelper.PlayerIsNotNull())
            {
                //Resetting script
                if (this.TempomatEnabled)
                {
                    this.ResetScript();
                }
                return;
            }
            try
            {
                if (Game.IsPaused || !Game.Player.Character.IsInVehicle() || !VehicleIsTempomatAllowed())
                {
                    if (this.TempomatEnabled && this.Vehicle == null)
                    {
                        UI.Notify("Tempomat OFF : Error 404 - No vehicle found");
                        this.ResetScript();
                    }
                    return;
                }

                //GTA.UI.ShowSubtitle($"{((this.TempomatMaxSpeed * 1.05) * 3.6f).ToString("000")} | {(this.TempomatMaxSpeed * 3.6f).ToString("000")} | {((this.TempomatMaxSpeed / 1.05) * 3.6f).ToString("000")}");
                if (this.TempomatEnabled)
                {
                    var ResetScript = false;

                    if (Game.IsControlPressed(Globals.GameInputMethod, GTA.Control.VehicleExit))
                    {
                        UI.Notify("Tempomat OFF : Error 404 - No driver found");
                        ResetScript = true;
                    }

                    if ((Game.IsControlPressed(Globals.GameInputMethod, GTA.Control.VehicleBrake) || Game.IsControlPressed(Globals.GameInputMethod, GTA.Control.VehicleHandbrake)))
                    {
                        UI.Notify("Tempomat OFF : Brake detected!");
                        ResetScript = true;
                    }

                    if (ResetScript)
                    {
                        this.ResetScript();
                        return;
                    }


                    this.UISpeedometer.Caption = $"Max Speed: {SpeedHelper.GetSpeedInKmh(this.TempomatMaxSpeed)}";
                    this.UISpeedometer.Draw();

                    //If vehicle is accelerating, TempomatMaxSpeed should change to VehicleSpeed
                    if (Game.IsControlPressed(1, GTA.Control.VehicleAccelerate))
                    {
                        if (IsVehicleAbnormal())
                        {
                            return;
                        }

                        //Player should be accelerating
                        if (Vehicle.Speed > (TempomatMaxSpeed * 1.05))
                        {
                            this.TempomatMaxSpeed = Vehicle.Speed;
                            return;
                        }

                    }

                }

                if (Game.IsControlPressed(Globals.GameInputMethod, GTA.Control.VehicleDuck) && !Globals.NativeUiIsAnyMenuOpen)
                {
                    this.TempomatEnabled = !this.TempomatEnabled;

                    if (this.TempomatEnabled && !IsVehicleAbnormal())
                    {
                        this.TempomatMaxSpeed = Vehicle.Speed;

                        GTA.UI.Notify("Tempomat ON");

                        if (this.TempomatMaxSpeed < 2.777778f)
                        {
                            this.TempomatMaxSpeed = 2.777778f;
                        }
                        GTA.UI.Notify($"Tempomat set to: {SpeedHelper.GetSpeedInKmh(this.TempomatMaxSpeed)}");

                    }
                    else
                    {
                        UI.Notify("Tempomat OFF : Disabled by driver");
                        this.ResetScript();
                    }
                    Script.Wait(250);

                }


                if (!this.TempomatEnabled || this.IsVehicleAbnormal())
                {
                    return;
                }


                //Mos MagicMathShit
                var factor = 10.0f;
                var perc = Vehicle.Speed / this.TempomatMaxSpeed;
                this.TempomatAccelerationRate = (1.06f - perc) * factor;
                this.TempomatAccelerationRate = Math.Min(1f, this.TempomatAccelerationRate); //max 1
                this.TempomatAccelerationRate = Math.Max(0f, this.TempomatAccelerationRate); //min 0

                //Accelerate
                Game.SetControlNormal(Globals.GameInputMethod, Control.VehicleAccelerate, TempomatAccelerationRate);

            }
            catch (Exception exc)
            {
                Logger.LogToFile(PluginName, exc);
            }
        }

        public void ResetScript()
        {
            this.TempomatEnabled = false;
            this.TempomatAccelerationRate = 0.0f;
            this.TempomatMaxSpeed = 2.777778f;
            this.UISpeedometer.Caption = "";
        }

        private bool VehicleIsTempomatAllowed()
        {
            return (this.Vehicle.ClassType != VehicleClass.Planes && this.Vehicle.ClassType != VehicleClass.Cycles && this.Vehicle.ClassType != VehicleClass.Helicopters);
        }

        public bool IsVehicleTireBurst()
        {
            return Vehicle.IsTireBurst(1) || Vehicle.IsTireBurst(2) || Vehicle.IsTireBurst(3) || Vehicle.IsTireBurst(4);
        }

        public bool IsVehicleAbnormal()
        {
            var found = false;
            //Vehicle Is In Air
            if (this.Vehicle.IsInAir)
            {
                GTA.UI.Notify("Tempomat OFF : Error - Vehicle in Air");
                found = true;
            }
            //Vehicle Engine Healt is low
            if (!found && this.Vehicle.EngineHealth < VehicleEngineHealth)
            {
                GTA.UI.Notify("Tempomat OFF : Error - Engine defect, please visit your car service station ");
                found = true;
            }
            //Vehicle Health is low
            if (!found && this.Vehicle.Health < this.VehicleCarHealth)
            {
                GTA.UI.Notify("Tempomat OFF : Error - Car is damaged, please visit your car service station ");
                found = true;
            }
            //Vehicle has Crash
            if (!found && (this.Vehicle.Speed > (this.TempomatMaxSpeed * 1.1) || this.Vehicle.Speed < (this.TempomatMaxSpeed / 1.1)) && !(this.TempomatMaxSpeed == 2.777778f))
            {
                GTA.UI.Notify("Tempomat OFF : Error - Car crashed");
                found = true;
            }
            //Tire is burst
            if (!found && this.IsVehicleTireBurst())
            {
                GTA.UI.Notify("Tempomat OFF : Error - Tire is defect");
            }

            //If Vehicle is abnormal, reset the script
            if (found)
            {
                this.ResetScript();
            }
            return found;
        }
    }
}