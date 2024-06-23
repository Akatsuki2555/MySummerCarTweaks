using System;
using UnityEngine;

namespace MySummerCarTweaks.Tweaks.ChangeLoadingScreen
{
    internal class LoadingScreen : MonoBehaviour
    {
        internal bool Rainbow;
        internal int Year;
        private TextMesh _textMesh;

        private void Start()
        {
            Rainbow = ChangeLoadingScreenModule.RainbowLoadingScreen.GetValue();
            Year = ChangeLoadingScreenModule.Year.GetValue();

            _textMesh = GetComponent<TextMesh>();

            if (Rainbow) ApplyRainbowText($"NOW LOADING YEAR {Year}...", Time.time * 2);
            else _textMesh.text = $"NOW LOADING YEAR {Year}...";
        }

        private void Update()
        {
            ApplyRainbowText($"NOW LOADING YEAR {Year}...", Time.time * 2);
        }

        private void ApplyRainbowText(string text, float offset)
        {
            _textMesh.text = string.Empty;

            var step = 1.0f / text.Length;
            var currentPosition = offset;

            var sb = new System.Text.StringBuilder();

            foreach (var ch in text)
            {
                var color = GetRainbowColor(currentPosition % 1f);
                sb.Append($"<color={ToHtmlStringRGB(color)}>{ch}</color>");
                currentPosition += step;
            }

            _textMesh.text = sb.ToString();
        }

        private Color GetRainbowColor(float position) => HSVToRGB(position % 1, 1.0f, 1.0f);

        private static string ToHtmlStringRGB(Color color)
        {
            Color32 color32 = color;
            return $"#{color32.r:X2}{color32.g:X2}{color32.b:X2}";
        }

        private Color HSVToRGB(float h, float s, float v)
        {
            if (s == 0)
            {
                return new Color(v, v, v);
            }

            h *= 360; // convert h to degrees
            h /= 60; // sector 0 to 5
            var i = (int)h;
            var f = h - i; // fractional part of h
            var p = v * (1 - s);
            var q = v * (1 - s * f);
            var t = v * (1 - s * (1 - f));
            i %= 6;
            switch (i)
            {
                case 0:
                    return new Color(v, t, p);
                case 1:
                    return new Color(q, v, p);
                case 2:
                    return new Color(p, v, t);
                case 3:
                    return new Color(p, q, v);
                case 4:
                    return new Color(t, p, v);
                default: // case 5:
                    return new Color(v, p, q);
            }
        }
    }
}