using System;
using System.Globalization;
using TMPro;
using UnityEngine;

public class FpsCounter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;

    private void Update()
    {
        text.text = (1.0f / Time.smoothDeltaTime).ToString(CultureInfo.InvariantCulture);
    }
}