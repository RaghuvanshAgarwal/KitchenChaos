using System;
using _RaghuvanshAgarwal.Modules.GameManager;
using TMPro;
using UnityEngine;

namespace _RaghuvanshAgarwal.Modules.UI.Game_Start_Countdown {
    public class GameStartCountdownUI : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI countdownText;

        private void Start() {
            KitchenChaoGameManager.Instance.OnStateChanged += GameManager_OnStateChanged;
            Hide();
        }

        private void OnDestroy() {
            KitchenChaoGameManager.Instance.OnStateChanged -= GameManager_OnStateChanged;
        }

        private void GameManager_OnStateChanged(object sender, EventArgs e) {
            if (KitchenChaoGameManager.Instance.IsCountdownToStart()) {
                Show();
            }
            else {
                Hide();
            }
        }

        private void Update() {
            countdownText.text = Mathf.CeilToInt(KitchenChaoGameManager.Instance.CountdownToStart).ToString();
        }
        
        private void Hide() {
            gameObject.SetActive(false);
        }

        private void Show() {
            gameObject.SetActive(true);
        }
    }
}
