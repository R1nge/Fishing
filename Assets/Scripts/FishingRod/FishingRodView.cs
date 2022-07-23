using System.IO;
using UnityEngine;

namespace FishingRod
{
    public class FishingRodView : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer rod, hook, bite;
        [SerializeField] private FishingRodSo rodSo;

        private void Awake()
        {
            rodSo.OnSpriteChanged += UpdateSprites;
            if (File.Exists(Application.persistentDataPath + "/Rod.json"))
                LoadFromJson();
            else
            {
                rodSo.data = new RodData
                {
                    hookImage = "FishingHook",
                    hookSprite = "FishingHook",
                    rodImage = "global",
                    rodSprite = "Fish1",
                    verticalSpeed = 15,
                    horizontalSpeed = 25,
                    maxLength = 15,
                    maxWeight = 15
                };
                UpdateSprites();
            }
        }

        private void UpdateSprites()
        {
            rod.sprite = Load($"{rodSo.data.rodImage}", $"{rodSo.data.rodSprite}");
            hook.sprite = Load($"{rodSo.data.hookImage}", $"{rodSo.data.hookSprite}");
            bite.sprite = Load($"{rodSo.data.biteImage}", $"{rodSo.data.biteSprite}");
            SaveToJson();
        }

        private Sprite Load(string imageName, string spriteName)
        {
            Sprite[] all = Resources.LoadAll<Sprite>(imageName);

            foreach (var sprite in all)
            {
                if (sprite.name == spriteName)
                {
                    return sprite;
                }
            }

            Debug.LogWarning("Sprite not found");
            return null;
        }

        private void SaveToJson() =>
            File.WriteAllText(Application.persistentDataPath + "/Rod.json", JsonUtility.ToJson(rodSo.data));

        private void LoadFromJson()
        {
            string fileContents = File.ReadAllText(Application.persistentDataPath + "/Rod.json");
            rodSo.data = JsonUtility.FromJson<RodData>(fileContents);
            UpdateSprites();
        }

        private void OnDestroy() => rodSo.OnSpriteChanged -= UpdateSprites;
    }
}