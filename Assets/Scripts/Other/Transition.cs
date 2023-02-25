using UnityEngine;

namespace Other
{
    public class Transition : MonoBehaviour
    {
        [SerializeField] private Animator animator;

        public Animator GetAnimator() => animator;
    }
}