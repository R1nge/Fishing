using UnityEngine;

namespace Restaurant
{
    [CreateAssetMenu(fileName = "CustomersSprites", menuName = "Restaurant/CustomersSprites")]
    public class CustomerSprites : ScriptableObject
    {
        public Sprite[] sprites;

        public Sprite GetRandom() => sprites[Random.Range(0, sprites.Length)];
    }
}