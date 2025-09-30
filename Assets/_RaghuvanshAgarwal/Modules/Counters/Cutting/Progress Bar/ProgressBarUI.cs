using System;
using _RaghuvanshAgarwal.Modules.Counters.Cutting.Scripts;
using UnityEngine;
using UnityEngine.UI;

namespace _RaghuvanshAgarwal.Modules.Counters.Cutting.Progress_Bar {
    public class ProgressBarUI : MonoBehaviour {
        
        [SerializeField] private CuttingCounter cuttingCounter;
        [SerializeField] private Image bar;

        private void Start() {
            bar.fillAmount = 0;
            cuttingCounter.OnProgressChanged += CuttingCounterOnProgressChanged;
            Hide();
        }

        private void OnDestroy() {
            cuttingCounter.OnProgressChanged -= CuttingCounterOnProgressChanged;
        }

        private void CuttingCounterOnProgressChanged(object sender, CuttingCounter.OnProgressChangedEventArgs e) {
            bar.fillAmount = e.NormalizedProgress;
            if (e.NormalizedProgress == 0f || Mathf.Approximately(e.NormalizedProgress, 1f)) {
                Hide();
            }
            else {
                Show();
            }
        }

        private void Show() {
            gameObject.SetActive(true);
        }

        private void Hide() {
            gameObject.SetActive(false);
        }
    }
}
