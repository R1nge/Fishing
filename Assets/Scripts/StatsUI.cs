using FishingRod;
using TMPro;
using UnityEngine;

public class StatsUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private FishingRodSo so;

    private void Awake() => so.OnStatsChanged += UpdateUI;

    private void Start() => UpdateUI();

    private void UpdateUI() =>
        text.text = "Speed: " + so.data.speed + " MaxLength: " + so.data.maxLength + " MaxWeight: " + so.data.maxWeight;

    private void OnDestroy() => so.OnStatsChanged -= UpdateUI;
}