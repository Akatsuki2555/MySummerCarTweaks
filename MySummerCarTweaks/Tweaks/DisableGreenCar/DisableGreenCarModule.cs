using UnityEngine;

namespace MySummerCarTweaks.Tweaks.DisableGreenCar
{
    internal class DisableGreenCarModule : MscTweak
    {
        internal override string Name => "Disable Green Car";

        internal override void MSC_OnLoad(MySummerCarTweaksMod mod)
        {
            var go = GameObject.Find("TRAFFIC/VehiclesDirtRoad/Rally/FITTAN");
            go.SetActive(false);
        }
    }
}