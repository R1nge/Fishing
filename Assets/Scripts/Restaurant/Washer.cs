using Restaurant.UI;
using UnityEngine;

namespace Restaurant
{
    public class Washer : MonoBehaviour
    {
        //private FishStatus _fishStatus;
        private WasherUI _userInterface;

        private void Awake()
        {
            //_fishStatus = FindObjectOfType<FishStatus>();
            _userInterface = FindObjectOfType<WasherUI>();
            _userInterface.Close();
        }

        private void OnMouseDown()
        {
            // if (_fishStatus.IsWashed)
            // {
            //     Debug.LogWarning("Already washed", this);
            //     return;
            // }

            _userInterface.Open();
        }
    }
}