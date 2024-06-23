using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace MySummerCarTweaks.Tweaks.MoneyChanges
{
    internal class MoneyChangesModule : MscTweak
    {
        internal override string Name => "Money Changes";

        internal override void MSC_OnLoad(MySummerCarTweaksMod mod)
        {
            base.MSC_OnLoad(mod);


            var hud = GameObject.Find("GUI").transform.Find("HUD").gameObject;
            var money = hud.transform.Find("Money").gameObject;

            var moneyClone = GameObject.Instantiate(money);
            moneyClone.transform.parent = hud.transform;
            // Local Position (X,Y,Z): -11.5f, 6.8f, 0f; - Local Euler Angles (X,Y,Z): 0f, 0f, 0f; - Local Scale (X,Y,Z): 1f, 1f, 1f
            moneyClone.transform.localPosition = new Vector3(-10f, 6.8f, 0f);
            moneyClone.name = "MoneyChange";
            GameObject.Destroy(moneyClone.transform.Find("HUDLabel").gameObject);
            GameObject.Destroy(moneyClone.transform.Find("BarBG").gameObject);

            var moneyCloneVal = moneyClone.transform.Find("HUDValue").gameObject;
            GameObject.Destroy(moneyCloneVal.GetComponent<PlayMakerFSM>());
            moneyCloneVal.AddComponent<MoneyChangesScript>();
        }
    }
}
