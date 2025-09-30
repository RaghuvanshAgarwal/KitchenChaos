using System;
using UnityEngine;

namespace _RaghuvanshAgarwal.Modules.Utils {
    public class LookAtCamera : MonoBehaviour {
        private enum Mode {
            LookAt,
            LookAtInverted,
        }
        [SerializeField] private Mode mode;
        private void LateUpdate() {
            switch (mode) {
                case Mode.LookAt:
                    transform.LookAt(Camera.main.transform);
                    break;
                case Mode.LookAtInverted:
                    Vector3 directionFromCamera = transform.position - Camera.main.transform.position;
                    transform.LookAt(transform.position + directionFromCamera);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
        }
    }
}
