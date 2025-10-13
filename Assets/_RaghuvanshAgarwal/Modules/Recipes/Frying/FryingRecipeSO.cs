using _RaghuvanshAgarwal.Modules.Kitchen_Objects.Scripts;
using UnityEngine;

namespace _RaghuvanshAgarwal.Modules.Recipes.Frying {
    
    [CreateAssetMenu(fileName = "Frying Recipe", menuName = "RaghuvanshAgarwal/Recipes/Frying Recipe")]
    public class FryingRecipeSO : ScriptableObject {
        [field: SerializeField] public KitchenObjectSO Input { get; private set; }
        [field: SerializeField] public KitchenObjectSO Output { get; private set; }
        [field: SerializeField] public float FryingTimerMax { get; private set; }
    }
}
