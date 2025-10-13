using System;
using UnityEngine;

namespace _RaghuvanshAgarwal.Modules.Counters.Stove.Scripts {
    public class StoveCounterVisual : MonoBehaviour
    {
        [SerializeField] StoveCounter stoveCounter;
        [SerializeField] GameObject[] visuals;

        private void Start() {
            stoveCounter.OnStoveStateChanged += (sender, args) => {
                switch (args.State) {
                    case StoveCounter.State.Idle:
                    case StoveCounter.State.Burnt:
                        Hide();
                        break;
                    case StoveCounter.State.Frying:
                    case StoveCounter.State.Fried:
                        Show();
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            };
        }


        void Show() {
            foreach (GameObject v in visuals) {
                v.SetActive(true);
            }
        }

        void Hide() {
            foreach (GameObject v in visuals) {
                v.SetActive(false);
            }
        }
    }
}
