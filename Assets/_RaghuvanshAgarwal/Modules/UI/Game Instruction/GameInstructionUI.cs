using System;
using _RaghuvanshAgarwal.Modules.GameManager;
using _RaghuvanshAgarwal.Modules.Player.Scripts;
using TMPro;
using UnityEngine;

namespace _RaghuvanshAgarwal.Modules.UI.Game_Instruction {
    public class GameInstructionUI : MonoBehaviour
    {
        [Header("Key Bindings")] 
        [SerializeField] private TextMeshProUGUI moveUp;
        [SerializeField] private TextMeshProUGUI moveDown;
        [SerializeField] private TextMeshProUGUI moveLeft;
        [SerializeField] private TextMeshProUGUI moveRight;
        [SerializeField] private TextMeshProUGUI interact;
        [SerializeField] private TextMeshProUGUI interactAtl;
        [SerializeField] private TextMeshProUGUI togglePause;
        [SerializeField] private TextMeshProUGUI gamepadInteract;
        [SerializeField] private TextMeshProUGUI gamepadInteractAtl;
        [SerializeField] private TextMeshProUGUI gamepadTogglePause;


        private void Start() {
            UpdateVisuals();
            Show();
            GameInput.Instance.OnBindingRebinded += GameInput_OnnBindingRebinded;
            KitchenChaoGameManager.Instance.OnStateChanged += GameManager_OnStateChange;
        }

        private void OnDestroy() {
            GameInput.Instance.OnBindingRebinded -= GameInput_OnnBindingRebinded;
        }
        
        private void GameManager_OnStateChange(object sender, EventArgs e) {
            if (KitchenChaoGameManager.Instance.IsCountdownToStart()) {
                GameInput.Instance.OnInteractAction -= GameManager_OnStateChange;
                Hide();
            }
        }

        private void GameInput_OnnBindingRebinded(object sender, EventArgs e) {
            UpdateVisuals();
        }

        private void UpdateVisuals() {
            moveUp.text = GameInput.Instance.GetBindingString(GameInput.Binding.MoveUp);
            moveDown.text = GameInput.Instance.GetBindingString(GameInput.Binding.MoveDown);
            moveLeft.text = GameInput.Instance.GetBindingString(GameInput.Binding.MoveLeft);
            moveRight.text = GameInput.Instance.GetBindingString(GameInput.Binding.MoveRight);
            interact.text = GameInput.Instance.GetBindingString(GameInput.Binding.Interact);
            interactAtl.text = GameInput.Instance.GetBindingString(GameInput.Binding.InteractAlternate);
            togglePause.text = GameInput.Instance.GetBindingString(GameInput.Binding.Pause);
            gamepadInteract.text = GameInput.Instance.GetBindingString(GameInput.Binding.GamepadInteract);
            gamepadInteractAtl.text = GameInput.Instance.GetBindingString(GameInput.Binding.GamepadInteractAlternate);
            gamepadTogglePause.text = GameInput.Instance.GetBindingString(GameInput.Binding.GamepadPause);
        }

        private void Show() {
            gameObject.SetActive(true);
        }

        private void Hide() {
            gameObject.SetActive(false);
        }
        
    }
}
