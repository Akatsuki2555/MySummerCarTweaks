using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace MySummerCarTweaks.Tweaks.DisableRally
{
    internal class DisableRallyModule : MscTweak
    {
        internal override string Name => "Disable Rally";

        private GameObject _rallyCars;

        internal override void MSC_OnLoad(MySummerCarTweaksMod mod)
        {
            base.MSC_OnLoad(mod);

            _rallyCars = GameObject.Find("RALLY").transform.Find("RallyCars").gameObject;
        }

        internal override void MSC_OnUpdate(MySummerCarTweaksMod mod)
        {
            base.MSC_OnUpdate(mod);

            if (_rallyCars.activeSelf)
            {
                _rallyCars.SetActive(false);
            }
        }
    }
}
