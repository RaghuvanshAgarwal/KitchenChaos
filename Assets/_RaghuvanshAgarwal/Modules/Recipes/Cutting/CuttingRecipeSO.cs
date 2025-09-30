using _RaghuvanshAgarwal.Modules.Kitchen_Objects.Scripts;
using UnityEngine;

namespace _RaghuvanshAgarwal.Modules.Recipes.Cutting {
    
    [CreateAssetMenu(fileName = "Cutting Recipe", menuName = "RaghuvanshAgarwal/Recipes/Cutting Recipe")]
    public class CuttingRecipeSO : ScriptableObject
    {
        [field: SerializeField] public KitchenObjectSO Input { get; private set; }
        [field: SerializeField] public KitchenObjectSO Output { get; private set; }
        [field: SerializeField] public int CuttingProgressMax { get; private set; }
    }
}
