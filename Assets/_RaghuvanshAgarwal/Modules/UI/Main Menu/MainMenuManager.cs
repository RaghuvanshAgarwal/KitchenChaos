using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace _RaghuvanshAgarwal.Modules.UI.Main_Menu {
    public class MainMenuManager : MonoBehaviour
    {
        [SerializeField] Button startGameButton;
        [SerializeField] Button quitGameButton;

        private void Awake() {
            startGameButton.onClick.AddListener(() => {
                Loader.Loader.LoadScene(Loader.Loader.Scene.GameScene);
            });
            
            quitGameButton.onClick.AddListener(Application.Quit);
        }
    }
}
