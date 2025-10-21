using System;
using _RaghuvanshAgarwal.Modules.GameManager;
using TMPro;
using UnityEngine;

namespace _RaghuvanshAgarwal.Modules.UI.Game_Playing {
    public class GamePlayingUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI gamePlayingValue;
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] Gradient colorGradient;
        private void Start() {
            KitchenChaoGameManager.Instance.OnStateChanged += GameManager_OnStateChanged;
            Hide();
        }

        private void GameManager_OnStateChanged(object sender, EventArgs e) {
            if (KitchenChaoGameManager.Instance.IsGamePlaying()) {
                Show();
            }
            else {
                Hide();
            }
        }

        private void Update() {
            gamePlayingValue.text = TimeSpan.FromSeconds(KitchenChaoGameManager.Instance.GamePlayingTimer).ToString(@"hh\:mm\:ss");
            gamePlayingValue.color = colorGradient.Evaluate(KitchenChaoGameManager.Instance.GamePlayingTimerNormalized);
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
