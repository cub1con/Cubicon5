using System;
using System.IO;
using GTA;

namespace Cubicon5.Settings
{
    public static class MenuSettings
    {
        public static string SettingsFile = @"scripts/Cubicon5.ini";

        private static bool _TurnLightsEnabled = false;
        public static bool TurnLightsEnabled
        {
            get
            {
                return _TurnLightsEnabled;
            }
            set
            {
                try
                {
                    SaveSetting("TurnLightsEnabled", value);
                    _TurnLightsEnabled = value;
                }
                catch (Exception e)
                {
                    UI.Notify($"~r~Error~w~: {SettingsFile} Failed To Save TurnLightsEnabled = {value}");
                    UI.Notify(e.Message);
                }
            }
        }

        private static bool _HeadlightFlasherEnabled = false;
        public static bool HeadlightFlasherEnabled
        {
            get
            {
                return _HeadlightFlasherEnabled;
            }
            set
            {
                try
                {
                    SaveSetting("HeadlightFlasherEnabled", value);
                    _HeadlightFlasherEnabled = value;
                }
                catch (Exception e)
                {
                    UI.Notify($"~r~Error~w~: {SettingsFile} Failed To Save HeadlightFlasherEnabled = {value}");
                    UI.Notify(e.Message);
                }
            }
        }

        private static bool _SpeedometerEnabled = false;
        public static bool SpeedometerEnabled
        {
            get
            {
                return _SpeedometerEnabled;
            }
            set
            {
                try
                {
                    SaveSetting("SpeedometerEnabled", value);
                    _SpeedometerEnabled = value;
                }
                catch (Exception e)
                {
                    UI.Notify($"~r~Error~w~: {SettingsFile} Failed To Save SpeedometerEnabled = {value}");
                    UI.Notify(e.Message);
                }
            }
        }

        private static bool _TempomatEnabled = false;
        public static bool TempomatEnabled
        {
            get
            {
                return _TempomatEnabled;
            }
            set
            {
                try
                {
                    SaveSetting("TempomatEnabled", value);
                    _TempomatEnabled = value;
                }
                catch (Exception e)
                {
                    UI.Notify($"~r~Error~w~: {SettingsFile} Failed To Save TempomatEnabled = {value}");
                    UI.Notify(e.Message);
                }
            }
        }


        public static void InitSettings()
        {
            if (!File.Exists(SettingsFile))
            {
                CreateSettings();
            }
            LoadSettings();
        }

        public static void CreateSettings()
        {
            //If Settings Exist, delete and recreate it
            if (File.Exists(SettingsFile))
            {
                File.Delete(SettingsFile);
            }
            string content = "[SETTINGS]\nTurnLightsEnabled = true\nHeadlightFlasherEnabled = true\nSpeedometerEnabled = true\nTempomatEnabled = true\n";
            StreamWriter streamWriter = null;

            try
            {
                streamWriter = File.CreateText(SettingsFile);
                streamWriter.WriteLine(content);
                
            }
            catch (Exception e)
            {
                UI.Notify($"~r~Error~w~: {SettingsFile} Failed To Create.");
                UI.Notify(e.Message);
            }
            finally
            {
                streamWriter.Close();
            }
        }


        public static void SaveSettings()
        {
            try
            {
                GTA.ScriptSettings scriptSettings = global::GTA.ScriptSettings.Load(SettingsFile);
                scriptSettings.SetValue<bool>("SETTINGS", "TurnLightsEnabled", _TurnLightsEnabled);
                scriptSettings.SetValue<bool>("SETTINGS", "HeadlightFlasherEnabled", _HeadlightFlasherEnabled);
                scriptSettings.SetValue<bool>("SETTINGS", "SpeedometerEnabled", _SpeedometerEnabled);
                scriptSettings.SetValue<bool>("SETTINGS", "TempomatEnabled", _TempomatEnabled);
                scriptSettings.Save();

            }
            catch (Exception e)
            {
                UI.Notify($"~r~Error~w~: {SettingsFile} Failed To Save.");
                UI.Notify(e.Message);
            }


        }

        public static void SaveSetting(string Setting, bool Value)
        {
            try
            {
                GTA.ScriptSettings scriptSettings = global::GTA.ScriptSettings.Load(SettingsFile);
                scriptSettings.SetValue<bool>("SETTINGS", Setting, Value);
                scriptSettings.Save();
            }
            catch (Exception e)
            {
                UI.Notify($"~r~Error~w~: {SettingsFile} Failed To Save {Setting} = {Value}");
                UI.Notify(e.Message);
            }
        }

        public static bool LoadSetting(string Setting)
        {
            try
            {
                GTA.ScriptSettings scriptSettings = global::GTA.ScriptSettings.Load(SettingsFile);
                return scriptSettings.GetValue<bool>("SETTINGS", Setting, false);

            }
            catch (Exception e)
            {
                UI.Notify($"~r~Error~w~: {SettingsFile} Failed To Load {Setting} - Setting to false");
                UI.Notify(e.Message);
                return false;
            }
        }

        public static void LoadSettings()
        {
            try
            {
                GTA.ScriptSettings scriptSettings = GTA.ScriptSettings.Load(SettingsFile);
                _TurnLightsEnabled = scriptSettings.GetValue<bool>("SETTINGS", "TurnLightsEnabled", true);
                _HeadlightFlasherEnabled = scriptSettings.GetValue<bool>("SETTINGS", "HeadlightFlasher", true);
                _SpeedometerEnabled = scriptSettings.GetValue<bool>("SETTINGS", "SpeedometerEnabled", true);
                _TempomatEnabled = scriptSettings.GetValue<bool>("SETTINGS", "TempomatEnabled", true);
            }
            catch (Exception e)
            {
                UI.Notify($"~r~Error~w~: {SettingsFile} Failed To Load Settings");
                UI.Notify(e.Message);
            }
        }
    }

}

