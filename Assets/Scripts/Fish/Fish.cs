using UnityEngine;

namespace Fish
{
    public class Fish : MonoBehaviour
    {
        [SerializeField] private FishStats fishStats;

        private void Start() => print(fishStats.TotalPrice);
    }
}