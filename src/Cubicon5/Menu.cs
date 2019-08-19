using System;
using GTA;
using GTA.Native;
using GTA.Math;
using GTA.NaturalMotion;
using NativeUI;
using System.Windows.Forms;
using Cubicon5.Settings;

namespace Cubicon5
{
    public class Menu : Script
    {

        private NativeUI.MenuPool menuPool;

        private float VehicleSpeed;
        private int VehicleEngineHealth = 150;
        private int VehicleCarHealth = 100;

        private bool TempomatEnabled;
        private bool TempomatAccelerating = false;
        private bool TempomatHasAccelerated = false;
        private float TempomatSpeed = 0f;
        private float TempomatMaxSpeed = 2.777778f;


        NativeUI.UIMenu CubiconMenu = new UIMenu($"{Globals.PluginName}", $"V.{Globals.AssemblyVersion}");

        public Menu()
        {
            MenuSettings.InitSettings();
            UI.Notify($"{Globals.PluginName} starting");



            this.menuPool = new MenuPool();
            this.menuPool.Add(CubiconMenu);
            this.Option_TurnLights(CubiconMenu);
            this.Option_HeadlightFlasher(CubiconMenu);
            this.Option_Speedometer(CubiconMenu);
            this.AboutMenu(CubiconMenu);

            this.menuPool.RefreshIndex();


            this.Tick += OnTick;
            this.KeyUp += OnKeyUp;
            this.KeyDown += OnKeyDown;


            UI.Notify($"{Globals.PluginName} is ready!", true);
        }

        public void AboutMenu(NativeUI.UIMenu Menu)
        {
            UIMenuItem newMenu = new UIMenuItem("Info", "Mod Information");
            Menu.AddItem(newMenu);
            Menu.OnItemSelect += delegate (UIMenu sender, UIMenuItem item, int index)
            {
                bool flag = item == newMenu;
                if (flag)
                {
                    BigMessageThread.MessageInstance.ShowSimpleShard("Info", $"Cubicon5{Environment.NewLine}By Cubicon", 5000);
                }
            };
        }

        public void Option_TurnLights(NativeUI.UIMenu Menu)
        {
            UIMenuCheckboxItem newMenu = new UIMenuCheckboxItem("Turn Lights", MenuSettings.TurnLightsEnabled);
            newMenu.CheckboxEvent += Option_TurnLights_OnCheckboxChange;
            Menu.AddItem(newMenu);
        }

        private void Option_TurnLights_OnCheckboxChange(UIMenuCheckboxItem sender, bool Checked)
        {
            MenuSettings.TurnLightsEnabled = Checked;
            if (Game.Player.Character.IsInVehicle())
            {
                GTA.Game.Player.Character.CurrentVehicle.LeftIndicatorLightOn = false;
                GTA.Game.Player.Character.CurrentVehicle.RightIndicatorLightOn = false;
            }
        }

        public void Option_HeadlightFlasher(NativeUI.UIMenu Menu)
        {
            UIMenuCheckboxItem newMenu = new UIMenuCheckboxItem("Headlight Flash on Horn", MenuSettings.HeadlightFlasherEnabled);
            newMenu.CheckboxEvent += Option_HeadlightFlasher_OnCheckboxChange;
            Menu.AddItem(newMenu);
        }

        private void Option_HeadlightFlasher_OnCheckboxChange(UIMenuCheckboxItem sender, bool Checked)
        {
            MenuSettings.HeadlightFlasherEnabled = Checked;
        }

        public void Option_Speedometer(NativeUI.UIMenu Menu)
        {
            UIMenuCheckboxItem newMenu = new UIMenuCheckboxItem("Speedometer", MenuSettings.SpeedometerEnabled);
            newMenu.CheckboxEvent += Option_Speedometer_OnCheckboxChange;
            Menu.AddItem(newMenu);
        }

        private void Option_Speedometer_OnCheckboxChange(UIMenuCheckboxItem sender, bool Checked)
        {
            MenuSettings.SpeedometerEnabled = Checked;
        }

        private void OnTick(object sender, EventArgs e)
        {
            //Logger.LogToUiNotify(e.ToString());
            this.menuPool.ProcessMenus();

            UI.ShowSubtitle($"{GTA.Game.Player.Character.CurrentVehicle.Acceleration}");
            if (!Game.IsPaused && Game.Player.Character.IsInVehicle())
            {

                VehicleSpeed = GTA.Game.Player.Character.CurrentVehicle.Speed;
                if (Game.IsControlPressed(Globals.GameInputMethod, GTA.Control.VehicleAccelerate) && this.TempomatEnabled)
                {
                    this.TempomatAccelerating = true;
                    this.TempomatMaxSpeed = GTA.Game.Player.Character.CurrentVehicle.Speed;
                    return;

                }
                else if (this.TempomatAccelerating)
                {
                    this.TempomatAccelerating = false;
                    this.TempomatHasAccelerated = true;
                }

                if (Game.IsControlPressed(Globals.GameInputMethod, GTA.Control.VehicleExit) && this.TempomatEnabled)
                {
                    UI.Notify("Tempomat OFF : Error 404 - No driver found");
                    this.TempomatEnabled = false;
                    this.TempomatMaxSpeed = 2.777778f;

                }
                if ((Game.IsControlPressed(Globals.GameInputMethod, GTA.Control.VehicleBrake) || Game.IsControlPressed(Globals.GameInputMethod, GTA.Control.VehicleHandbrake)) && this.TempomatEnabled)
                {
                    UI.Notify("Tempomat OFF : Brake detected!");
                    this.TempomatEnabled = false;
                    this.TempomatMaxSpeed = 2.777778f;
                }



                if (Game.IsControlPressed(Globals.GameInputMethod, GTA.Control.VehicleDuck))
                {
                    this.TempomatEnabled = !this.TempomatEnabled;

                    if (this.TempomatEnabled == true)
                    {

                        UI.Notify("IN ENabled");
                        this.TempomatMaxSpeed = GTA.Game.Player.Character.CurrentVehicle.Speed;
                        if (!IsVehicleAbnormal())
                        {
                            GTA.UI.Notify("Tempomat ON");
                            this.TempomatSpeed = GTA.Game.Player.Character.CurrentVehicle.Speed;
                            if (this.TempomatMaxSpeed < 2.777778f)
                            {
                                this.TempomatMaxSpeed = 2.777778f;
                            }
                            GTA.UI.Notify("Tempomat set to: " + Math.Round(this.TempomatMaxSpeed * 3.6f).ToString() + " km/h");

                        }
                    }

                    Script.Wait(500);
                }

                if (this.TempomatEnabled == true)
                {
                    //UI.Notify("Exec");
                    this.IsVehicleTireBurst();
                    if (!IsVehicleAbnormal())
                    {
                        if (this.TempomatSpeed < this.TempomatMaxSpeed)
                        {
                            this.TempomatSpeed += 0.1f;
                        }
                        Game.Player.Character.CurrentVehicle.Speed = this.TempomatSpeed;
                    }
                }
            }
        }

