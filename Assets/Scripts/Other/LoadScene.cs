using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Other
{
    public class LoadScene : MonoBehaviour
    {
        [SerializeField] private string levelName;
        private static readonly int StartAnim = Animator.StringToHash("Start");

        public void LoadWithString() => StartCoroutine(Load_c(levelName));

        public void Load(string title) => StartCoroutine(Load_c(title));

        private IEnumerator Load_c(string title)
        {
            if (string.Equals(levelName, SceneManager.GetActiveScene().name)) yield break;
            var transitionAnimator = Transition.Instance.GetAnimator();
            transitionAnimator.SetTrigger(StartAnim);
            yield return new WaitForSeconds(transitionAnimator.GetCurrentAnimatorClipInfo(0)[0].clip.length);

            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(title);

            while (!asyncLoad.isDone)
            {
                yield return null;
            }
        }
    }
}