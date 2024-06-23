using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace MySummerCarTweaks.Tweaks.DisableBoat
{
    internal class DisableBoatModule : MscTweak
    {
        internal override string Name => "Disable AI Boat";

        internal override void MSC_OnLoad(MySummerCarTweaksMod mod)
        {
            base.MSC_OnLoad(mod);

            GameObject.Find("TRAFFIC").transform.Find("Lake").gameObject.SetActive(false);
        }
    }
}
