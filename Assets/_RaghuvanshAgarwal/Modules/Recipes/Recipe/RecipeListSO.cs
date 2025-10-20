using System.Collections.Generic;
using UnityEngine;

namespace _RaghuvanshAgarwal.Modules.Recipes.Recipe {
    [CreateAssetMenu(fileName = "Recipe List", menuName = "RaghuvanshAgarwal/Recipes/Recipe List", order = 0)]
    public class RecipeListSO : ScriptableObject {
        public List<RecipeSO> recipes;
    }
}