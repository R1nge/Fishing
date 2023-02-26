using Other;
using UnityEngine;
using Zenject;

namespace Restaurant
{
    public class Load : MonoBehaviour
    {
        private SceneController _sceneController;

        [Inject]
        public void Constructor(SceneController sceneController)
        {
            _sceneController = sceneController;
        }

        public void LoadScene(string sceneName)
        {
            _sceneController.LoadSceneByName(sceneName);
        }
    }
}