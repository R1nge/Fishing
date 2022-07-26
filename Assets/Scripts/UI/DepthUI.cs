using TMPro;
using UnityEngine;

namespace UI
{
    public class DepthUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI text;

        // https://gamedev.stackexchange.com/questions/171749/frequently-change-textmeshpro-text-without-garbage-allocation
        public void UpdateUI(int value) => text.SetText(value.ToString());
        // ToString() generate garbage because of string.FastAllocateString(32);
        // Can use reflection to change string inside stringbuilder
    }
}