﻿using UnityEngine;

namespace Other
{
    public class FpsManager : MonoBehaviour
    {
        [SerializeField] private int fps;

        private void Awake() => Application.targetFrameRate = fps;
    }
}