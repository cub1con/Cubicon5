using Cubicon5.Settings;
using NativeUI;

namespace Cubicon5.Menus
{
    public static class TempomatMenuMenuItem
    {
        public static void Add_Option_Tempomat(NativeUI.UIMenu Menu)
        {
            UIMenuCheckboxItem newMenu = new UIMenuCheckboxItem("Tempomat", MenuSettings.TempomatEnabled);
            newMenu.CheckboxEvent += Option_Tempomat_OnCheckboxChange;
            Menu.AddItem(newMenu);
        }

        private static void Option_Tempomat_OnCheckboxChange(UIMenuCheckboxItem sender, bool Checked)
        {
            MenuSettings.TempomatEnabled = Checked;
        }
    }
}
