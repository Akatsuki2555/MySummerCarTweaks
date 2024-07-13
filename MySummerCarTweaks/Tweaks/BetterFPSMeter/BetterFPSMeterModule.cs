using MSCLoader;
using UnityEngine;

namespace MySummerCarTweaks.Tweaks.BetterFPSMeter
{
    internal class BetterFPSMeterModule : MscTweak
    {
        internal override string Name => "Better FPS Meter";

        private SettingsSlider _frequency;
        private SettingsCheckBox _showDifference;
        private SettingsCheckBox _showColor;

        internal override void MSC_ModSettings(MySummerCarTweaksMod mod)
        {
            _frequency = Settings.AddSlider(mod, "frequency", "Frequency", 1f, 10f, 2f);
            Settings.AddText(mod, "Setting the frequency higher will make the FPS less accurate.");
            _showDifference = Settings.AddCheckBox(mod, "showDifference", "Show Difference");
            _showColor = Settings.AddCheckBox(mod, "showColor", "Show Color");
        }

        internal override void MSC_OnLoad(MySummerCarTweaksMod mod)
        {
            var betterFPSMeter = GameObject.Find("GUI/HUD/FPS/HUDValue").AddComponent<BetterFPSMeter>();
            betterFPSMeter.Frequency = _frequency.GetValue();
            betterFPSMeter.ShowDifference = _showDifference.GetValue();
            betterFPSMeter.Colors = _showColor.GetValue();
        }
    }
}