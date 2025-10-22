using System;
using _RaghuvanshAgarwal.Modules.Player.Input;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _RaghuvanshAgarwal.Modules.Player.Scripts {
	public class GameInput: MonoBehaviour {
		public static GameInput Instance {get; private set;}

		public event EventHandler OnInteractAction;
		public event EventHandler OnInteractAlternateAction;
		public event EventHandler OnPauseAction;
		
		private PlayerInputActions _playerInput;
		private void Awake() {
			Instance = this;
			_playerInput= new PlayerInputActions();
			_playerInput.Player.Enable();
		}

		private void Start() {
			_playerInput.Player.Interact.performed += InteractOnPerformed;
			_playerInput.Player.InteractAlternate.performed += InteractAlternateOnPerformed;
			_playerInput.Player.Pause.performed += PauseOnPerformed;
		}
		
		private void OnDestroy() {
			_playerInput.Player.Interact.performed -= InteractOnPerformed;
			_playerInput.Player.InteractAlternate.performed -= InteractAlternateOnPerformed;
			_playerInput.Player.Pause.performed -= PauseOnPerformed;
			
			_playerInput.Player.Disable();
		}

		private void InteractOnPerformed(InputAction.CallbackContext obj) {
			OnInteractAction?.Invoke(this, EventArgs.Empty);
		}
		
		private void InteractAlternateOnPerformed(InputAction.CallbackContext obj) {
			OnInteractAlternateAction?.Invoke(this, EventArgs.Empty);
		}
		
		private void PauseOnPerformed(InputAction.CallbackContext obj) {
			OnPauseAction?.Invoke(this, EventArgs.Empty);
		}

		public Vector2 GetMovementVectorNormalized() {
			Vector2 inputVector = _playerInput.Player.Move.ReadValue<Vector2>();
			inputVector = inputVector.normalized;
			return inputVector;
		}
		
	}
}