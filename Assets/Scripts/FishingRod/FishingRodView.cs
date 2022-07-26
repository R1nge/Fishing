using System.IO;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

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
            {
                LoadFromJson();
            }
            else
            {
                rodSo.data = new RodData
                {
                    rodSprite = "global[Fish1]",
                    hookSprite = "FishingHook",
                    biteSprite = "",
                    horizontalSpeed = 1500,
                    maxLength = 30,
                    maxWeight = 20,
                    verticalSpeed = 200
                };
                UpdateSprites();
            }

            UpdateSprites();
        }


        private void Rod(AsyncOperationHandle<Sprite> obj)
        {
            switch (obj.Status)
            {
                case AsyncOperationStatus.Succeeded:
                    rod.sprite = obj.Result;
                    break;
            }
        }

        private void Hook(AsyncOperationHandle<Sprite> obj)
        {
            switch (obj.Status)
            {
                case AsyncOperationStatus.Succeeded:
                    hook.sprite = obj.Result;
                    break;
            }
        }

        private void Bite(AsyncOperationHandle<Sprite> obj)
        {
            switch (obj.Status)
            {
                case AsyncOperationStatus.Succeeded:
                    bite.sprite = obj.Result;
                    break;
            }
        }


        private void UpdateSprites()
        {
            Addressables.LoadAssetAsync<Sprite>($"{rodSo.data.hookSprite}").Completed += Hook;
            Addressables.LoadAssetAsync<Sprite>($"{rodSo.data.rodSprite}").Completed += Rod;
            Addressables.LoadAssetAsync<Sprite>($"{rodSo.data.biteSprite}").Completed += Bite;

            SaveToJson();
        }

        // private Sprite Load(string imageName, string spriteName)
        // {
        //     Sprite[] all = Resources.LoadAll<Sprite>(imageName);
        //
        //     foreach (var sprite in all)
        //     {
        //         if (sprite.name == spriteName)
        //         {
        //             return sprite;
        //         }
        //     }
        //
        //     //Debug.LogWarning("Sprite not found");
        //     return null;
        // }

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