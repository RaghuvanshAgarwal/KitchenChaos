using System;
using _RaghuvanshAgarwal.Modules.Player.Scripts;
using UnityEngine;

namespace _RaghuvanshAgarwal.Modules.UI.Game_Options {
    public class RebindConflictUI : MonoBehaviour
    {
        [SerializeField] private GameOptionsUI gameOptionsUI;
        private float _timer;
        private const float TimerMax = 5f;
        private void Start() {
            gameOptionsUI.OnRebindConflict += GameOptionsUI_OnRebindConflict;
            Hide();
        }

        private void OnDestroy() {
            gameOptionsUI.OnRebindConflict -= GameOptionsUI_OnRebindConflict;
        }

        private void Update() {
            _timer += Time.fixedUnscaledDeltaTime;
            if (_timer > TimerMax) {
                Hide();
            }
        }

        private void GameOptionsUI_OnRebindConflict(object sender, EventArgs e) {
            Show();
            _timer = 0;
            Debug.Log("show");
        }

        private void Show() {
            gameObject.SetActive(true);
        }

        private void Hide() {
            gameObject.SetActive(false);
        }
    }
}
