using System;
using _RaghuvanshAgarwal.Modules.Audio.Music;
using _RaghuvanshAgarwal.Modules.Audio.Sound;
using _RaghuvanshAgarwal.Modules.GameManager;
using _RaghuvanshAgarwal.Modules.UI.Game_Pause;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _RaghuvanshAgarwal.Modules.UI.Game_Options {
    public class GameOptionsUI : MonoBehaviour
    {
        [SerializeField] private Button soundButton;
        [SerializeField] private Button musicButton;
        [SerializeField] private Button backButton;
        [SerializeField] private TextMeshProUGUI musicText;
        [SerializeField] private TextMeshProUGUI soundText;
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private GamePauseUI  gamePauseUI;

        private void Awake() {
            soundButton.onClick.AddListener(() => {
                SoundManager.Instance.ChangeVolume();
                UpdateSoundText();
            });
            
            musicButton.onClick.AddListener(() => {
                MusicManager.Instance.ChangeVolume();
                UpdateMusicText();
            });
            
            backButton.onClick.AddListener(() => {
                Hide();
                gamePauseUI.Show();
            });
        }

        private void UpdateSoundText() {
            int volume = Mathf.CeilToInt(SoundManager.Instance.SoundVolume * 10);
            soundText.text = $"Sound: {volume}";
        }
        
        private void UpdateMusicText() {
            int volume = Mathf.CeilToInt(MusicManager.Instance.MusicVolume * 10);
            musicText.text = $"Music: {volume}";
        }

        private void Start() {
            Hide();
            
            KitchenChaoGameManager.Instance.OnStateChanged += OnStateChanged;
            KitchenChaoGameManager.Instance.OnGameResumed += GameManager_OnGameResumed;
        }
        
        private void OnDestroy() {
            KitchenChaoGameManager.Instance.OnStateChanged -= OnStateChanged;
        }

        private void GameManager_OnGameResumed(object sender, EventArgs e) {
            Hide();
        }
        

        private void OnStateChanged(object sender, EventArgs e) {
            if (KitchenChaoGameManager.Instance.IsGameOver()) {
                KitchenChaoGameManager.Instance.OnGameResumed -= GameManager_OnGameResumed;
            }
        }

        public void Show() {
            canvasGroup.alpha = 1;
            canvasGroup.blocksRaycasts = true;
            canvasGroup.interactable = true;
            UpdateSoundText();
            UpdateMusicText();
        }

        private void Hide() {
            canvasGroup.alpha = 0;
            canvasGroup.blocksRaycasts = false;
            canvasGroup.interactable = false;
        }
    }
}