        public bool IsVehicleTireBurst()
        {
            return Game.Player.Character.CurrentVehicle.IsTireBurst(1) || Game.Player.Character.CurrentVehicle.IsTireBurst(2) || GTA.Game.Player.Character.CurrentVehicle.IsTireBurst(3) || GTA.Game.Player.Character.CurrentVehicle.IsTireBurst(4);
        }

        public bool IsVehicleAbnormal()
        {
            bool AbnormalVehicleStatus = (((this.VehicleSpeed > this.TempomatMaxSpeed + 0.3 || this.VehicleSpeed < this.TempomatMaxSpeed - 0.3)) && !this.TempomatHasAccelerated) || this.IsVehicleTireBurst() || GTA.Game.Player.Character.CurrentVehicle.IsInAir || GTA.Game.Player.Character.CurrentVehicle.EngineHealth < this.VehicleEngineHealth || GTA.Game.Player.Character.CurrentVehicle.Health < this.VehicleCarHealth;
            if (AbnormalVehicleStatus)
            {
                var found = false;
                //IsInAir
                if (GTA.Game.Player.Character.CurrentVehicle.IsInAir)
                {
                    GTA.UI.Notify("Tempomat OFF : Error - Vehicle in Air");
                    found = true;
                }
                //VehicleEngineHealtLow
                if (GTA.Game.Player.Character.CurrentVehicle.EngineHealth < VehicleEngineHealth && !found)
                {
                    GTA.UI.Notify("Tempomat OFF : Error - Engine defect, please visit your car service station ");
                    found = true;
                }
                //VehicleHealthLow
                if (GTA.Game.Player.Character.CurrentVehicle.Health < this.VehicleCarHealth && !found)
                {
                    GTA.UI.Notify("Tempomat OFF : Error - Car is damaged, please visit your car service station ");
                    found = true;
                }
                // CarCrash
                if (VehicleSpeed > TempomatMaxSpeed + 0.3 && !found)
                {
                    GTA.UI.Notify("Faster Tempomat OFF : Error - Car crashed");
                    found = true;
                }
                else if (VehicleSpeed < TempomatMaxSpeed - 0.3 && !found)
                {
                    GTA.UI.Notify($"{VehicleSpeed} smallerThen {TempomatMaxSpeed} - 0.3");
                    GTA.UI.Notify("Slower Tempomat OFF : Error - Car crashed");
                }
                else
                {
                    UI.Notify("LOOOOOL");
                }
                //TireBurst
                if (this.IsVehicleTireBurst())
                {
                    GTA.UI.Notify("Tempomat OFF : Error - Tire is defect");
                }
                this.TempomatEnabled = false;
                this.TempomatMaxSpeed = 2.777778f;
                return true;
            }
            return false;
        }

        private void OnKeyUp(object sender, KeyEventArgs e)
        {
            //if (!Game.IsPaused)
            //{
            //    switch (e.KeyCode)
            //    {

            //        case Keys.X:
            //            break;

            //        default:
            //            break;
            //    }

        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (!Game.IsPaused)
            {
                //Logger.LogToUiNotify(e.KeyCode.ToString());


                switch (e.KeyCode)
                {
                    case Keys.ShiftKey:
                        if (!Game.IsPaused && Game.Player.Character.IsInVehicle())
                        {
                            var Vh = GTA.Game.Player.Character.CurrentVehicle;

                            Vh.Speed *= 2;
                            //Vector3 velocity = Vh.Velocity;
                            //velocity.Z = velocity.Z + 10;
                            //Vh.Velocity = velocity;
                        }

                        break;
                    case Keys.OemMinus:
                        if (!this.menuPool.IsAnyMenuOpen())
                        {
                            CubiconMenu.Visible = !CubiconMenu.Visible;
                        }
                        break;

                    case Keys.U:
                        if (!Game.IsPaused)
                        {
                            var PlayerPed = GTA.Game.Player.Character;

                            Vector3 velocity = PlayerPed.Velocity;
                            velocity.Z += 100;
                            PlayerPed.Velocity = velocity;
                        }
                        break;
                    case Keys.I:

                        break;

                    default:
                        break;
                }

            }
        }
    }
}