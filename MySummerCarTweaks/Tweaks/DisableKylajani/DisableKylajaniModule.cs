using UnityEngine;

namespace MySummerCarTweaks.Tweaks.DisableKylajani
{
    internal class DisableKylajaniModule : MscTweak
    {
        internal override string Name => "Disable Kyläjäni";

        internal override void MSC_OnLoad(MySummerCarTweaksMod mod)
        {
            var gameObject2 = GameObject.Find("NPC_CARS/Amikset/KYLAJANI");
            gameObject2.SetActive(false);
        }
    }
}