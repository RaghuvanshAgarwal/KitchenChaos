using System;
using _RaghuvanshAgarwal.Modules.Player.Input;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _RaghuvanshAgarwal.Modules.Player.Scripts {
	public class GameInput: MonoBehaviour {

		public event EventHandler OnInteractAction;
		
		private PlayerInputActions _playerInput;
		private void Awake() {
			_playerInput= new PlayerInputActions();
			_playerInput.Player.Enable();
			
			_playerInput.Player.Interact.performed += InteractOnPerformed;
		}

		private void InteractOnPerformed(InputAction.CallbackContext obj) {
			OnInteractAction?.Invoke(this, EventArgs.Empty);
		}

		public Vector2 GetMovementVectorNormalized() {
			Vector2 inputVector = _playerInput.Player.Move.ReadValue<Vector2>();
			inputVector = inputVector.normalized;
			return inputVector;
		}
		
	}
}