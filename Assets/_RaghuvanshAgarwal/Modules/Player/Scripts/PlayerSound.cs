using System;
using _RaghuvanshAgarwal.Modules.Audio.Sound;
using UnityEngine;

namespace _RaghuvanshAgarwal.Modules.Player.Scripts {
    public class PlayerSound : MonoBehaviour {
        private Player _player;
        private float _footstepTimer = 0f;
        private const float FootstepTimerMax = 0.1f;

        private void Awake() {
            _player = GetComponent<Player>();
        }

        private void Update() {
            _footstepTimer -= Time.deltaTime;
            if (_footstepTimer <= 0f) {
                _footstepTimer = FootstepTimerMax;
                if (_player.IsWalking()) {
                    float volume = 2f;
                    SoundManager.Instance.PlayFootstepSounds(transform.position, volume);
                }
            }
        }
    }
}