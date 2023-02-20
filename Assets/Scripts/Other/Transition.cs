using UnityEngine;

namespace Other
{
    public class Transition : Singleton<Transition>
    {
        [SerializeField] private Animator animator;

        public Animator GetAnimator() => animator;
    }
}