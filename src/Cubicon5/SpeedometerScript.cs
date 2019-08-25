using GTA;
using System;
using Cubicon5.Settings;
using Cubicon5.Helper;

namespace Cubicon5
{
    public class SpeedometerScript : Script
    {

        private static readonly string PluginName = "Speedometer";

        System.Drawing.Point SpeedPoint = new System.Drawing.Point(1280 - 100, 720 - 150);
        System.Drawing.PointF RpmPoint = new System.Drawing.Point(1280 - 161, 720 - 130);
        System.Drawing.PointF RpmSize = new System.Drawing.PointF(120, 100);

        private readonly UIText UISpeedometer;
        private readonly GTA.Scaleform RpmMeter = new Scaleform("CLUBHOUSE_NAME");
        private GTA.Math.Vector3 prevPos;
        private Ped Character => GTA.Game.Player.Character;

        public SpeedometerScript()
        {
            this.UISpeedometer = new UIText("", SpeedPoint, 1, System.Drawing.Color.White, Font.ChaletComprimeCologne, true);

            this.Tick += OnTick;
        }

        private void OnTick(object sender, EventArgs e)
        {
            if (!MenuSettings.SpeedometerEnabled || !EnableSpeedometer())
            {
                return;
            }
            try
            {
                this.UISpeedometer.Caption = SpeedHelper.GetSpeedInKmh(GetPlayerSpeed());
                this.DrawMeters();
            }
            catch(Exception exc)
            {
                Logger.LogToFile(PluginName, exc);
            }
            
            //UI.ShowSubtitle("jap.");

            this.prevPos = this.Character.Position;
        }

        private bool EnableSpeedometer()
        {
            return PlayerHelper.PlayerIsNotNull() && (this.Character.IsInVehicle() || this.Character.IsInParachuteFreeFall || this.Character.IsFalling);
        }

        private float GetPlayerSpeed()
        {
            if (this.Character.IsInVehicle())
            {
                return this.Character.CurrentVehicle.Speed;
            }
            else if (this.Character.IsInParachuteFreeFall || this.Character.IsFalling)
            {
                return this.GetSpeedFromPosChange(this.Character);
            }
            return 0f;
        }

        private void DrawRpm()
        {
            RpmMeter.CallFunction("SET_CLUBHOUSE_NAME", new object[]
            {
                $"{GetRPMText(this.Character.CurrentVehicle)}",
                GetRPMColor(this.Character.CurrentVehicle),
                1
            });
            RpmMeter.Render2DScreenSpace(RpmPoint, RpmSize);
            
        }

        private void DrawMeters()
        {
            this.UISpeedometer.Draw();
            //No RPM in Fall/Helicopter/Plane
            if (this.CanDrawRpm())
            {
                this.DrawRpm();
            }
        }
        
        private bool CanDrawRpm()
        {
            return this.Character.IsInVehicle() || this.Character.CurrentVehicle.ClassType != VehicleClass.Helicopters || this.Character.CurrentVehicle.ClassType != VehicleClass.Planes;
        }

        private float GetSpeedFromPosChange(GTA.Entity entity)
        {
            float num = entity.Position.DistanceTo(this.prevPos);
            return num / Game.LastFrameTime;
        }

        public static string GetRPMText(GTA.Vehicle entity)
        {
            
            var length = (int)(entity.CurrentRPM * 20);
            var RpmString = "|";
            if (length > 0)
            {
                RpmString = new string('|', length);
            }
           
            RpmString += new string('|', new Random().Next(2));
            return RpmString;
        }

        public static int GetRPMColor(GTA.Vehicle entity)
        {
            double CurrentRPM = Math.Round((double)entity.CurrentRPM, 2);
            int RPMColor;
            if (CurrentRPM <= 0.25)
            {
                RPMColor = 6;
            }
            else if (CurrentRPM <= 0.60)
            {
                RPMColor = 3;
            }
            else if (CurrentRPM <= 0.80)
            {
                RPMColor = 7;
            }
            else if (CurrentRPM <= 0.95)
            {
                RPMColor = 5;
            }
            else
            {
                RPMColor = 4;
            }
            return RPMColor;
        }

    }
}