using HutongGames.PlayMaker;
using MSCLoader;
using UnityEngine;

namespace MySummerCarTweaks.Tweaks.InfiniteNitrous
{
    internal class InfiniteNitrousModule : MscTweak
    {
        internal override string Name => "Infinite Nitrous";

        internal override void MSC_OnUpdate(MySummerCarTweaksMod mod)
        {
            var nitrous = GameObject.Find("n2o bottle(Clone)/Dynamics");
            if (nitrous != null && nitrous.activeSelf)
            {
                nitrous.GetPlayMaker("Pressure").GetVariable<FsmFloat>("Fluid").Value = 5;
            }
        }
    }
}