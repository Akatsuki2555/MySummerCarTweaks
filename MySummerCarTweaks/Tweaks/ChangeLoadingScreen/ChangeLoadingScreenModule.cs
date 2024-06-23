using MSCLoader;
using UnityEngine;

namespace MySummerCarTweaks.Tweaks.ChangeLoadingScreen
{
    internal class ChangeLoadingScreenModule : MscTweak
    {
        private LoadingScreen _loadingScreen;
        internal static SettingsCheckBox RainbowLoadingScreen;
        internal static SettingsSliderInt Year;
        internal override string Name => "Change Loading Screen";


        internal override void MSC_ModSettings(MySummerCarTweaksMod mod)
        {
            RainbowLoadingScreen = Settings.AddCheckBox(mod, "loadingScreenRainbow", "Rainbow Loading Screen", false,
                () =>
                {
                    if (_loadingScreen != null)
                        _loadingScreen.Rainbow = RainbowLoadingScreen.GetValue();
                });

            var currentYear = System.DateTime.Now.Year;
            Year = Settings.AddSlider(mod, "loadingScreenYear", "Year", 1986, currentYear, 1995, () =>
            {
                if (_loadingScreen != null)
                    _loadingScreen.Year = Year.GetValue();
            });
        }


        internal override void MSC_OnMenuLoad(MySummerCarTweaksMod mod)
        {
            foreach (var gameObject in Resources.FindObjectsOfTypeAll<GameObject>())
                if (gameObject.transform.parent == null && gameObject.name == "Loading")
                    _loadingScreen = gameObject.transform.GetChild(2).gameObject.AddComponent<LoadingScreen>();
        }
    }
}