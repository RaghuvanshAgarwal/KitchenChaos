using System.Collections.Generic;
using _RaghuvanshAgarwal.Modules.Kitchen_Objects.Scripts;
using UnityEngine;

namespace _RaghuvanshAgarwal.Modules.Recipes.Recipe {
    [CreateAssetMenu(fileName = "RecipeSO", menuName = "RaghuvanshAgarwal/Recipes/RecipeSO")]
    public class RecipeSO : ScriptableObject
    {
        public string recipeName;
        public List<KitchenObjectSO> ingredients = new List<KitchenObjectSO>();


        public bool IsThisRecipe(List<KitchenObjectSO> plateKitchenObjects) {
            if (ingredients.Count != plateKitchenObjects.Count) return false;
            foreach (KitchenObjectSO kitchenObject in plateKitchenObjects) {
                if (!ingredients.Contains(kitchenObject)) {
                    return false;
                }
            }
            return true;
        }
    }
}
