using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace MySummerCarTweaks.Tweaks.GifuGaugeFix
{
    internal class GifuGaugeFixModule : MscTweak
    {
        internal override string Name => "Gifu Gauge Fix";


        internal override void MSC_OnLoad(MySummerCarTweaksMod mod)
        {
            base.MSC_OnLoad(mod);

            var oilGauge = GameObject.Find("GIFU(750/450psi)").transform.Find("LOD/Dashboard/Gauges/Oil Pressure").gameObject;
            oilGauge.AddComponent<GifuOilGauge>();

            var batteryGauge = GameObject.Find("GIFU(750/450psi)").transform.Find("LOD/Dashboard/Gauges/Volts").gameObject;
            batteryGauge.AddComponent<GifuBatteryGauge>();
        }
    }
}
