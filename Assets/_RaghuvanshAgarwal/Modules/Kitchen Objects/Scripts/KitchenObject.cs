using _RaghuvanshAgarwal.Modules.Counters.Clear;
using UnityEngine;

namespace _RaghuvanshAgarwal.Modules.Kitchen_Objects.Scripts {
    public class KitchenObject : MonoBehaviour {
        [field: SerializeField] public KitchenObjectSO ObjectData { get; private set; }
        
        private IKitchenObjectParent _parent;

        public void SetParent(IKitchenObjectParent newParent) {
            // Telling the previous counter to clear the object
            if (_parent != null) {
                _parent.ClearKitchenObject();
            }
            
            // Setting the new counter
            _parent = newParent;
            if (newParent.HasKitchenObject()) {
                Debug.LogError("Kitchen Object Parent already has a KitchenObject");
            }
            newParent.SetKitchenObject(this);
            
            //Visually changing the position
            transform.parent = newParent.GetKitchenObjectFollowTransform();
            transform.localPosition = Vector3.zero;
        }

        public IKitchenObjectParent GetParent() {
            return _parent;
        }
    }
}
