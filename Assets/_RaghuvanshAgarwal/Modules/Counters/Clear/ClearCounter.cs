using _RaghuvanshAgarwal.Modules.Kitchen_Objects;
using _RaghuvanshAgarwal.Modules.Kitchen_Objects.Scripts;
using UnityEngine;

namespace _RaghuvanshAgarwal.Modules.Counters.Clear {
    public class ClearCounter : MonoBehaviour {
        [SerializeField] private KitchenObjectSO objectData;
        [SerializeField] private Transform counterTopPoint;
        
        public void Interact() {
            Transform kitchenObject =  Instantiate(objectData.Prefab, counterTopPoint.position, Quaternion.identity, transform);
            if (kitchenObject.TryGetComponent(out KitchenObject kitchen)) {
                Debug.Log(kitchen.ObjectData.Name + " has been created");
            }
        }
    }
}
