using System.Collections;
using Other;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : Singleton<SceneController>
{
    [SerializeField] private string[] sceneNames;
    private byte _currentIndex;
    private static readonly int StartAnim = Animator.StringToHash("Start");

    private void Start()
    {
        SwipeController.Instance.OnSwipeLeftEvent += OnSwipeLeft;
        SwipeController.Instance.OnSwipeRightEvent += OnSwipeRight;
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
        var transitionAnimator = Transition.Instance.GetAnimator();
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
        if (SwipeController.Instance)
        {
            SwipeController.Instance.OnSwipeLeftEvent -= OnSwipeLeft;
            SwipeController.Instance.OnSwipeRightEvent -= OnSwipeRight;
        }
    }
}