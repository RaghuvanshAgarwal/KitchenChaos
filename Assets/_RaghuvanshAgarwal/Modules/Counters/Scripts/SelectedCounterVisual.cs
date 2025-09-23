using System;
using _RaghuvanshAgarwal.Modules.Counters.Clear;
using _RaghuvanshAgarwal.Modules.Player.Scripts;
using UnityEngine;

namespace _RaghuvanshAgarwal.Modules.Counters.Scripts {
    public class SelectedCounterVisual : MonoBehaviour {
        
        [SerializeField] private GameObject[] visualArray;
        [SerializeField] private BaseCounter  counter;
        
        private void Start() {
            Player.Scripts.Player.Instance.OnSelectedCounterChanged += PlayerOnSelectedCounterChanged;
        }
        
        private void OnDestroy() {
            Player.Scripts.Player.Instance.OnSelectedCounterChanged -= PlayerOnSelectedCounterChanged;
        }
        
        private void PlayerOnSelectedCounterChanged(object sender, OnSelectedCounterChangedEventArgs e) {
            if (e.SelectedCounter == counter) {
                Show();
            }
            else {
                Hide();
            }
        }

        private void Show() {
            foreach (GameObject obj in visualArray) {
                obj.SetActive(true);
            }
        }

        private void Hide() {
            foreach (GameObject obj in visualArray) {
                obj.SetActive(false);
            }
        }
    }
}