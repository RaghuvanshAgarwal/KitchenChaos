using System;
using _RaghuvanshAgarwal.Modules.Player.Input;
using UnityEngine;

namespace _RaghuvanshAgarwal.Modules.Player.Scripts {
	public class GameInput: MonoBehaviour {
		private PlayerInputActions _playerInput;
		private void Awake() {
			_playerInput= new PlayerInputActions();
			_playerInput.Player.Enable();
		}

		public Vector2 GetMovementVectorNormalized() {
			Vector2 inputVector = _playerInput.Player.Move.ReadValue<Vector2>();
			inputVector = inputVector.normalized;
			return inputVector;
		}
		
	}
}