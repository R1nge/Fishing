using System;
using System.Collections;
using UnityEngine;

namespace Restaurant
{
    public class Timer : GenericSingleton<Timer>
    {
        public int Hours { get; private set; }
        public int Minutes { get; private set; }
        private const int MINTIME = 0;
        private const int MAXTIME = 24;
        public event Action<int, int> OnTimeUpdated;

        public override void Awake()
        {
            base.Awake();
            StartCoroutine(Timer_c());
            OnTimeUpdated?.Invoke(Hours, Minutes);
        }

        private void Start() => OnTimeUpdated?.Invoke(Hours, Minutes);

        private IEnumerator Timer_c()
        {
            while (enabled)
            {
                yield return new WaitForSeconds(.4f);

                Minutes++;
                if (Minutes >= 60)
                {
                    Hours++;
                    if (Hours >= MAXTIME)
                    {
                        Hours = MINTIME;
                    }

                    Minutes = 0;
                }

                OnTimeUpdated?.Invoke(Hours, Minutes);
            }
        }

        public void SetTime(int hours, int minutes)
        {
            Hours = hours;
            Minutes = minutes;
            OnTimeUpdated?.Invoke(Hours, Minutes);
        }
    }
}