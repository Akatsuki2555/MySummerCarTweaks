using System.Linq;
using HutongGames.PlayMaker;
using UnityEngine;

namespace MySummerCarTweaks.Tweaks.PlayerCantDieFromUrine
{
    internal class PlayerCantDieFromUrineModule : MscTweak
    {
        internal override string Name => "Player Can't Die From Urine Like IRL";

        private PlayMakerFSM _pissLogic;
        private FsmFloat _playerUrine;

        internal override void MSC_OnLoad(MySummerCarTweaksMod mod)
        {        
            _playerUrine = FsmVariables.GlobalVariables.FindFsmFloat("PlayerUrine");
            _pissLogic = GameObject.Find("PLAYER/Pivot/AnimPivot/Camera/FPSCamera/Piss").GetComponents<PlayMakerFSM>().First(x => x.FsmName == "Logic");
            _pissLogic.SendEvent("FINISHED");
        }

        internal override void MSC_OnUpdate(MySummerCarTweaksMod mod)
        {
            if (_playerUrine.Value > 100f && _pissLogic.ActiveStateName == "Start urinate")
            {
                _pissLogic.SendEvent("FINISHED");
            }
        }
    }
}