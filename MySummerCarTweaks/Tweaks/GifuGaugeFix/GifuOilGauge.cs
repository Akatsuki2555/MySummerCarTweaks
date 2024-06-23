using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace MySummerCarTweaks.Tweaks.GifuGaugeFix
{
    internal class GifuOilGauge : MonoBehaviour
    {
        private Drivetrain _drivetrain;

        void Start()
        {
            var gifu = GameObject.Find("GIFU(750/450psi)");
            _drivetrain = gifu.GetComponent<Drivetrain>();
        }

        void Update()
        {
            // Top most Local Position (X,Y,Z): 0.25497f, 0.54809f, 0.9073f; - Local Euler Angles (X,Y,Z): 18.53548f, 24.3178f, 334.1908f; - Local Scale (X,Y,Z): 1f, 1f, 1f
            // Idle Local Position (X,Y,Z): 0.25497f, 0.54809f, 0.9073f; - Local Euler Angles (X,Y,Z): 18.53548f, 74.31778f, 334.1908f; - Local Scale (X,Y,Z): 1f, 1f, 1f
            // Off Local Position (X,Y,Z): 0.25497f, 0.54809f, 0.9073f; - Local Euler Angles (X,Y,Z): 18.53548f, 124.3178f, 334.1908f; - Local Scale (X,Y,Z): 1f, 1f, 1f


            transform.localRotation = Quaternion.Lerp(
                transform.localRotation,
                Quaternion.Euler(18.53548f - Mathf.Clamp(_drivetrain.rpm, 0, 2400) / 60, Mathf.Clamp(124.3178f - Mathf.Clamp(_drivetrain.rpm, 0, 2400) / 36, 25, 125), 334.1908f),
                Time.deltaTime * 4);
        }

    }
}
