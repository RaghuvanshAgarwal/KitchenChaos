using _RaghuvanshAgarwal.Modules.Counters.Cutting.Scripts;
using UnityEngine;
using UnityEngine.UI;

namespace _RaghuvanshAgarwal.Modules.Progress_Bar {
    public class ProgressBarUI : MonoBehaviour {
        [SerializeField] GameObject hasProgressGameObject;
        [SerializeField] private Image bar;
        private IHasProgress _hasProgress;
        

        private void Start() {
            if (hasProgressGameObject.TryGetComponent(out IHasProgress progress)) {
                _hasProgress = progress;
                _hasProgress.OnProgressChanged += CuttingCounterOnProgressChanged;
            }
            bar.fillAmount = 0;
            Hide();
        }

        private void OnDestroy() {
            if (hasProgressGameObject.TryGetComponent(out IHasProgress progress)) {
                _hasProgress.OnProgressChanged -= CuttingCounterOnProgressChanged;
            }
        }

        private void CuttingCounterOnProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs e) {
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
