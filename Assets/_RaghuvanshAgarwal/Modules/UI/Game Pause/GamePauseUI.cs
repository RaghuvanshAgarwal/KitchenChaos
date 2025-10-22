using System;
using _RaghuvanshAgarwal.Modules.GameManager;
using UnityEngine;
using UnityEngine.UI;

namespace _RaghuvanshAgarwal.Modules.UI.Game_Pause {
    public class GamePauseUI : MonoBehaviour
    {
        [SerializeField] private Button resumeButton;
        [SerializeField] private Button mainMenuButton;
        [SerializeField] private CanvasGroup canvasGroup;

        private void Start() {
            KitchenChaoGameManager.Instance.OnStateChanged += OnStateChanged;
            KitchenChaoGameManager.Instance.OnGamePaused += GameManager_OnGamePaused;
            KitchenChaoGameManager.Instance.OnGameResumed += GameManager_OnGameResumed;
            
            resumeButton.onClick.AddListener(KitchenChaoGameManager.Instance.ToggleGamePause);
            mainMenuButton.onClick.AddListener(KitchenChaoGameManager.Instance.GoToMainMenu);
            
            Hide();
        }

        private void GameManager_OnGameResumed(object sender, EventArgs e) {
            Hide();
        }

        private void GameManager_OnGamePaused(object sender, EventArgs e) {
            Show();
        }

        private void OnStateChanged(object sender, EventArgs e) {
            if (KitchenChaoGameManager.Instance.IsGameOver()) {
                KitchenChaoGameManager.Instance.OnGamePaused -= GameManager_OnGamePaused;
                KitchenChaoGameManager.Instance.OnGameResumed -= GameManager_OnGameResumed;
            }
        }

        private void Show() {
            canvasGroup.alpha = 1;
            canvasGroup.blocksRaycasts = true;
            canvasGroup.interactable = true;
        }

        private void Hide() {
            canvasGroup.alpha = 0;
            canvasGroup.blocksRaycasts = false;
            canvasGroup.interactable = false;
        }
    }
}
