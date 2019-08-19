﻿using GTA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Cubicon5.Settings;

namespace Cubicon5
{
    public class Speedometer : Script
    {

        private static readonly string PluginName = "Speedometer";

        System.Drawing.Point SpeedPoint = new System.Drawing.Point(GTA.Game.ScreenResolution.Width - 100, GTA.Game.ScreenResolution.Height - 150);
        System.Drawing.PointF RpmPoint = new System.Drawing.Point(GTA.Game.ScreenResolution.Width - 161, GTA.Game.ScreenResolution.Height - 130);
        System.Drawing.PointF RpmSize = new System.Drawing.PointF(120, 100);
        private UIText UISpeedometer;
        private GTA.Scaleform RpmMeter = new Scaleform("CLUBHOUSE_NAME");
        private GTA.Math.Vector3 prevPos;

        public Speedometer()
        {
            this.UISpeedometer = new UIText("", SpeedPoint, 1, System.Drawing.Color.White, Font.ChaletComprimeCologne, true);
            

            this.Tick += OnTick;

            UI.Notify($"{PluginName} started");

        }

        private void OnTick(object sender, EventArgs e)
        {
            if (MenuSettings.SpeedometerEnabled)
            {
                float PlayerSpeedThisFrame = 0f;
                if (Game.Player.Character.IsInVehicle())
                {
                    PlayerSpeedThisFrame = Game.Player.Character.CurrentVehicle.Speed;
                }
                else if (Game.Player.Character.IsInParachuteFreeFall || Game.Player.Character.IsFalling)
                {
                    PlayerSpeedThisFrame = this.GetSpeedFromPosChange(Game.Player.Character);
                }

                if (Game.Player.Character.IsInVehicle() || Game.Player.Character.IsInParachuteFreeFall || Game.Player.Character.IsFalling)
                {
                    Update(PlayerSpeedThisFrame, this.UISpeedometer);
                    this.DrawMeters();
                }

                if (Game.Player != null && Game.Player.Character != null)
                {
                    this.prevPos = global::GTA.Game.Player.Character.Position;
                }
            }
        }

        private void DrawRpm()
        {
            var Vh = Game.Player.Character.CurrentVehicle;
            RpmMeter.CallFunction("SET_CLUBHOUSE_NAME", new object[]
            {
                string.Format("{0}", Speedometer.GetRPMText(Vh)),
                Speedometer.GetRPMColor(Vh),
                1
            });
            RpmMeter.Render2DScreenSpace(RpmPoint, RpmSize);
        }

        private void DrawMeters()
        {
            this.UISpeedometer.Draw();
            this.DrawRpm();
        }

        private float GetSpeedFromPosChange(global::GTA.Entity entity)
        {
            float num = entity.Position.DistanceTo(this.prevPos);
            return num / global::GTA.Game.LastFrameTime;
        }

        private void Update(float speedThisFrame, UIText UiText)
        {
            float num = speedThisFrame * 3600f / 1000f;
            UiText.Caption = global::System.Math.Floor((double)num).ToString("0 " + "KM\\H");
        }


        public static string GetRPMText(GTA.Vehicle entity)
        {
            int num = new global::System.Random().Next(2);
            string text = "|";
            string str = text;
            double num2 = global::System.Math.Round((double)(entity.CurrentRPM * 3f), 2);
            if (num2 <= 0.60000002384185791)
            {
                text = "|";
            }
            else if (num2 <= 0.699999988079071)
            {
                text = "||";
            }
            else if (num2 <= 0.800000011920929)
            {
                text = "|||";
            }
            else if (num2 <= 0.89999997615814209)
            {
                text = "||||";
            }
            else if (num2 <= 1.0)
            {
                text = "|||||";
            }
            else if (num2 <= 1.1000000238418579)
            {
                text = "||||||";
            }
            else if (num2 <= 1.2000000476837158)
            {
                text = "|||||||";
            }
            else if (num2 <= 1.2999999523162842)
            {
                text = "||||||||";
            }
            else if (num2 <= 1.3999999761581421)
            {
                text = "|||||||||";
            }
            else if (num2 <= 1.5)
            {
                text = "||||||||||";
            }
            else if (num2 <= 1.6000000238418579)
            {
                text = "|||||||||||";
            }
            else if (num2 <= 1.7000000476837158)
            {
                text = "||||||||||||";
            }
            else if (num2 <= 1.7999999523162842)
            {
                text = "|||||||||||||";
            }
            else if (num2 <= 1.8999999761581421)
            {
                text = "||||||||||||||";
            }
            else if (num2 <= 2.0)
            {
                text = "|||||||||||||||";
            }
            else if (num2 <= 2.0999999046325684)
            {
                text = "||||||||||||||||";
            }
            else if (num2 <= 2.2000000476837158)
            {
                text = "|||||||||||||||||";
            }
            else if (num2 <= 2.2999999523162842)
            {
                text = "||||||||||||||||||";
            }
            else if (num2 <= 2.4000000953674316)
            {
                text = "|||||||||||||||||||";
            }
            else if (num2 <= 2.5)
            {
                text = "||||||||||||||||||||";
            }
            else if (num2 <= 2.5999999046325684)
            {
                text = "|||||||||||||||||||||";
            }
            else if (num2 <= 2.7000000476837158)
            {
                text = "||||||||||||||||||||||";
            }
            else if (num2 <= 2.7999999523162842)
            {
                text = "|||||||||||||||||||||||";
            }
            else if (num2 <= 2.7999999523162842)
            {
                text = "||||||||||||||||||||||||";
            }
            else
            {
                text = "|||||||||||||||||||||||||";
            }
            switch (num)
            {
                case 0:
                    str = "";
                    break;
                case 1:
                    str = "|";
                    break;
                case 2:
                    str = "||";
                    break;
            }
            return text + str;
        }

        // Token: 0x06000015 RID: 21 RVA: 0x000029B8 File Offset: 0x00000BB8
        public static int GetRPMColor(GTA.Vehicle entity)
        {
            double num = global::System.Math.Round((double)entity.CurrentRPM, 2);
            int result;
            if (num <= 0.25)
            {
                result = 6;
            }
            else if (num <= 0.60000002384185791)
            {
                result = 3;
            }
            else if (num <= 0.800000011920929)
            {
                result = 7;
            }
            else if (num <= 0.949999988079071)
            {
                result = 5;
            }
            else
            {
                result = 4;
            }
            return result;
        }

    }
}