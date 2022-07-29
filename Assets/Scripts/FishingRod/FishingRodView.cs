using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace FishingRod
{
    public class FishingRodView : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer rod, hook, bite;
        [SerializeField] private FishingRodSo so;
        private List<string> _keys;
        AsyncOperationHandle<IList<Sprite>> _loadHandle;

        private void Awake() => so.OnSpriteChanged += UpdateSprites;

        private void Start() => UpdateSprites();

        private void UpdateSprites()
        {
            _keys = new List<string>
                {so.data.hookSprite, so.data.rodSprite}; //, $"{rodSo.data.biteSprite}"};
            Addressables.LoadAssetsAsync<Sprite>(
                _keys, null, Addressables.MergeMode.Union, false
            ).Completed += UpdateUI;
        }

        private void UpdateUI(AsyncOperationHandle<IList<Sprite>> asyncOperationHandle)
        {
            hook.sprite = asyncOperationHandle.Result[0];
            rod.sprite = asyncOperationHandle.Result[1];
            //bite.sprite = asyncOperationHandle.Result[2];
        }
        
        private void OnDestroy() => so.OnSpriteChanged -= UpdateSprites;
    }
}