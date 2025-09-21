using System;
using _RaghuvanshAgarwal.Modules.Kitchen_Objects;
using _RaghuvanshAgarwal.Modules.Kitchen_Objects.Scripts;
using UnityEngine;

namespace _RaghuvanshAgarwal.Modules.Counters.Clear {
    public class ClearCounter : MonoBehaviour {
        [SerializeField] private KitchenObjectSO objectData;
        [SerializeField] private Transform counterTopPoint;
        [SerializeField] private ClearCounter secondaryCounter;
        [SerializeField] private bool testing;
        
        private KitchenObject _kitchenObject;

        private void Update() {
            if (testing && Input.GetKeyDown(KeyCode.T)) {
                if (_kitchenObject != null) {
                    _kitchenObject.SetCounter(secondaryCounter);
                }
            }
        }

        public void Interact() {
            if (_kitchenObject != null) {
                Debug.Log(_kitchenObject.GetCounter().name);
                return;
            }
            Transform kitchenObject =  Instantiate(objectData.Prefab, counterTopPoint.position, Quaternion.identity, counterTopPoint);
            if (kitchenObject.TryGetComponent(out KitchenObject kitchen)) {
                kitchen.SetCounter(this);
            }
        }
        
        public Transform GetKitchenObjectCounterTop() => counterTopPoint;
        public void SetKitchenObject(KitchenObject kitchenObject) => _kitchenObject = kitchenObject;
        public KitchenObject GetKitchenObject() => _kitchenObject;
        public void ClearKitchenObject() => _kitchenObject = null;
        public bool HasKitchenObject() => _kitchenObject != null;
    }
}
