using System;
using System.Linq;
using _RaghuvanshAgarwal.Modules.Counters.Scripts;
using _RaghuvanshAgarwal.Modules.Kitchen_Objects.Plate.Scripts;
using _RaghuvanshAgarwal.Modules.Kitchen_Objects.Scripts;
using _RaghuvanshAgarwal.Modules.Progress_Bar;
using _RaghuvanshAgarwal.Modules.Recipes.Cutting;
using UnityEngine;
using UnityEngine.Events;

namespace _RaghuvanshAgarwal.Modules.Counters.Cutting.Scripts {
    public class CuttingCounter : BaseCounter, IHasProgress
    {
        public event EventHandler OnCuttingActionPerformed;
        public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;
        
        [SerializeField] CuttingRecipeSO[] cuttingRecipes;
        private int _cuttingProgress;
        public override void Interact(Player.Scripts.Player player) {
            if (HasKitchenObject()) {
                if (player.HasKitchenObject()) {
                    // Player has kitchen Object
                    if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject playerPlate)) {
                        if (playerPlate.TryAddIngredient(GetKitchenObject().ObjectData)) {
                            GetKitchenObject().DestroySelf();
                            _cuttingProgress = 0;
                            OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs(0));
                        }
                    }
                }
                else {
                    GetKitchenObject().SetParent(player);
                    _cuttingProgress = 0;
                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs(0));
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
            CuttingRecipeSO recipeSO = GetRecipeWithInput(GetKitchenObject().ObjectData);
            OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs((float)_cuttingProgress / recipeSO.CuttingProgressMax));
            OnCuttingActionPerformed?.Invoke(this, EventArgs.Empty);
            if (_cuttingProgress >= recipeSO.CuttingProgressMax) {
                GetKitchenObject().DestroySelf();
                KitchenObject.Spawn(this, recipeSO.Output);
            }
            
        }

        bool HasRecipeWithKitchenObject(KitchenObjectSO kitchenObject) {
            return GetRecipeWithInput(kitchenObject) != null;
        }

        private CuttingRecipeSO GetRecipeWithInput(KitchenObjectSO input) {
            return cuttingRecipes.FirstOrDefault(recipe => recipe.Input.Name == input.Name);
        }

    }
}
