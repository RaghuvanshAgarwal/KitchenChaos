using System;
using _RaghuvanshAgarwal.Modules.Progress_Bar;
using UnityEngine;

namespace _RaghuvanshAgarwal.Modules.Counters.Stove.Scripts {
    public class StoveProgressBarAnimationController : MonoBehaviour
    {
        private static readonly int ShouldFlash = Animator.StringToHash("ShouldFlash");
        [SerializeField] private StoveCounter stoveCounter;
        private Animator _animator;

        private void Awake() {
            _animator =  GetComponent<Animator>();
        }

        private void Start() {
            stoveCounter.OnProgressChanged += StoveCounter_OnProgressChanged;
            _animator.SetBool(ShouldFlash, false);
        }

        private void OnDestroy() {
            stoveCounter.OnProgressChanged -= StoveCounter_OnProgressChanged;
        }

        private void StoveCounter_OnProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs e) {
            float burnShowProgressAmount = 0.5f;
            bool shouldFlash = stoveCounter.IsFried() && e.NormalizedProgress > burnShowProgressAmount;
            _animator.SetBool(ShouldFlash, shouldFlash);
        }
    }
}
