using System;
using _RaghuvanshAgarwal.Modules.Audio.Sound;
using UnityEngine;

namespace _RaghuvanshAgarwal.Modules.Counters.Stove.Scripts {
    public class StoveCounterSound : MonoBehaviour
    {
        [SerializeField] StoveCounter stoveCounter;
        
        AudioSource _audioSource;

        private void Awake() {
            _audioSource  = GetComponent<AudioSource>();
        }

        private void Start() {
            stoveCounter.OnStoveStateChanged += StoveCounter_OnStoveStateChanged;
        }

        private void OnDestroy() {
            stoveCounter.OnStoveStateChanged -= StoveCounter_OnStoveStateChanged;
        }

        private void Update() {
            _audioSource.volume = SoundManager.Instance.SoundVolume;
        }

        private void StoveCounter_OnStoveStateChanged(object sender, OnStoveChangedEventArgs e) {
            if (e.State is StoveCounter.State.Idle or StoveCounter.State.Burnt) {
                _audioSource.Pause();
            }
            else {
                _audioSource.Play();
            }
        }
    }
}
