using _RaghuvanshAgarwal.Modules.Counters.Clear;
using UnityEngine;

namespace _RaghuvanshAgarwal.Modules.Kitchen_Objects.Scripts {
    public class KitchenObject : MonoBehaviour {
        [field: SerializeField] public KitchenObjectSO ObjectData { get; private set; }
        
        private ClearCounter _counter;

        public void SetCounter(ClearCounter newCounter) {
            // Telling the previous counter to clear the object
            if (_counter != null) {
                _counter.ClearKitchenObject();
            }
            
            // Setting the new counter
            _counter = newCounter;
            if (newCounter.HasKitchenObject()) {
                Debug.LogError("Counter already has a KitchenObject");
            }
            newCounter.SetKitchenObject(this);
            
            //Visually changing the position
            transform.parent = newCounter.GetKitchenObjectCounterTop();
            transform.localPosition = Vector3.zero;
        }

        public ClearCounter GetCounter() {
            return _counter;
        }
    }
}
