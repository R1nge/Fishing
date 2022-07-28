using System.IO;
using FishingRod;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    [SerializeField] private FishingRodSo so;

    private void Awake()
    {
        if (File.Exists(Application.persistentDataPath + "/Rod.json"))
        {
            LoadFromJson();
        }
        else
        {
            so.data = new RodData
            {
                rodSprite = "global[Fish1]",
                hookSprite = "FishingHook",
                biteSprite = "",
                maxWeight = 15,
                maxLength = 25,
                verticalSpeed = 10,
                horizontalSpeed = 3000
            };
        }

        so.OnSpriteChanged += SaveToJson;
    }

    private void SaveToJson() =>
        File.WriteAllText(Application.persistentDataPath + "/Rod.json", JsonUtility.ToJson(so.data));

    private void LoadFromJson()
    {
        string fileContents = File.ReadAllText(Application.persistentDataPath + "/Rod.json");
        so.data = JsonUtility.FromJson<RodData>(fileContents);
    }

    private void OnDestroy() => so.OnSpriteChanged -= SaveToJson;
}