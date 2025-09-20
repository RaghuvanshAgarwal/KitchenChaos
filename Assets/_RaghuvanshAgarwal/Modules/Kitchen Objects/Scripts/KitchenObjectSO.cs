using UnityEngine;

namespace _RaghuvanshAgarwal.Modules.Kitchen_Objects.Scripts {
    [CreateAssetMenu(fileName = "Kitchen Object", menuName = "RaghuvanshAgarwal/Objects/Kitchen Object")]
    public class KitchenObjectSO : ScriptableObject {
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public Sprite Icon { get; private set; }
        [field: SerializeField] public Transform Prefab { get; private set; }
        
    }
}