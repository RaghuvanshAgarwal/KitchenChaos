using System;
using System.Collections.Generic;
using _RaghuvanshAgarwal.Modules.Player.Input;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _RaghuvanshAgarwal.Modules.Player.Scripts {
	public class GameInput: MonoBehaviour {
		private const string PlayerPrefInputBinding = "PLAYER_PREF_INPUT_BINDING";
		public enum Binding {
			MoveUp,
			MoveDown,
			MoveLeft,
			MoveRight,
			Interact,
			InteractAlternate,
			Pause,
			GamepadInteract,
			GamepadInteractAlternate,
			GamepadPause
		}
		public static GameInput Instance {get; private set;}
		public event EventHandler OnInteractAction;
		public event EventHandler OnInteractAlternateAction;
		public event EventHandler OnPauseAction;
		public event EventHandler OnBindingRebinded;
		
		private PlayerInputActions _playerInput;
		private void Awake() {
			Instance = this;
			_playerInput= new PlayerInputActions();
			if (PlayerPrefs.HasKey(PlayerPrefInputBinding)) {
				_playerInput.LoadBindingOverridesFromJson(PlayerPrefs.GetString(PlayerPrefInputBinding));
			}
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

		public string GetBindingString(Binding binding) {
			switch (binding) {
				case Binding.MoveUp:
					return _playerInput.Player.Move.bindings[1].ToDisplayString();
				case Binding.MoveDown:
					return _playerInput.Player.Move.bindings[2].ToDisplayString();
				case Binding.MoveLeft:
					return _playerInput.Player.Move.bindings[3].ToDisplayString();
				case Binding.MoveRight:
					return _playerInput.Player.Move.bindings[4].ToDisplayString();
				case Binding.Interact:
					return _playerInput.Player.Interact.GetBindingDisplayString(0);
				case Binding.InteractAlternate:
					return _playerInput.Player.InteractAlternate.GetBindingDisplayString(0);
				case Binding.Pause:
					return _playerInput.Player.Pause.GetBindingDisplayString(0);
				case Binding.GamepadInteract:
					return _playerInput.Player.Interact.GetBindingDisplayString(1);
				case Binding.GamepadInteractAlternate:
					return _playerInput.Player.InteractAlternate.GetBindingDisplayString(1);
				case Binding.GamepadPause:
					return _playerInput.Player.Pause.GetBindingDisplayString(1);
				default:
					throw new ArgumentOutOfRangeException(nameof(binding), binding, null);
			}
		}

		public void RebindInput(Binding binding, Action onComplete, Action onConflict) {
			_playerInput.Player.Disable();
			InputAction inputAction;
			int index;
			switch (binding) {
				case Binding.MoveUp:
					inputAction = _playerInput.Player.Move;
					index = 1;
					break;
				case Binding.MoveDown:
					inputAction = _playerInput.Player.Move;
					index = 2;
					break;
				case Binding.MoveLeft:
					inputAction = _playerInput.Player.Move;
					index = 3;
					break;
				case Binding.MoveRight:
					inputAction = _playerInput.Player.Move;
					index = 4;
					break;
				case Binding.Interact:
					inputAction = _playerInput.Player.Interact;
					index = 0;
					break;
				case Binding.InteractAlternate:
					inputAction = _playerInput.Player.InteractAlternate;
					index = 0;
					break;
				case Binding.Pause:
					inputAction = _playerInput.Player.Pause;
					index = 0;
					break;
				case Binding.GamepadInteract:
					inputAction = _playerInput.Player.Interact;
					index = 1;
					break;
				case Binding.GamepadInteractAlternate:
					inputAction = _playerInput.Player.InteractAlternate;
					index = 1;
					break;
				case Binding.GamepadPause:
					inputAction = _playerInput.Player.Pause;
					index = 1;
					break;
				default:
					throw new ArgumentOutOfRangeException(nameof(binding), binding, null);
			}
			RebindInput(inputAction, index, onComplete, onConflict);
		}

		private void RebindInput(InputAction inputAction, int index, Action onComplete, Action onConflict) {
			string oldPath = inputAction.bindings[index].effectivePath;
			inputAction.PerformInteractiveRebinding(index)
				.WithControlsExcluding("<Mouse>/position")
				.WithControlsExcluding("<Mouse>/delta")
				.OnComplete(callback => {
					callback.Dispose();
					string newPath = inputAction.bindings[index].effectivePath;
					if (IsRebindConflict()) {
						inputAction.ApplyBindingOverride(index,oldPath);
						onConflict();
					}
					_playerInput.Player.Enable();
					onComplete();
					OnBindingRebinded?.Invoke(this, EventArgs.Empty);
					PlayerPrefs.SetString(PlayerPrefInputBinding, _playerInput.SaveBindingOverridesAsJson());
					PlayerPrefs.Save();
				})
				.OnCancel(callback => {
					_playerInput.Player.Enable();
					callback.Dispose();
					onComplete();
				})
				.Start();
		}


		private bool IsRebindConflict() {
			List<string> paths = new List<string>();
			foreach (InputActionMap inputActionMap in _playerInput.asset.actionMaps) {
				foreach (InputAction inputAction in inputActionMap.actions) {
					foreach (InputBinding inputBinding in inputAction.bindings) {
						if(inputBinding.isComposite) continue;
						if (paths.Contains(inputBinding.effectivePath)) {
							return true;
						}
						paths.Add(inputBinding.effectivePath);
					}
				}
			}

			return false;
		}

	}
}