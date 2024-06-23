using MSCLoader;
using UnityEngine;

namespace MySummerCarTweaks.Tweaks.MoveJokkeInMenu
{
    internal class MoveJokkeInMenuModule : MscTweak
    {
        internal override string Name => "Move Jokke In Menu";

        internal override void MSC_OnMenuLoad(MySummerCarTweaksMod mod)
        {
            MoveJokke();
        }

        internal override void MSC_ModSettings(MySummerCarTweaksMod mod)
        {
            Settings.AddButton(mod, "Move Jokke In Menu", MoveJokke);
        }

        private static void MoveJokke()
        {
            var jokke = GameObject.Find("BeerCamp");
            jokke.transform.position = new Vector3(-6.05f, -1.32f, -1.04f);
            jokke.transform.eulerAngles = new Vector3(-90, 0, 71.774f);
        }
    }
}