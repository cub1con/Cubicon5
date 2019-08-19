using System;
using System.Windows.Forms;
using GTA;
using Cubicon5.Settings;

namespace Cubicon5
{
    public class TurnLights : Script
    {
        private bool Links = false;
        private bool Rechts = false;


        public TurnLights()
        {
            this.Tick += OnTick;
            this.Interval += 100;
            GTA.Game.Player.Character.CurrentVehicle.LeftIndicatorLightOn = false;
            GTA.Game.Player.Character.CurrentVehicle.RightIndicatorLightOn = false;
            Logger.LogToUiNotifyWithoutMemberName("TurnLights has started!");
        }

        private void OnTick(object sender, EventArgs e)
        {
            try
            {
                if (MenuSettings.TurnLightsEnabled)
                {


                    if (!Game.IsPaused && Game.Player.Character.CurrentVehicle != null)
                    {
                        if (!Game.IsControlPressed(Globals.GameInputMethod, GTA.Control.VehicleMoveLeftOnly) && this.Links == true)
                        {
                            GTA.Game.Player.Character.CurrentVehicle.LeftIndicatorLightOn = false;
                            this.Links = false;
                            //UI.ShowSubtitle("Links Aus", 2000);
                        }
                        if (!Game.IsControlPressed(Globals.GameInputMethod, GTA.Control.VehicleMoveRightOnly) && this.Rechts == true)
                        {
                            GTA.Game.Player.Character.CurrentVehicle.RightIndicatorLightOn = false;
                            this.Rechts = false;
                            //UI.ShowSubtitle("Rechts Aus", 2000);
                        }

                        if (Game.IsControlPressed(Globals.GameInputMethod, GTA.Control.VehicleMoveLeftOnly) && this.Links == false)
                        {
                            GTA.Game.Player.Character.CurrentVehicle.LeftIndicatorLightOn = true;
                            this.Links = true;
                            //UI.ShowSubtitle("Links An", 2000);
                        }

                        if (Game.IsControlPressed(Globals.GameInputMethod, GTA.Control.VehicleMoveRightOnly) && this.Rechts == false)
                        {
                            GTA.Game.Player.Character.CurrentVehicle.RightIndicatorLightOn = true;
                            this.Rechts = true;
                            //UI.ShowSubtitle("Rechts An", 2000);
                        }
                    }
                }
            }
            catch (Exception exc)
            {
                Logger.LogToFile(exc.Message);
            }
        }
    }
}




//using System;
//using GTA;

//public class IndicatorControl : Script
//{
//    public IndicatorControl()
//    {
//        Tick += OnTick;
//        Interval = 100;
//    }

//    readonly bool[] _active = new bool[2];
//    readonly DateTime[] _timeLeft = new DateTime[2];

//    void OnTick(object sender, EventArgs e)
//    {
//        Ped playerPed = Game.Player.Character;

//        if (playerPed.IsInVehicle())
//        {
//            Vehicle vehicle = playerPed.CurrentVehicle;

//            if (Game.IsControlPressed(0, Control.VehicleMoveLeftOnly))
//            {
//                if (vehicle.Speed < 10.0f)
//                {
//                    vehicle.LeftIndicatorLightOn = _active[0] = true;
//                    vehicle.RightIndicatorLightOn = _active[1] = false;
//                    _timeLeft[0] = DateTime.Now + TimeSpan.FromMilliseconds(3000);
//                }
//            }
//            else if (_active[0] && DateTime.Now > _timeLeft[0])
//            {
//                vehicle.LeftIndicatorLightOn = _active[0] = false;
//            }

//            if (Game.IsControlPressed(0, Control.VehicleMoveRightOnly))
//            {
//                if (vehicle.Speed < 10.0f)
//                {
//                    vehicle.LeftIndicatorLightOn = _active[0] = false;
//                    vehicle.RightIndicatorLightOn = _active[1] = true;
//                    _timeLeft[1] = DateTime.Now + TimeSpan.FromMilliseconds(3000);
//                }
//            }
//            else if (_active[1] && DateTime.Now > _timeLeft[1])
//            {
//                vehicle.RightIndicatorLightOn = _active[1] = false;
//            }
//        }
//    }
//}
