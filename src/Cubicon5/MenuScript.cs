using System;
using GTA;
using NativeUI;
using System.Windows.Forms;
using Cubicon5.Settings;
using Cubicon5.Helper;

namespace Cubicon5
{
    public class MenuScript : Script
    {

        public NativeUI.MenuPool menuPool;
        private const string PluginName = "Menu";


        readonly UIMenu CubiconMenu = new UIMenu($"{Globals.PluginName}", $"V.{Globals.AssemblyVersion}");

        public MenuScript()
        {
            Globals.Settings = MenuSettings.InitSettings();

            this.menuPool = new MenuPool();

            this.menuPool.Add(CubiconMenu);

            Menus.TurnLightsMenuItem.Add_Option_TurnLights(CubiconMenu);
            Menus.HeadlightFlasherMenuItem.Add_Option_HeadlightFlasher(CubiconMenu);
            Menus.SpeedometerMenuItem.Add_Option_Speedometer(CubiconMenu);

            var tempoSubMenu = this.menuPool.AddSubMenu(CubiconMenu, "Tempomat Options");
            tempoSubMenu.AddItem(Menus.TempomatMenuMenuItem.TempomatEnableMenuItem());
            tempoSubMenu.AddItem(Menus.TempomatMenuMenuItem.TempomatIgnoreVehicleInAirMenuItem());
            
            Menus.RecreateSettingsMenuItem.Add_Button_RecreateSettings(CubiconMenu);
            Menus.AboutMenuItem.Add_Item_AboutMenu(CubiconMenu);

            this.menuPool.RefreshIndex();


            this.Tick += OnTick;
            this.KeyDown += OnKeyDown;


            UI.Notify($"{Globals.PluginName} started!", true);
        }

        private void OnTick(object sender, EventArgs e)
        {
            try
            {
                this.menuPool.ProcessMenus();
                Globals.NativeUiIsAnyMenuOpen = this.menuPool.IsAnyMenuOpen();
            }
            catch (Exception exc)
            {
                Logger.LogToFile(PluginName, exc);
            }
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            try
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
                        case Keys.L:
                            Game.Player.Character.Kill();
                            break;
                        default:
                            break;
                    }

                }
            }
            catch (Exception exc)
            {
                Logger.LogToFile(PluginName, exc);
            }
        }
    }
}