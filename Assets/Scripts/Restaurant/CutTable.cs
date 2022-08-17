using Restaurant.UI;
using UnityEngine;

namespace Restaurant
{
    public class CutTable : MonoBehaviour
    {
        private CutTableUI _ui;
        private FishStatus _fishStatus;

        private void Awake()
        {
            _ui = FindObjectOfType<CutTableUI>();
            _ui.Close();
            _fishStatus = FindObjectOfType<FishStatus>();
        }

        private void OnMouseDown()
        {
            if (_fishStatus.IsChopped)
            {
                Debug.LogWarning("Already chopped", this);
                return;
            }

            _ui.Open();
        }
    }
}