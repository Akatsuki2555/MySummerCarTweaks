using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace MySummerCarTweaks.Tweaks.GarageCustomizer
{
    internal class PartsMagazineScript : MonoBehaviour
    {
        public Vector3 newPos = Vector3.zero;
        public Quaternion newRot = Quaternion.identity;

        void Update()
        {
            transform.localPosition = newPos;
            transform.localRotation = newRot;
        }
    }
}
