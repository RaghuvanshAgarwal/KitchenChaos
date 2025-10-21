using System;
using UnityEngine;

namespace _RaghuvanshAgarwal.Modules.GameManager {
    public class KitchenChaoGameManager : MonoBehaviour {
        
        public static KitchenChaoGameManager Instance {get; private set;}
        public event EventHandler OnStateChanged;
        private enum State {
            WaitingToStart,
            CountdownToStart,
            Playing,
            GameOver
        }

        private State _state;
        private float _waitingToStartTime = 1f;
        private float _countdownToStart = 3f;
        private float _gamePlayingTimer = 10f;

        public float CountdownToStart => _countdownToStart;

        private void Awake() {
            Instance = this;
            _state = State.WaitingToStart;
            
        }

        private void Update() {
            switch (_state) {
                case State.WaitingToStart:
                    _waitingToStartTime -= Time.deltaTime;
                    if (_waitingToStartTime <= 0) {
                        _state = State.CountdownToStart;
                        OnStateChanged?.Invoke(this, EventArgs.Empty);
                    }
                    break;
                case State.CountdownToStart:
                    _countdownToStart -= Time.deltaTime;
                    if (_countdownToStart <= 0) {
                        _state = State.Playing;
                        OnStateChanged?.Invoke(this, EventArgs.Empty);
                    }
                    break;
                case State.Playing:
                    _gamePlayingTimer -= Time.deltaTime;
                    if (_gamePlayingTimer <= 0) {
                        _state = State.GameOver;
                        OnStateChanged?.Invoke(this, EventArgs.Empty);
                    }
                    break;
                case State.GameOver:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            Debug.Log(_state);
        }

        public bool IsGamePlaying() {
            return _state == State.Playing;
        }
        
        public bool IsCountdownToStart() {
            return _state == State.CountdownToStart;
        }
    }
}