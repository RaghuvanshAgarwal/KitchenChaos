using System;
using _RaghuvanshAgarwal.Modules.Kitchen_Objects;
using _RaghuvanshAgarwal.Modules.Kitchen_Objects.Scripts;
using UnityEngine;

namespace _RaghuvanshAgarwal.Modules.Counters.Clear {
    public class ClearCounter : MonoBehaviour, IKitchenObjectParent {
        [SerializeField] private KitchenObjectSO objectData;
        [SerializeField] private Transform counterTopPoint;
        
        private KitchenObject _kitchenObject;
        

        public void Interact(Player.Scripts.Player player) {
            if (_kitchenObject != null) {
                _kitchenObject.SetParent(player);
                return;
            }

            if (player.HasKitchenObject()) {
                player.GetKitchenObject().SetParent(this);
                return;
            }
            
            Transform kitchenObject =  Instantiate(objectData.Prefab, counterTopPoint.position, Quaternion.identity, counterTopPoint);
            if (kitchenObject.TryGetComponent(out KitchenObject kitchen)) {
                kitchen.SetParent(this);
            }
        }
        
        public Transform GetKitchenObjectFollowTransform() => counterTopPoint;
        public void SetKitchenObject(KitchenObject kitchenObject) => _kitchenObject = kitchenObject;
        public KitchenObject GetKitchenObject() => _kitchenObject;
        public void ClearKitchenObject() => _kitchenObject = null;
        public bool HasKitchenObject() => _kitchenObject != null;
    }
}
