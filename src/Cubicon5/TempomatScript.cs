using System;
using GTA;
using Cubicon5.Settings;

namespace Cubicon5
{
    public class TempomatScript : Script
    {
        private Vehicle Vehicle;
        private int VehicleEngineHealth = 150;
        private int VehicleCarHealth = 100;

        private bool TempomatEnabled;
        private bool DriverHasAccelerated = false;
        private float TempomatAccelerationRate = 0f;
        private float TempomatMaxSpeed = 2.777778f;

        private static readonly string PluginName = "HeadlightFlasher";

        public TempomatScript()
        {
            this.Tick += OnTick;

            UI.Notify($"{PluginName} started");
        }

        private void OnTick(object sender, EventArgs e)
        {
            if (!MenuSettings.TempomatEnabled)
            {
                if (!this.TempomatEnabled)
                {
                    return;
                }
                //Resetting script
                this.ResetScript();
                return;
            }

            if (!Game.IsPaused && Game.Player.Character.IsInVehicle())
            {
                //GTA.UI.ShowSubtitle($"{((this.TempomatMaxSpeed * 1.05) * 3.6f).ToString("000")} | {(this.TempomatMaxSpeed * 3.6f).ToString("000")} | {((this.TempomatMaxSpeed / 1.05) * 3.6f).ToString("000")}");
                this.Vehicle = Game.Player.Character.CurrentVehicle;
               
                if (Game.IsControlPressed(1, GTA.Control.VehicleAccelerate) && this.TempomatEnabled)
                {
                    if (IsVehicleAbnormal())
                    {
                        return;
                    }
                    if (!(Vehicle.Speed < (TempomatMaxSpeed * 1.05)))
                    {
                        this.DriverHasAccelerated = true;
                        this.TempomatMaxSpeed = Vehicle.Speed;
                        return;
                    }

                }

                if (this.DriverHasAccelerated)
                {
                    GTA.UI.Notify("Tempomat increased to: " + Math.Round(this.TempomatMaxSpeed * 3.6f).ToString() + " km/h");
                    this.DriverHasAccelerated = false;
                }


                if (Game.IsControlPressed(Globals.GameInputMethod, GTA.Control.VehicleExit) && this.TempomatEnabled)
                {
                    UI.Notify("Tempomat OFF : Error 404 - No driver found");
                    this.ResetScript();

                }
                if ((Game.IsControlPressed(Globals.GameInputMethod, GTA.Control.VehicleBrake) || Game.IsControlPressed(Globals.GameInputMethod, GTA.Control.VehicleHandbrake)) && this.TempomatEnabled)
                {
                    UI.Notify("Tempomat OFF : Brake detected!");
                    this.ResetScript();
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
                        GTA.UI.Notify($"Tempomat set to: " + Math.Round((this.TempomatMaxSpeed * 3600f) / 1000f).ToString() + " km/h");

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

                //GTA.UI.ShowSubtitle($"Acc to: {((this.TempomatAccelerationRate)).ToString()}");

                Game.SetControlNormal(Globals.GameInputMethod, Control.VehicleAccelerate, TempomatAccelerationRate);

            }

        }

        public void ResetScript()
        {
            this.TempomatEnabled = false;
            this.DriverHasAccelerated = false;
            this.TempomatAccelerationRate = 0.0f;
            this.TempomatMaxSpeed = 2.777778f;

        }

        public bool IsVehicleTireBurst()
        {
            return Vehicle.IsTireBurst(1) || Vehicle.IsTireBurst(2) || Vehicle.IsTireBurst(3) || Vehicle.IsTireBurst(4);
        }

        public bool IsVehicleAbnormal()
        {
            var found = false;
            //VehicleIsInAir;
            if (Vehicle.IsInAir)
            {
                GTA.UI.Notify("Tempomat OFF : Error - Vehicle in Air");
                found = true;
            }
            //VehicleEngineHealtLow
            if (!found && Vehicle.EngineHealth < VehicleEngineHealth)
            {
                GTA.UI.Notify("Tempomat OFF : Error - Engine defect, please visit your car service station ");
                found = true;
            }
            //VehicleHealthLow
            if (!found && Vehicle.Health < this.VehicleCarHealth)
            {
                GTA.UI.Notify("Tempomat OFF : Error - Car is damaged, please visit your car service station ");
                found = true;
            }
            //CarCrash // TODO
            if (!found && (/*this.Vehicle.Speed > (this.TempomatMaxSpeed * 1.05) ||*/ this.Vehicle.Speed < (this.TempomatMaxSpeed / 1.05)) && !(this.TempomatMaxSpeed == 2.777778f))
            {
                GTA.UI.Notify("Tempomat OFF : Error - Car crashed");
                found = true;
            }
            //TireBurst
            if (!found && this.IsVehicleTireBurst())
            {
                GTA.UI.Notify("Tempomat OFF : Error - Tire is defect");
            }

            if (found)
            {
                this.ResetScript();
            }
            return found;
        }
    }
}