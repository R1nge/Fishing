using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Other
{
    public class LoadScene : MonoBehaviour
    {
        [SerializeField] private string levelName;
        [SerializeField] private Animator animator;
        private static readonly int StartAnim = Animator.StringToHash("Start");

        public void LoadWithString() => StartCoroutine(Load_c(levelName));

        public void Load(string title) => StartCoroutine(Load_c(title));

        private IEnumerator Load_c(string title)
        {
            if (string.Equals(levelName, SceneManager.GetActiveScene().name)) yield break;
            animator.SetTrigger(StartAnim);
            yield return new WaitForSeconds(animator.GetCurrentAnimatorClipInfo(0)[0].clip.length);

            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(title);

            while (!asyncLoad.isDone)
            {
                yield return null;
            }
        }
    }
}