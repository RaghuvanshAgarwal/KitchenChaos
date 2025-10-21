using System;
using UnityEngine;

namespace _RaghuvanshAgarwal.Modules.Loader {
    public class LoaderCallback : MonoBehaviour
    {
        private bool _isFirstFrame = true;

        private void Update() {
            if (_isFirstFrame) {
                _isFirstFrame = false;
                Loader.LoaderCallback();
            }
        }
    }
}
