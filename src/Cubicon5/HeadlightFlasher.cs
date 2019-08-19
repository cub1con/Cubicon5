using GTA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Cubicon5.Settings;

namespace Cubicon5
{
    public class HeadlightFlasher : Script
    {
        bool vhLightsOn;
        bool VhHighBeamsOn;
        bool resetLights;

        private static readonly string PluginName = "HeadlightFlasher";

        public HeadlightFlasher()
        {
            //    this.KeyUp += onKeyUp;
            //    this.KeyDown += onKeyDown;
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

                    if (Game.IsControlPressed(Globals.GameInputMethod, GTA.Control.VehicleHorn))
                    {
                        if (resetLights)
                        {
                            return;
                        }
                        vhLightsOn = Vh.LightsOn;
                        VhHighBeamsOn = Vh.HighBeamsOn;

                        if (VhHighBeamsOn == false && vhLightsOn == false)
                        {
                            Vh.LightsOn = true;
                            Vh.HighBeamsOn = true;

                        }
                        else if (VhHighBeamsOn == false && vhLightsOn == true)
                        {
                            Vh.LightsOn = true;
                            Vh.HighBeamsOn = true;
                        }
                        else if (VhHighBeamsOn == true && vhLightsOn == true)
                        {
                            //VhPerformingLightHorn = true;
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
                            //VhPerformingLightHorn = false;
                        }
                        resetLights = true;
                        return;
                    }

                    if (resetLights)
                    {
                        //if (VhHighBeamsOn == true)
                        //{
                        Vh.HighBeamsOn = VhHighBeamsOn;
                        //}
                        Vh.LightsOn = vhLightsOn;
                        resetLights = false;
                    }
                }

                //if (Game.IsControlJustReleased(GameInputMethod, GTA.Control.VehicleHorn))
                //{
                //    if (!VhPerformingLightHorn)
                //    {
                //        var Vh = GTA.Game.Player.Character.CurrentVehicle;
                //        Vh.LightsOn = vhLightsOn;
                //        Vh.HighBeamsOn = VhHighBeamsOn;
                //    }

                //}
            }

        }
        private void OnKeyUp(object sender, KeyEventArgs e)
        {
            if (!Game.IsPaused && Game.Player.Character.CurrentVehicle != null)
            {
                //if (Game.IsControlJustReleased(0, GTA.Control.VehicleHorn) || Game.IsControlJustReleased(2, GTA.Control.VehicleHorn))
                //if (Game.IsControlJustReleased(Convert.ToInt32(Game.CurrentInputMode), GTA.Control.VehicleHorn))
                //{
                //    if (!VhPerformingLightHorn)
                //    {
                //        var Vh = GTA.Game.Player.Character.CurrentVehicle;
                //        Vh.LightsOn = vhLightsOn;
                //        Vh.HighBeamsOn = VhHighBeamsOn;
                //    }

                //}
            }
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            //    //var ControlIndex = Game.GetU
            //    UI.Notify(Convert.ToString(Game.CurrentInputMode));
            //    if (!Game.IsPaused && Game.Player.Character.CurrentVehicle != null)
            //    {
            //        //if (Game.IsControlJustPressed(0, GTA.Control.VehicleHorn) || Game.IsControlJustPressed(2, GTA.Control.VehicleHorn))
            //        if (Game.IsControlPressed(Convert.ToInt32(Game.CurrentInputMode), GTA.Control.VehicleHorn))
            //        {
            //            var Vh = GTA.Game.Player.Character.CurrentVehicle;
            //            vhLightsOn = Vh.LightsOn;
            //            VhHighBeamsOn = Vh.HighBeamsOn;

            //            if (VhHighBeamsOn == false && vhLightsOn == false)
            //            {
            //                Vh.LightsOn = true;
            //                Vh.HighBeamsOn = true;
            //            }
            //            else if (VhHighBeamsOn == false && vhLightsOn == true)
            //            {
            //                Vh.HighBeamsOn = true;
            //            }
            //            else if (VhHighBeamsOn == true && vhLightsOn == true)
            //            {
            //                VhPerformingLightHorn = true;
            //                Vh.LightsOn = false;
            //                Vh.HighBeamsOn = false;
            //                Script.Wait(100);
            //                Vh.LightsOn = true;
            //                Vh.HighBeamsOn = true;
            //                Script.Wait(200);
            //                Vh.LightsOn = false;
            //                Vh.HighBeamsOn = false;
            //                Script.Wait(100);
            //                Vh.LightsOn = true;
            //                Vh.HighBeamsOn = true;
            //                VhPerformingLightHorn = false;
            //            }
            //        }
            //    }
        }
    }
}