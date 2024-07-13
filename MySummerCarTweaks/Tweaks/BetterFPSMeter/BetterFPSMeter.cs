using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MySummerCarTweaks.Tweaks.BetterFPSMeter
{
    public class BetterFPSMeter : MonoBehaviour
    {
        private float _time = 0.9f;
        private float _lastDiff;
        private float _current;
        private List<float> _values = new List<float>();
        private TextMesh _text;
        private TextMesh _shadowText;

        internal float Frequency = 4;
        internal bool ShowDifference = true;
        internal bool Colors = true;

        private void Start()
        {
            foreach (var fsm in gameObject.GetComponents<PlayMakerFSM>())
                fsm.enabled = false;
            _text = GetComponent<TextMesh>();
            _shadowText = transform.GetChild(0).GetComponent<TextMesh>();
        }

        private void Update()
        {
            _time += Time.deltaTime;
            _values.Add(1f / Time.deltaTime);
            if (_time < 1 / Frequency) return;
            _time = 0;

            var newFPS = CalculateAverage(_values);
            _lastDiff = newFPS - _current;
            _current = newFPS;

            var finalText = $"{_current:0}";
            var finalTextNoColor = $"{_current:0}";

            if (_current > 60)
                finalText = $"<color=\"lime\">{finalText}</color>";
            else if (_current < 20)
                finalText = $"<color=\"red\">{finalText}</color>";

            if (ShowDifference)
            {
                if (_lastDiff > 0)
                {
                    finalText += $" <color=\"lime\">(+ {_lastDiff:0})</color>";
                    finalTextNoColor += $" (+ {_lastDiff:0})";
                }
                if (_lastDiff < 0)
                {
                    finalText += $" <color=\"red\">(- {Math.Abs(_lastDiff):0})</color>";
                    finalTextNoColor += $" (- {Math.Abs(_lastDiff):0})";
                }
            }

            _text.text = Colors ? finalText : finalTextNoColor;
            _shadowText.text = finalTextNoColor;

            _values.Clear();
        }

        private static float CalculateAverage(ICollection<float> values) => values.Sum(x => x) / values.Count;
    }
}
