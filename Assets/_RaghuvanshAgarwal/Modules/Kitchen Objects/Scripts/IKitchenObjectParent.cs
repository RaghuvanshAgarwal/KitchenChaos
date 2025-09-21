using UnityEngine;

namespace _RaghuvanshAgarwal.Modules.Kitchen_Objects.Scripts {
    public interface IKitchenObjectParent {
        public Transform GetKitchenObjectFollowTransform();
        public void SetKitchenObject(KitchenObject kitchenObject);
        public KitchenObject GetKitchenObject();
        public void ClearKitchenObject();
        public bool HasKitchenObject();
    }
}