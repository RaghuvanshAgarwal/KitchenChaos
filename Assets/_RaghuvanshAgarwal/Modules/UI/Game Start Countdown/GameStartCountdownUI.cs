using System;
using _RaghuvanshAgarwal.Modules.Audio.Sound;
using _RaghuvanshAgarwal.Modules.GameManager;
using TMPro;
using UnityEngine;

namespace _RaghuvanshAgarwal.Modules.UI.Game_Start_Countdown {
    public class GameStartCountdownUI : MonoBehaviour
    {
        private static readonly int NumberPopup = Animator.StringToHash("NumberPopup");
        [SerializeField] TextMeshProUGUI countdownText;
        
        private Animator _animator;
        private int _currentCountdown = 0;

        private void Awake() {
            _animator =  GetComponent<Animator>();
        }

        private void Start() {
            KitchenChaoGameManager.Instance.OnStateChanged += GameManager_OnStateChanged;
            Hide();
        }

        private void OnDestroy() {
            KitchenChaoGameManager.Instance.OnStateChanged -= GameManager_OnStateChanged;
        }

        private void GameManager_OnStateChanged(object sender, EventArgs e) {
            if (KitchenChaoGameManager.Instance.IsCountdownToStart()) {
                KitchenChaoGameManager.Instance.OnGamePaused += GameManager_OnGamePaused;
                KitchenChaoGameManager.Instance.OnGameResumed += GameManager_OnGameResumed;
                Show();
            }
            else {
                KitchenChaoGameManager.Instance.OnGamePaused -= GameManager_OnGamePaused;
                KitchenChaoGameManager.Instance.OnGameResumed -= GameManager_OnGameResumed;
                Hide();
            }
        }
        
        private void GameManager_OnGameResumed(object sender, EventArgs e) {
            Show();
        }

        private void GameManager_OnGamePaused(object sender, EventArgs e) {
            Hide();
        }
        
        private void Update() {
            int num = Mathf.CeilToInt(KitchenChaoGameManager.Instance.CountdownToStart);
            if (num != _currentCountdown) {
                _currentCountdown = num;
                _animator.SetTrigger(NumberPopup);
                SoundManager.Instance.PlayCountdownSounds();
            }
            countdownText.text = _currentCountdown.ToString();
        }
        
        private void Hide() {
            gameObject.SetActive(false);
        }

        private void Show() {
            gameObject.SetActive(true);
        }
    }
}
