using System;
using System.Collections.Generic;
using UnityEngine;

namespace _RaghuvanshAgarwal.Modules.Counters.Plate.Scripts {
    public class PlateCounterVisual : MonoBehaviour
    {
        [SerializeField] GameObject platePrefab;
        [SerializeField] PlateCounter plateCounter;
        [SerializeField] Transform plateParent;

        List<GameObject> _plateObjects = new List<GameObject>();

        private void Awake() {
            _plateObjects = new List<GameObject>();
        }

        private void Start() {
            plateCounter.OnPlateSpawned += PlateCounterOnPlateSpawned;
            plateCounter.OnPlateRemoved += PlateCounterOnPlateRemoved;
        }

        private void OnDestroy() {
            plateCounter.OnPlateSpawned -= PlateCounterOnPlateSpawned;
            plateCounter.OnPlateRemoved -= PlateCounterOnPlateRemoved;
        }

        private void PlateCounterOnPlateSpawned(object sender, EventArgs e) {
            Transform plate = Instantiate(platePrefab, plateParent).transform;
            const float verticalSpace = 0.1f;
            plate.localPosition = Vector3.up * (_plateObjects.Count * verticalSpace);
            _plateObjects.Add(plate.gameObject);
        }

        private void PlateCounterOnPlateRemoved(object sender, EventArgs e) {
            Destroy(_plateObjects[^1]);
            _plateObjects.RemoveAt(_plateObjects.Count - 1);
        }
    }
}
