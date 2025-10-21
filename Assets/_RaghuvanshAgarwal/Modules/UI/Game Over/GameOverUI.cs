using System;
using _RaghuvanshAgarwal.Modules.Delivery;
using _RaghuvanshAgarwal.Modules.GameManager;
using TMPro;
using UnityEngine;

namespace _RaghuvanshAgarwal.Modules.UI.Game_Over {
    public class GameOverUI : MonoBehaviour {
        [SerializeField] private TextMeshProUGUI orderDeliveredValue;
        [SerializeField] private CanvasGroup canvasGroup;


        private void Start() {
            Hide();
            KitchenChaoGameManager.Instance.OnStateChanged += GameManager_OnStateChanged;
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
