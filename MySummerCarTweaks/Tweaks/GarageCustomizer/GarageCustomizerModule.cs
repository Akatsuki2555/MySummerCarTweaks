using MSCLoader;
using Steamworks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace MySummerCarTweaks.Tweaks.GarageCustomizer
{
    internal class GarageCustomizerModule : MscTweak
    {
        internal override string Name => "Garage Customizer";

        SettingsDropDownList _tablePosition, _flagPosition;

        internal override void MSC_ModSettings(MySummerCarTweaksMod mod)
        {
            _tablePosition = Settings.AddDropDownList(mod, "garageCustomizerTable", "Table Position", new string[]
            {
                "Default", "Other Side", "Towards Door", "Towards Door Other Side", "Remove"
            }, 0);

            _flagPosition = Settings.AddDropDownList(mod, "garageCustomizerFlag", "Flag Position", new string[]
            {
                "Default", "Other Side", "Disable"
            }, 0);
        }

        internal override void MSC_OnLoad(MySummerCarTweaksMod mod)
        {
            var partsMagazine = GameObject.Find("ITEMS").transform.Find("parts magazine(itemx)").gameObject;
            var partsMagazineScript = partsMagazine.AddComponent<PartsMagazineScript>();
            var table = GameObject.Find("YARD").transform.Find("Building/Garage/garage_table").gameObject;
            var batteryCharger = GameObject.Find("YARD").transform.Find("Building/Garage/BatteryCharger").gameObject;
            var batteryChargerTrigger = GameObject.Find("YARD").transform.Find("Building/Garage/BatteryCharger/TriggerCharger").gameObject;

            switch (_tablePosition.GetSelectedItemIndex())
            {
                case 1:
                    // Parts magazine Local Position (X,Y,Z): -2.3202f, 0.4261f, -11.267f; - Local Euler Angles (X,Y,Z): 0f, 98.42776f, 0f
                    partsMagazineScript.newPos = new Vector3(-2.3202f, 0.4261f, -11.267f);
                    partsMagazineScript.newRot = Quaternion.Euler(0f, 98.42776f, 0f);
                    // Garage table Local Position (X,Y,Z): 8.251259f, -5.772472f, 0.6833538f; - Local Euler Angles (X,Y,Z): 0f, 0f, 179.9999f
                    table.transform.localPosition = new Vector3(8.251259f, -5.772472f, 0.6833538f);
                    table.transform.localRotation = Quaternion.Euler(0f, 0f, 179.9999f);
                    // Battery charger Local Position (X,Y,Z): 8.374416f, -4.439572f, 1.067342f; - Local Euler Angles (X,Y,Z): 2.369752E-07f, -2.765657E-05f, 302.0093f
                    batteryCharger.transform.localPosition = new Vector3(8.274416f, -4.639572f, 1.067342f);
                    batteryCharger.transform.localRotation = Quaternion.Euler(0f, -2.765656E-05f, 302.0093f);
                    // Battery charger trigger Local Position (X,Y,Z): 0.2119999f, -0.295f, 0f
                    batteryChargerTrigger.transform.localPosition = new Vector3(0.2119999f, -0.295f, 0f);
                    break;
                case 2:
                    // Parts magazine Local Position (X,Y,Z): 1.4798f, 0.4261f, -7.267f; - Local Euler Angles (X,Y,Z): 0f, 278.4277f, 0f
                    partsMagazineScript.newPos = new Vector3(1.4798f, 0.4261f, -7.267f);
                    partsMagazineScript.newRot = Quaternion.Euler(0f, 278.4277f, 0f);
                    // Garage table Local Position (X,Y,Z): 4.251259f, -1.772472f, 0.6833538f; - Local Euler Angles (X,Y,Z): -2.897805E-05f, -6.602413E-06f, -9.505242E-05f
                    table.transform.localPosition = new Vector3(4.251259f, -1.772472f, 0.6833538f);
                    table.transform.localRotation = Quaternion.Euler(-2.897805E-05f, -6.602413E-06f, -9.505242E-05f);
                    // Battery charger Local Position (X,Y,Z): 4.074415f, -0.439572f, 1.067342f; - Local Euler Angles (X,Y,Z): 2.369752E-07f, -2.807379E-05f, 242.0093f
                    batteryCharger.transform.localPosition = new Vector3(4.074415f, -0.439572f, 1.067342f);
                    batteryCharger.transform.localRotation = Quaternion.Euler(2.369752E-07f, -2.807379E-05f, 242.0093f);
                    break;
                 case 3:
                    // Parts Magazine Local Position (X,Y,Z): -2.2202f, 0.4261f, -7.267f; - Local Euler Angles (X,Y,Z): 0f, 98.4277f, 0f
                    partsMagazineScript.newPos = new Vector3(-2.2202f, 0.4261f, -7.267f);
                    partsMagazineScript.newRot = Quaternion.Euler(0f, 98.4277f, 0f);
                    // Garage table Local Position (X,Y,Z): 8.251259f, -1.772472f, 0.683354f; - Local Euler Angles (X,Y,Z): 0f, 0f, 179.9999f
                    table.transform.localPosition = new Vector3(8.251259f, -1.772472f, 0.683354f);
                    table.transform.localRotation = Quaternion.Euler(0f, 0f, 179.9999f);
                    // Battery charger Local Position (X,Y,Z): 8.174416f, -0.539572f, 1.067342f; - Local Euler Angles (X,Y,Z): 2.369752E-07f, -2.807379E-05f, 299.0093f
                    batteryCharger.transform.localPosition = new Vector3(8.174416f, -0.539572f, 1.067342f);
                    batteryCharger.transform.localRotation = Quaternion.Euler(2.369752E-07f, -2.807379E-05f, 299.0093f);
                    // Battery charger trigger Local Position (X,Y,Z): 0.2119999f, -0.295f, 0f; - Local Euler Angles (X,Y,Z): 0f, 0f, 0f
                    batteryChargerTrigger.transform.localPosition = new Vector3(0.2119999f, -0.295f, 0f);
                    break;
                case 4:
                    GameObject.Destroy(table);
                    GameObject.Destroy(batteryCharger);
                    GameObject.Destroy(partsMagazine);
                    break;
            }

            var flag = GameObject.Find("YARD").transform.Find("Building/LOD/flag").gameObject;
            
            switch (_flagPosition.GetSelectedItemIndex())
            {
                case 1:
                    // Local Position (X,Y,Z): -18.97865f, 1.151047f, 2.756564f; - Local Euler Angles (X,Y,Z): 270f, 0.0001983643f, 0f
                    flag.transform.localPosition = new Vector3(-18.97865f, 1.151047f, 2.756564f);
                    flag.transform.localRotation = Quaternion.Euler(270f, 0.0001983643f, 0f);
                    break;
                case 2:
                    GameObject.Destroy(flag);
                    break;
            }
        }

    }
}
