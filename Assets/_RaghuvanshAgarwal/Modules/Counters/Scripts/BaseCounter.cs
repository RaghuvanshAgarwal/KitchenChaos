using _RaghuvanshAgarwal.Modules.Kitchen_Objects.Scripts;
using UnityEngine;

namespace _RaghuvanshAgarwal.Modules.Counters.Scripts {
    public class BaseCounter : MonoBehaviour, IKitchenObjectParent {
        public virtual void Interact(Player.Scripts.Player player){}
        public virtual void InteractAlternate(Player.Scripts.Player player){}
        [SerializeField] protected Transform counterTopPoint;
        
        private KitchenObject _kitchenObject;
        
        public Transform GetKitchenObjectFollowTransform() => counterTopPoint;
        public void SetKitchenObject(KitchenObject kitchenObject) => _kitchenObject = kitchenObject;
        public KitchenObject GetKitchenObject() => _kitchenObject;
        public void ClearKitchenObject() => _kitchenObject = null;
        public bool HasKitchenObject() => _kitchenObject != null;
    }
}
