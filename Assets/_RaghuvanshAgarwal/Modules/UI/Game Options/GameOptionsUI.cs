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
        }
        [SerializeField] private Button soundButton;
        [SerializeField] private Button musicButton;
        [SerializeField] private Button backButton;
        [SerializeField] private TextMeshProUGUI musicText;
        [SerializeField] private TextMeshProUGUI soundText;
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private GamePauseUI  gamePauseUI;
        [SerializeField] private GameObject rebindText;

        [Header("Key Bindings")] 
        [SerializeField] private ButtonTextData moveUp;
        [SerializeField] private ButtonTextData moveDown;
        [SerializeField] private ButtonTextData moveLeft;
        [SerializeField] private ButtonTextData moveRight;
        [SerializeField] private ButtonTextData interact;
        [SerializeField] private ButtonTextData interactAtl;
        [SerializeField] private ButtonTextData togglePause;
        [SerializeField] private ButtonTextData gamepadInteract;
        [SerializeField] private ButtonTextData gamepadInteractAtl;
        [SerializeField] private ButtonTextData gamepadTogglePause;
        

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
                ShowRebindText();
                RebindBinding(GameInput.Binding.MoveUp);
            });
            
            moveDown.button.onClick.AddListener(() => {
                ShowRebindText();
                RebindBinding(GameInput.Binding.MoveDown);
            });
            
            moveLeft.button.onClick.AddListener(() => {
                ShowRebindText();
                RebindBinding(GameInput.Binding.MoveLeft);
            });
            
            moveRight.button.onClick.AddListener(() => {
                ShowRebindText();
                RebindBinding(GameInput.Binding.MoveRight);
            });
            
            interact.button.onClick.AddListener(() => {
                ShowRebindText();
                RebindBinding(GameInput.Binding.Interact);
            });
            
            interactAtl.button.onClick.AddListener(() => {
                ShowRebindText();
                RebindBinding(GameInput.Binding.InteractAlternate);
            });
            
            togglePause.button.onClick.AddListener(() => {
                ShowRebindText();
                RebindBinding(GameInput.Binding.Pause);
            });
            
            gamepadInteract.button.onClick.AddListener(() => {
                ShowRebindText();
                RebindBinding(GameInput.Binding.GamepadInteract);
            });
            
            gamepadInteractAtl.button.onClick.AddListener(() => {
                ShowRebindText();
                RebindBinding(GameInput.Binding.GamepadInteractAlternate);
            });
            
            gamepadTogglePause.button.onClick.AddListener(() => {
                ShowRebindText();
                RebindBinding(GameInput.Binding.GamepadPause);
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
            gamepadInteract.text.text = GameInput.Instance.GetBindingString(GameInput.Binding.GamepadInteract);
            gamepadInteractAtl.text.text = GameInput.Instance.GetBindingString(GameInput.Binding.GamepadInteractAlternate);
            gamepadTogglePause.text.text = GameInput.Instance.GetBindingString(GameInput.Binding.GamepadPause);
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
            soundButton.Select();
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

        private void ShowRebindText() {
            rebindText.SetActive(true);
        }

        private void HideRebindText() {
            rebindText.SetActive(false);
        }
    }
}
