using System;
using _RaghuvanshAgarwal.Modules.Delivery;
using _RaghuvanshAgarwal.Modules.GameManager;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _RaghuvanshAgarwal.Modules.UI.Game_Over {
    public class GameOverUI : MonoBehaviour {
        [SerializeField] private TextMeshProUGUI orderDeliveredValue;
        [SerializeField] private CanvasGroup canvasGroup;

        [SerializeField] private Button playAgainButton;
        [SerializeField] private Button mainMenuButton;

        private void Start() {
            Hide();
            KitchenChaoGameManager.Instance.OnStateChanged += GameManager_OnStateChanged;
            
            playAgainButton.onClick.AddListener(() => {
                Loader.Loader.LoadScene(Loader.Loader.Scene.GameScene);
            });
            
            mainMenuButton.onClick.AddListener(() => {
                Loader.Loader.LoadScene(Loader.Loader.Scene.MainMenuScene);
            });
        }

        private void GameManager_OnStateChanged(object sender, EventArgs e) {
            if (KitchenChaoGameManager.Instance.IsGameOver()) {
                orderDeliveredValue.text = DeliveryManager.Instance.CorrectOrderCount.ToString();
                Show();
            }
        }

        void Show() {
            canvasGroup.alpha = 1;
            canvasGroup.blocksRaycasts = true;
            canvasGroup.interactable = true;
        }

        void Hide() {
            canvasGroup.alpha = 0;
            canvasGroup.blocksRaycasts = false;
            canvasGroup.interactable = false;
        }
    }
}
