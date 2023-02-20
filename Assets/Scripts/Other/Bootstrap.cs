using UnityEngine;
using UnityEngine.SceneManagement;

namespace Other
{
    public class Bootstrap : MonoBehaviour
    {
        private void Awake() => SceneManager.LoadScene("Scenes/Fishing");
    }
}