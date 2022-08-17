using UnityEngine;

namespace Restaurant
{
    public class Oven : MonoBehaviour
    {
        private FishStatus _fishStatus;

        private void Awake() => _fishStatus = FindObjectOfType<FishStatus>();

        private void OnMouseDown()
        {
            if (_fishStatus.IsCooked)
            {
                Debug.LogWarning("Already cooked", this);
                return;
            }

            _fishStatus.Cook();
        }
    }
}