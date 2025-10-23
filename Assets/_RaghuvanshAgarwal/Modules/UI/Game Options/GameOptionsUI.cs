using System;
using _RaghuvanshAgarwal.Modules.Audio.Music;
using _RaghuvanshAgarwal.Modules.Audio.Sound;
using _RaghuvanshAgarwal.Modules.GameManager;
using _RaghuvanshAgarwal.Modules.Player.Scripts;
using _RaghuvanshAgarwal.Modules.UI.Game_Pause;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _RaghuvanshAgarwal.Modules.UI.Game_Options {
    public class GameOptionsUI : MonoBehaviour
    {
        public event EventHandler OnRebindConflict;
        [Serializable]
        struct ButtonTextData {
            public Button button;
            public TextMeshProUGUI text;
            public GameObject rebindText;
        }
        [SerializeField] private Button soundButton;
        [SerializeField] private Button musicButton;
        [SerializeField] private Button backButton;
        [SerializeField] private TextMeshProUGUI musicText;
        [SerializeField] private TextMeshProUGUI soundText;
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private GamePauseUI  gamePauseUI;

        [Header("Key Bindings")] 
        [SerializeField] private ButtonTextData moveUp;
        [SerializeField] private ButtonTextData moveDown;
        [SerializeField] private ButtonTextData moveLeft;
        [SerializeField] private ButtonTextData moveRight;
        [SerializeField] private ButtonTextData interact;
        [SerializeField] private ButtonTextData interactAtl;
        [SerializeField] private ButtonTextData togglePause;
        

        private void Awake() {
            HideRebindText();
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
            
            moveUp.button.onClick.AddListener(() => {
                ShowRebindText(ref moveUp);
                RebindBinding(GameInput.Binding.MoveUp);
            });
            
            moveDown.button.onClick.AddListener(() => {
                ShowRebindText(ref moveDown);
                RebindBinding(GameInput.Binding.MoveDown);
            });
            
            moveLeft.button.onClick.AddListener(() => {
                ShowRebindText(ref moveLeft);
                RebindBinding(GameInput.Binding.MoveLeft);
            });
            
            moveRight.button.onClick.AddListener(() => {
                ShowRebindText(ref moveRight);
                RebindBinding(GameInput.Binding.MoveRight);
            });
            
            interact.button.onClick.AddListener(() => {
                ShowRebindText(ref interact);
                RebindBinding(GameInput.Binding.Interact);
            });
            
            interactAtl.button.onClick.AddListener(() => {
                ShowRebindText(ref interactAtl);
                RebindBinding(GameInput.Binding.InteractAlternate);
            });
            
            togglePause.button.onClick.AddListener(() => {
                ShowRebindText(ref togglePause);
                RebindBinding(GameInput.Binding.Pause);
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
        
        private void UpdateBindingText() {
            interact.text.text = GameInput.Instance.GetBindingString(GameInput.Binding.Interact);
            interactAtl.text.text = GameInput.Instance.GetBindingString(GameInput.Binding.InteractAlternate);
            togglePause.text.text = GameInput.Instance.GetBindingString(GameInput.Binding.Pause);
            moveUp.text.text = GameInput.Instance.GetBindingString(GameInput.Binding.MoveUp);
            moveDown.text.text = GameInput.Instance.GetBindingString(GameInput.Binding.MoveDown);
            moveLeft.text.text = GameInput.Instance.GetBindingString(GameInput.Binding.MoveLeft);
            moveRight.text.text = GameInput.Instance.GetBindingString(GameInput.Binding.MoveRight);
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
            UpdateBindingText();
        }

        

        private void Hide() {
            canvasGroup.alpha = 0;
            canvasGroup.blocksRaycasts = false;
            canvasGroup.interactable = false;
        }

        private void RebindBinding(GameInput.Binding binding) {
            GameInput.Instance.RebindInput(binding, OnRebindComplete, () => {
                OnRebindConflict?.Invoke(this, EventArgs.Empty);
            });
        }
        
        private void OnRebindComplete() {
            UpdateBindingText();
            HideRebindText();
        }

        private void ShowRebindText(ref ButtonTextData buttonTextData) {
            buttonTextData.rebindText.SetActive(true);
        }

        private void HideRebindText() {
            moveUp.rebindText.SetActive(false);
            moveDown.rebindText.SetActive(false);
            moveLeft.rebindText.SetActive(false);
            moveRight.rebindText.SetActive(false);
            interact.rebindText.SetActive(false);
            interactAtl.rebindText.SetActive(false);
            togglePause.rebindText.SetActive(false);
        }
    }
}
