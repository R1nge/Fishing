using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Other
{
    public class SceneController : MonoBehaviour
    {
        [SerializeField] private string[] sceneNames;
        private byte _currentIndex;
        private static readonly int StartAnim = Animator.StringToHash("Start");
        private SwipeController _swipeController;
        private Transition _transition;

        private void Awake()
        {
            _swipeController = FindObjectOfType<SwipeController>();
            _swipeController.OnSwipeLeftEvent += OnSwipeLeft;
            _swipeController.OnSwipeRightEvent += OnSwipeRight;
            _transition = GameObject.FindWithTag("GameManager").GetComponentInChildren<Transition>();
        }

        private void OnSwipeLeft()
        {
            if (_currentIndex < sceneNames.Length - 1)
            {
                _currentIndex++;
                LoadScene();
            }
        }

        private void OnSwipeRight()
        {
            if (_currentIndex > 0)
            {
                _currentIndex--;
                LoadScene();
            }
        }

        private void LoadScene() => StartCoroutine(Load_c(sceneNames[_currentIndex]));

        private IEnumerator Load_c(string title)
        {
            if (string.Equals(title, SceneManager.GetActiveScene().name)) yield break;
            var transitionAnimator = _transition.GetAnimator();
            transitionAnimator.SetTrigger(StartAnim);
            yield return new WaitForSeconds(transitionAnimator.GetCurrentAnimatorClipInfo(0)[0].clip.length);

            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(title);

            while (!asyncLoad.isDone)
            {
                yield return null;
            }
        }

        private void OnDestroy()
        {
            _swipeController.OnSwipeLeftEvent -= OnSwipeLeft;
            _swipeController.OnSwipeRightEvent -= OnSwipeRight;
        }
    }
}