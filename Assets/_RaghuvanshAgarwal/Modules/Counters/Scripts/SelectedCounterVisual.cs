using System;
using _RaghuvanshAgarwal.Modules.Counters.Clear;
using _RaghuvanshAgarwal.Modules.Player.Scripts;
using UnityEngine;

namespace _RaghuvanshAgarwal.Modules.Counters.Scripts {
    public class SelectedCounterVisual : MonoBehaviour {
        
        [SerializeField] private GameObject visual;
        [SerializeField] private ClearCounter  clearCounter;
        
        private void Start() {
            Player.Scripts.Player.Instance.OnSelectedCounterChanged += PlayerOnSelectedCounterChanged;
        }
        
        private void OnDestroy() {
            Player.Scripts.Player.Instance.OnSelectedCounterChanged -= PlayerOnSelectedCounterChanged;
        }
        
        private void PlayerOnSelectedCounterChanged(object sender, OnSelectedCounterChangedEventArgs e) {
            if (e.SelectedCounter == clearCounter) {
                Show();
            }
            else {
                Hide();
            }
        }

        private void Show() {
            visual.SetActive(true);
        }

        private void Hide() {
            visual.SetActive(false);
        }
    }
}