using System;
using System.Collections.Generic;
using _RaghuvanshAgarwal.Modules.Kitchen_Objects.Scripts;
using UnityEngine;

namespace _RaghuvanshAgarwal.Modules.Kitchen_Objects.Plate.Scripts {

    public class IngredientAddedOnPlateEventArgs : EventArgs {
        public KitchenObjectSO Ingredient;

        public IngredientAddedOnPlateEventArgs(KitchenObjectSO ingredient) { 
            Ingredient = ingredient;
        }
    }
    public class PlateKitchenObject : KitchenObject
    {
        public event EventHandler<IngredientAddedOnPlateEventArgs> OnIngredientAddedOnPlate;
        [SerializeField] private List<KitchenObjectSO> validIngredients = new List<KitchenObjectSO>();
        private List<KitchenObjectSO> _ingredients = new List<KitchenObjectSO>();

        private void Awake() {
            _ingredients =  new List<KitchenObjectSO>();
        }

        public bool TryAddIngredient(KitchenObjectSO ingredient) {
            if(!validIngredients.Contains(ingredient)) return false;
            if(_ingredients.Contains(ingredient)) return false;
            _ingredients.Add(ingredient);
            OnIngredientAddedOnPlate?.Invoke(this, new IngredientAddedOnPlateEventArgs(ingredient));
            return true;
        }
    }
}
