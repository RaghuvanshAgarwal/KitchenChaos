using System;
using System.Collections.Generic;
using _RaghuvanshAgarwal.Modules.Kitchen_Objects.Plate.Scripts;
using _RaghuvanshAgarwal.Modules.Recipes.Recipe;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _RaghuvanshAgarwal.Modules.Delivery {
    
    public class DeliveryManager : MonoBehaviour {
        public event EventHandler OnRecipeAdded;
        public event EventHandler OnRecipeDelivered;
        public static DeliveryManager Instance { get; private set; }
        [SerializeField] RecipeListSO recipeListSO;
        private List<RecipeSO> _waitingRecipeList;
        
        private float _waitingTime = 0f;
        private const float WaitingTimeMax = 4f;
        private const int WaitingRecipeMax = 4;

        public List<RecipeSO> WaitingRecipeList => _waitingRecipeList;

        private void Awake() {
            if (Instance != null) {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            _waitingRecipeList = new List<RecipeSO>();
        }

        private void Update() {
            _waitingTime -=  Time.deltaTime;
            if (_waitingTime <= 0f) {
                _waitingTime = WaitingTimeMax;
                if (_waitingRecipeList.Count < WaitingRecipeMax) {
                    RecipeSO recipe = recipeListSO.recipes[Random.Range(0, recipeListSO.recipes.Count)];
                    _waitingRecipeList.Add(recipe);
                    OnRecipeAdded?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        public void DeliverRecipe(PlateKitchenObject plateKitchenObject) {
            for (int i = 0; i < _waitingRecipeList.Count; ++i) {
                RecipeSO recipe = _waitingRecipeList[i];
                if (recipe.IsThisRecipe(plateKitchenObject.Ingredients)) {
                    Debug.Log("Delivering recipe is correct " + recipe.name);
                    _waitingRecipeList.RemoveAt(i);
                    OnRecipeDelivered?.Invoke(this, EventArgs.Empty);
                    return;
                }
            }
            Debug.Log("Delivering recipe is incorrect");
        }
        
        
    }
}
