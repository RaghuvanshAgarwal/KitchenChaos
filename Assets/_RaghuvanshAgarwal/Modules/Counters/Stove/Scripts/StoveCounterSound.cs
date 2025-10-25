using System;
using _RaghuvanshAgarwal.Modules.Audio.Sound;
using _RaghuvanshAgarwal.Modules.Progress_Bar;
using UnityEngine;

namespace _RaghuvanshAgarwal.Modules.Counters.Stove.Scripts {
    public class StoveCounterSound : MonoBehaviour
    {
        [SerializeField] StoveCounter stoveCounter;
        
        AudioSource _audioSource;
        private bool _isBurning;
        private float _warningSoundTimer;
        private const float WarningSoundTimerMax = 0.2f;

        private void Awake() {
            _audioSource  = GetComponent<AudioSource>();
        }

        private void Start() {
            stoveCounter.OnStoveStateChanged += StoveCounter_OnStoveStateChanged;
            stoveCounter.OnProgressChanged += StoveCounter_OnProgressChanged;
        }

        private void OnDestroy() {
            stoveCounter.OnStoveStateChanged -= StoveCounter_OnStoveStateChanged;
            stoveCounter.OnProgressChanged -= StoveCounter_OnProgressChanged;
        }

        private void Update() {
            _audioSource.volume = SoundManager.Instance.SoundVolume;
            _warningSoundTimer -= Time.deltaTime;
            if (!_isBurning) return;
            if (!(_warningSoundTimer <= 0f)) return;
            _warningSoundTimer = WarningSoundTimerMax;
            float volume = 1f;
            SoundManager.Instance.PlayWarningSound(transform.position, volume);

        }

        private void StoveCounter_OnStoveStateChanged(object sender, OnStoveChangedEventArgs e) {
            if (e.State is StoveCounter.State.Idle or StoveCounter.State.Burnt) {
                _audioSource.Pause();
            }
            else {
                _audioSource.Play();
            }
        }
        
        private void StoveCounter_OnProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs e) {
            float burnShowProgressAmount = 0.5f;
            _isBurning = stoveCounter.IsFried() && e.NormalizedProgress > burnShowProgressAmount;
        }
    }
}
