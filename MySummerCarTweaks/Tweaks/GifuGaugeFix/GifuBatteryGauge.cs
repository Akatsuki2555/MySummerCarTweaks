using HutongGames.PlayMaker;
using MSCLoader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.SocialPlatforms;

namespace MySummerCarTweaks.Tweaks.GifuGaugeFix
{
    internal class GifuBatteryGauge : MonoBehaviour
    {
        private Quaternion _offRot = Quaternion.Euler(16.7999f, 128.6444f, 333.0755f);
        private Quaternion _onRot = Quaternion.Euler(-5.20007f, 88.6444f, 333.0755f);
        private Quaternion _startRot = Quaternion.Euler(6.79993f, 108.6444f, 333.0755f);
        private Quaternion _runRot = Quaternion.Euler(-15.20007f, 68.6444f, 333.0755f);

        private FsmBool _acc, _starting;
        private Drivetrain _drivetrain;

        void Start()
        {
            var gifu = GameObject.Find("GIFU(750/450psi)");
            var playmaker = gifu.transform.Find("Simulation/Starter").GetPlayMaker("Starter");

            _acc = playmaker.GetVariable<FsmBool>("ACC");
            _starting = playmaker.GetVariable<FsmBool>("Starting");

            _drivetrain = gifu.GetComponent<Drivetrain>();
        }


        void Update()
        {
            transform.localRotation = Quaternion.Lerp(transform.localRotation, Determine(), Time.deltaTime * 4);
        }

        Quaternion Determine()
        {
            if (_drivetrain.rpm > 100) return _runRot;
            if (_starting.Value) return _startRot;
            if (_acc.Value) return _onRot;

            return _offRot;
        }

    }
}
