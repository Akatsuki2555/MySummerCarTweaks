using HutongGames.PlayMaker.Actions;
using MSCLoader;
using UnityEngine;

namespace MySummerCarTweaks.Tweaks.BetterWindowHandles
{
    internal class BetterWindowHandlesModule : MscTweak
    {
        internal override string Name => "Better Window Handles";

        internal override void MSC_OnLoad(MySummerCarTweaksMod mod)
        {
            var component = GameObject.Find("door left(Clone)/opener").GetComponent<PlayMakerFSM>();
            component.GetState("State 1").GetAction<iTweenRotateUpdate>(0).time.Value /= 4f;
            component.GetState("State 1").GetAction<iTweenMoveUpdate>(1).time.Value /= 4f;
            component.GetState("State 2").GetAction<iTweenRotateUpdate>(0).time.Value /= 4f;
            component.GetState("State 2").GetAction<iTweenMoveUpdate>(1).time.Value /= 4f;
            var component2 = GameObject.Find("door right(Clone)/opener").GetComponent<PlayMakerFSM>();
            component2.GetState("State 1").GetAction<iTweenRotateUpdate>(0).time.Value /= 4f;
            component2.GetState("State 1").GetAction<iTweenMoveUpdate>(1).time.Value /= 4f;
            component2.GetState("State 2").GetAction<iTweenRotateUpdate>(0).time.Value /= 4f;
            component2.GetState("State 2").GetAction<iTweenMoveUpdate>(1).time.Value /= 4f;
        }
    }
}