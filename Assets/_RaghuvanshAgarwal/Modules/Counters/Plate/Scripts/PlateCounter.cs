using System;
using _RaghuvanshAgarwal.Modules.Counters.Scripts;
using _RaghuvanshAgarwal.Modules.Kitchen_Objects.Scripts;
using UnityEngine;

namespace _RaghuvanshAgarwal.Modules.Counters.Plate.Scripts {

    public class PlateCounter : BaseCounter
    {
        public event EventHandler OnPlateSpawned;
        public event EventHandler OnPlateRemoved;
        [SerializeField] KitchenObjectSO plateObject;
        [SerializeField] float plateSpawnRate = 4f;
        [SerializeField] private int maxSpawnedCount = 5;
        private int _spawnedCount;
        private float _currenTime = 0f;

        private void Update() {
            _currenTime += Time.deltaTime;
            if (_currenTime >= plateSpawnRate) {
                _currenTime = 0f;
                if (_spawnedCount < maxSpawnedCount) {
                    _spawnedCount++;
                    OnPlateSpawned?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        public override void Interact(Player.Scripts.Player player) {
            if(player.HasKitchenObject()) return;
            if(_spawnedCount == 0) return;
            KitchenObject.Spawn(player, plateObject);
            _spawnedCount--;
            OnPlateRemoved?.Invoke(this, EventArgs.Empty);
        }
    }
}
