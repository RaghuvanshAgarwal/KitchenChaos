using System;
using UnityEngine;

namespace _RaghuvanshAgarwal.Modules.Counters.Container {
    public class ContainerCounterVisual : MonoBehaviour {
        private static readonly int OpenClose = Animator.StringToHash("OpenClose");
        [SerializeField] private ContainerCounter counter;
        private Animator _animator;
        
        private void Awake() {
            _animator = GetComponent<Animator>();
        }

        private void Start() {
            counter.OnPlayerGrabbedObject += CounterOnPlayerGrabbedObject;
        }

        private void OnDestroy() {
            counter.OnPlayerGrabbedObject -= CounterOnPlayerGrabbedObject;
        }

        private void CounterOnPlayerGrabbedObject(object sender, EventArgs e) {
            _animator.SetTrigger(OpenClose);
        }
    }
}
