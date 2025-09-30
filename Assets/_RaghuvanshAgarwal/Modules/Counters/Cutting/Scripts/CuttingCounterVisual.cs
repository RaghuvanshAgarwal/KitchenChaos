using System;
using UnityEngine;

namespace _RaghuvanshAgarwal.Modules.Counters.Cutting.Scripts {
    public class CuttingCounterVisual : MonoBehaviour
    {
        private static readonly int Cut = Animator.StringToHash("Cut");
        [SerializeField] private CuttingCounter counter;
        [SerializeField] private Animator animator;

        private void Start() {
            counter.OnCuttingActionPerformed += CounterOnCuttingActionPerformed;
        }

        private void OnDestroy() {
            counter.OnCuttingActionPerformed -= CounterOnCuttingActionPerformed;
        }
        
        private void CounterOnCuttingActionPerformed(object sender, EventArgs e) {
            animator.SetTrigger(Cut);
        }
    }
}
