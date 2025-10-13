using _RaghuvanshAgarwal.Modules.Kitchen_Objects.Scripts;
using UnityEngine;

namespace _RaghuvanshAgarwal.Modules.Recipes.Burning_Recipe {
    [CreateAssetMenu(fileName = "Burning Recipe", menuName = "RaghuvanshAgarwal/Recipes/Burning", order = 0)]
    public class BurningRecipeSO : ScriptableObject {
        [field: SerializeField] public KitchenObjectSO Input { get; private set; }
        [field: SerializeField] public KitchenObjectSO Output { get; private set; }
        [field: SerializeField] public float BurningTimerMax { get; private set; }
    }
}