using UnityEngine;

namespace MySummerCarTweaks.Tweaks.DisableAmis2
{
    internal class DisableAmis2Module : MscTweak
    {
        internal override string Name => "Disable Amis2";

        internal override void MSC_OnLoad(MySummerCarTweaksMod mod)
        {
            var gameObject3 = GameObject.Find("NPC_CARS/Amikset/AMIS2");
            gameObject3.SetActive(false);
        }
    }
}