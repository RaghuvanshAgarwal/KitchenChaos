using System;
using System.Linq;
using _RaghuvanshAgarwal.Modules.Counters.Scripts;
using _RaghuvanshAgarwal.Modules.Kitchen_Objects.Scripts;
using _RaghuvanshAgarwal.Modules.Recipes.Cutting;
using UnityEngine;
using UnityEngine.Events;

namespace _RaghuvanshAgarwal.Modules.Counters.Cutting.Scripts {
    public class CuttingCounter : BaseCounter
    {
        public class OnProgressChangedEventArgs : EventArgs {
            public float NormalizedProgress { get; private set; }

            public OnProgressChangedEventArgs(float normalizedProgress) {
                NormalizedProgress = normalizedProgress;
            }
        }
        public event EventHandler<OnProgressChangedEventArgs> OnProgressChanged;
        public event EventHandler OnCuttingActionPerformed;
        
        
        [SerializeField] CuttingRecipeSO[] cuttingRecipes;
        private int _cuttingProgress;
        public override void Interact(Player.Scripts.Player player) {
            if (HasKitchenObject()) {
                if (!player.HasKitchenObject()) {
                    GetKitchenObject().SetParent(player);
                    _cuttingProgress = 0;
                    OnProgressChanged?.Invoke(this, new OnProgressChangedEventArgs(0));
                }
            }
            else {
                if (player.HasKitchenObject()) {
                    if (HasRecipeWithKitchenObject(player.GetKitchenObject().ObjectData)) {
                        player.GetKitchenObject().SetParent(this);
                    }
                }
            }
        }

        public override void InteractAlternate(Player.Scripts.Player player) {
            if (!HasKitchenObject() || !HasRecipeWithKitchenObject(GetKitchenObject().ObjectData)) return;
            _cuttingProgress++;
            CuttingRecipeSO recipeSO = GetCuttingRecipeWithInput(GetKitchenObject().ObjectData);
            OnProgressChanged?.Invoke(this, new OnProgressChangedEventArgs((float)_cuttingProgress / recipeSO.CuttingProgressMax));
            OnCuttingActionPerformed?.Invoke(this, EventArgs.Empty);
            if (_cuttingProgress >= recipeSO.CuttingProgressMax) {
                GetKitchenObject().DestroySelf();
                KitchenObject.Spawn(this, recipeSO.Output);
            }
            
        }

        bool HasRecipeWithKitchenObject(KitchenObjectSO kitchenObject) {
            return GetCuttingRecipeWithInput(kitchenObject) != null;
        }

        private KitchenObjectSO GetOutputKitchenObjectWithInput(KitchenObjectSO input) {
            CuttingRecipeSO recipe = GetCuttingRecipeWithInput(input);
            return recipe != null ? recipe.Output : null;
        }

        private CuttingRecipeSO GetCuttingRecipeWithInput(KitchenObjectSO input) {
            return cuttingRecipes.FirstOrDefault(recipe => recipe.Input.Name == input.Name);
        }
    }
}
