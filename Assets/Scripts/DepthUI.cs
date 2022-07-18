using TMPro;
using UnityEngine;

public class DepthUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;

    public void UpdateUI(string value)
    {
        text.text = value;
    }
}