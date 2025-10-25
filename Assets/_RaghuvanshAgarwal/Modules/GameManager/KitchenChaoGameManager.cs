using System;
using _RaghuvanshAgarwal.Modules.Player.Scripts;
using UnityEngine;

namespace _RaghuvanshAgarwal.Modules.GameManager {
    public class KitchenChaoGameManager : MonoBehaviour {
        
        public static KitchenChaoGameManager Instance {get; private set;}
        public event EventHandler OnStateChanged;
        public event EventHandler OnGamePaused;
        public event EventHandler OnGameResumed;
        private enum State {
            WaitingToStart,
            CountdownToStart,
            Playing,
            GameOver
        }

        private State _state;
        private float _countdownToStart = 3f;
        private float _gamePlayingTimer;
        private const float GamePlayingTimerMax = 600f;
        private bool _isGamePaused;

        public float CountdownToStart => _countdownToStart;

        public float GamePlayingTimer => _gamePlayingTimer;
        public float GamePlayingTimerNormalized => 1 - (_gamePlayingTimer / GamePlayingTimerMax);

        private void Awake() {
            Instance = this;
            _state = State.WaitingToStart;
        }

        private void Start() {
            GameInput.Instance.OnPauseAction += ToggleGamePause;
            GameInput.Instance.OnInteractAction += GameInput_OnInteractAction;
        }

        private void OnDestroy() {
            GameInput.Instance.OnPauseAction -= ToggleGamePause;
            GameInput.Instance.OnInteractAction -= GameInput_OnInteractAction;
        }


        private void Update() {
            switch (_state) {
                case State.WaitingToStart:
                    break;
                case State.CountdownToStart:
                    _countdownToStart -= Time.deltaTime;
                    if (_countdownToStart <= 0) {
                        _gamePlayingTimer = GamePlayingTimerMax;
                        SetState(State.Playing);
                    }
                    break;
                case State.Playing:
                    _gamePlayingTimer -= Time.deltaTime;
                    if (_gamePlayingTimer <= 0) {
                        SetState(State.GameOver);
                    }
                    break;
                case State.GameOver:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public bool IsGamePlaying() {
            return _state == State.Playing;
        }
        
        public bool IsCountdownToStart() {
            return _state == State.CountdownToStart;
        }

        public bool IsGameOver() {
            return _state == State.GameOver;
        }
        
        private void GameInput_OnInteractAction(object sender, EventArgs e) {
            if (_state == State.WaitingToStart) {
                GameInput.Instance.OnInteractAction -= GameInput_OnInteractAction;
                SetState(State.CountdownToStart);
            }
        }
        
        private void ToggleGamePause(object sender, EventArgs e) {
            ToggleGamePause();
        }

        private void SetState(State state) {
            _state = state;
            OnStateChanged?.Invoke(this, EventArgs.Empty);
        }

        public void ToggleGamePause() {
            _isGamePaused = !_isGamePaused;
            Time.timeScale = _isGamePaused ? 0f : 1f;
            if (_isGamePaused) {
                OnGamePaused?.Invoke(this, EventArgs.Empty);
            }
            else {
                OnGameResumed?.Invoke(this, EventArgs.Empty);
            }
        }

        public void GoToMainMenu() {
            Time.timeScale = 1f;
            Loader.Loader.LoadScene(Loader.Loader.Scene.MainMenuScene);
        }
    }
}