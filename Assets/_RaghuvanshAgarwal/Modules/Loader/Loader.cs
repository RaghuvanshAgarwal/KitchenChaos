using UnityEngine;
using UnityEngine.SceneManagement;

namespace _RaghuvanshAgarwal.Modules.Loader {
    public static class Loader
    {
        public enum Scene {
            MainMenuScene,
            GameScene,
            LoadingScene,
        }

        private static Scene _targetScene;
        
        public static void LoadScene(Scene sceneName) {
            _targetScene = sceneName;
            SceneManager.LoadScene(nameof(Scene.LoadingScene));
        }

        public static void LoaderCallback() {
            SceneManager.LoadScene(_targetScene.ToString());
        }
    }
}
