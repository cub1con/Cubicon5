using System;
using GTA;
using GTA.Math;
using NativeUI;
using System.Windows.Forms;
using Cubicon5.Settings;

namespace Cubicon5
{
    public class MenuScript : Script
    {

        public NativeUI.MenuPool menuPool;


        NativeUI.UIMenu CubiconMenu = new UIMenu($"{Globals.PluginName}", $"V.{Globals.AssemblyVersion}");

        public MenuScript()
        {
            MenuSettings.InitSettings();
            UI.Notify($"{Globals.PluginName} starting");

            this.menuPool = new MenuPool();

            this.menuPool.Add(CubiconMenu);

            Menus.TurnLightsMenuItem.Option_TurnLights(CubiconMenu);
            Menus.HeadlightFlasherMenuItem.Option_HeadlightFlasher(CubiconMenu);
            Menus.SpeedometerMenuItem.Option_Speedometer(CubiconMenu);
            Menus.TempomatMenuMenuItem.Option_Tempomat(CubiconMenu);
            Menus.RecreateSettingsMenuItem.Button_RecreateSettings(CubiconMenu);
            Menus.AboutMenuItem.AboutMenu(CubiconMenu);

            this.menuPool.RefreshIndex();


            this.Tick += OnTick;
            this.KeyUp += OnKeyUp;
            this.KeyDown += OnKeyDown;


            UI.Notify($"{Globals.PluginName} is ready!", true);
        }
        private void OnTick(object sender, EventArgs e)
        {
            this.menuPool.ProcessMenus();
            Globals.NativeUiIsAnyMenuOpen = this.menuPool.IsAnyMenuOpen();

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


                switch (e.KeyCode)
                {
                    case Keys.ShiftKey:
                        if (!Game.IsPaused && Game.Player.Character.IsInVehicle())
                        {
                            var Vh = GTA.Game.Player.Character.CurrentVehicle;

                            Vh.Speed *= 2;
                        }

                        break;
                    case Keys.OemMinus:
                        if (!this.menuPool.IsAnyMenuOpen())
                        {
                            CubiconMenu.Visible = !CubiconMenu.Visible;
                        }
                        break;

                    case Keys.U:
                        if (!Game.IsPaused && Game.Player.Character.IsInVehicle())
                        {
                            var Vh = GTA.Game.Player.Character.CurrentVehicle;

                            Vh.Speed = 0;
                        }
                        break;
                    case Keys.I:
                        if (!Game.IsPaused && Game.Player.Character.IsInVehicle())
                        {
                            var Vh = GTA.Game.Player.Character.CurrentVehicle;

                            Vh.Speed = 0;
                        }
                        break;

                    default:
                        break;
                }

            }
        }
    }
}