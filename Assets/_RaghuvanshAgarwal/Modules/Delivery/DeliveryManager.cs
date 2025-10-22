using System;
using System.Collections.Generic;
using _RaghuvanshAgarwal.Modules.GameManager;
using _RaghuvanshAgarwal.Modules.Kitchen_Objects.Plate.Scripts;
using _RaghuvanshAgarwal.Modules.Recipes.Recipe;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _RaghuvanshAgarwal.Modules.Delivery {
    
    public class DeliveryManager : MonoBehaviour {
        public event EventHandler OnRecipeAdded;
        public event EventHandler OnRecipeDelivered;
        public event EventHandler OnCorrectRecipeDelivered;
        public event EventHandler OnWrongRecipeDelivered;
        public static DeliveryManager Instance { get; private set; }
        [SerializeField] RecipeListSO recipeListSO;
        private List<RecipeSO> _waitingRecipeList;
        
        private float _waitingTime = 0f;
        private const float WaitingTimeMax = 4f;
        private const int WaitingRecipeMax = 10;

        public List<RecipeSO> WaitingRecipeList => _waitingRecipeList;
        public int CorrectOrderCount { get; private set; }

        private void Awake() {
            if (Instance != null) {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            _waitingRecipeList = new List<RecipeSO>();
            CorrectOrderCount = 0;
        }

        private void Update() {
            if(!KitchenChaoGameManager.Instance.IsGamePlaying()) return;
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
                    _waitingRecipeList.RemoveAt(i);
                    CorrectOrderCount++;
                    OnRecipeDelivered?.Invoke(this, EventArgs.Empty);
                    OnCorrectRecipeDelivered?.Invoke(this, EventArgs.Empty);
                    return;
                }
            }
            OnWrongRecipeDelivered?.Invoke(this, EventArgs.Empty);
        }
        
        
    }
}
