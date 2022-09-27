using System.Collections;
using Cysharp.Text;
using TMPro;
using UnityEngine;

namespace Restaurant
{
    public class Timer : GenericSingleton<Timer>
    {
        [SerializeField] private TextMeshProUGUI text;
        private byte _hours;
        private byte _minutes;
        private const byte MINTIME = 0;
        private const byte MAXTIME = 24;
        private const float DELAY = 0.1f;
        private YieldInstruction _yield;

        public override void Awake()
        {
            base.Awake();
            _yield = new WaitForSeconds(DELAY);
            StartCoroutine(Timer_c());
        }

        private IEnumerator Timer_c()
        {
            while (enabled)
            {
                yield return _yield;

                _minutes++;
                if (_minutes >= 60)
                {
                    _hours++;
                    if (_hours >= MAXTIME)
                    {
                        _hours = MINTIME;
                    }

                    _minutes = 0;
                }

                UpdateUI();
            }
        }

        public void SetTime(byte hours, byte minutes)
        {
            _hours = hours;
            _minutes = minutes;
            UpdateUI();
        }

        private void UpdateUI()
        {
            if (_hours >= 10)
            {
                text.SetTextFormat(_minutes >= 10 ? "{0}:{1}" : "{0}:0{1}", _hours, _minutes);
            }
            else
            {
                text.SetTextFormat(_minutes >= 10 ? "0{0}:{1}" : "0{0}:0{1}", _hours, _minutes);
            }
        }
    }
}