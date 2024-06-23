using HutongGames.PlayMaker;
using MSCLoader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace MySummerCarTweaks.Tweaks.MoneyChanges
{
    internal class MoneyChangesScript : MonoBehaviour
    {
        private TextMesh _text;
        private float _money;
        private float _moneyTimer;
        private float _moneyChange;

        void Start()
        {
            _text = GetComponent<TextMesh>();
            _money = FsmVariables.GlobalVariables.GetFsmFloat("PlayerMoney").Value;
        }

        private bool _thrownError;

        void Update()
        {
            try
            {
                if (FsmVariables.GlobalVariables.GetFsmFloat("PlayerMoney").Value != _money)
                {
                    _moneyChange = FsmVariables.GlobalVariables.GetFsmFloat("PlayerMoney").Value - _money;
                    _money = FsmVariables.GlobalVariables.GetFsmFloat("PlayerMoney").Value;
                    _moneyTimer = 5;
                }

                if (_moneyTimer < 0)
                {
                    _text.text = "";
                    return;
                }

                if (_moneyChange < 0) _text.text = $"<color=red>{_moneyChange:0}</color>";
                else _text.text = $"<color=green>+{_moneyChange:0}</color>";

                _moneyTimer -= Time.deltaTime;
            }
            catch (Exception e)
            {
                if (_thrownError) return;
                ModConsole.LogError("Module Money Changer threw an error");
                ModConsole.LogError(e.StackTrace.ToString());
                _thrownError = true;
            }
        }
    }
}
