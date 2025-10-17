using System;
using System.Linq;
using _RaghuvanshAgarwal.Modules.Counters.Scripts;
using _RaghuvanshAgarwal.Modules.Kitchen_Objects.Plate.Scripts;
using _RaghuvanshAgarwal.Modules.Kitchen_Objects.Scripts;
using _RaghuvanshAgarwal.Modules.Progress_Bar;
using _RaghuvanshAgarwal.Modules.Recipes.Burning_Recipe;
using _RaghuvanshAgarwal.Modules.Recipes.Frying;
using UnityEngine;

namespace _RaghuvanshAgarwal.Modules.Counters.Stove.Scripts {

    public class OnStoveChangedEventArgs : EventArgs {
        public StoveCounter.State State;

        public OnStoveChangedEventArgs(StoveCounter.State state) {
            State = state;
        }
    }
    public class StoveCounter : BaseCounter, IHasProgress {

        public enum State {
            Idle,
            Frying,
            Fried,
            Burnt,
        }
        
        public event EventHandler<OnStoveChangedEventArgs> OnStoveStateChanged;
        public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;
        
        
        [SerializeField] private FryingRecipeSO[] fryingRecipes;
        private float _fryingTimer;
        private FryingRecipeSO _fryingRecipe;
        
        [SerializeField] private BurningRecipeSO[] burningRecipes;
        private float _burningTimer;
        private BurningRecipeSO _burningRecipe;
        
        
        private State _state;

        private void Start() {
            _state =  State.Idle;
        }

        private void Update() {
            if(!HasKitchenObject()) return;
            switch (_state) {
                case State.Idle:
                    break;
                case State.Frying:
                    _fryingTimer += Time.deltaTime;
                    OnProgressChanged?.Invoke(this,new IHasProgress.OnProgressChangedEventArgs(_fryingTimer / _fryingRecipe.FryingTimerMax));
                    if (_fryingTimer >= _fryingRecipe.FryingTimerMax) {
                        GetKitchenObject().DestroySelf();
                        KitchenObject.Spawn(this, _fryingRecipe.Output);
                        if (HasBurningRecipeWithKitchenObject(GetKitchenObject().ObjectData)) {
                            _state = State.Fried;
                            _burningTimer = 0;
                            _burningRecipe = GetBurningRecipeWithInput(GetKitchenObject().ObjectData);
                            OnStoveStateChanged?.Invoke(this, new OnStoveChangedEventArgs(_state));
                        }
                    }
                    break;
                case State.Fried:
                    _burningTimer += Time.deltaTime;
                    OnProgressChanged?.Invoke(this,new IHasProgress.OnProgressChangedEventArgs(_burningTimer / _burningRecipe.BurningTimerMax));
                    if(_burningTimer >= _burningRecipe.BurningTimerMax)
                    {
                        _state = State.Burnt;
                        GetKitchenObject().DestroySelf();
                        KitchenObject.Spawn(this, _burningRecipe.Output);
                        OnStoveStateChanged?.Invoke(this, new OnStoveChangedEventArgs(_state));
                        OnProgressChanged?.Invoke(this,new IHasProgress.OnProgressChangedEventArgs(0f));
                    }
                    break;
                case State.Burnt:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public override void Interact(Player.Scripts.Player player) {
            if (HasKitchenObject()) {
                if (player.HasKitchenObject()) {
                    // Player has kitchen Object
                    if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject playerPlate)) {
                        if (playerPlate.TryAddIngredient(GetKitchenObject().ObjectData)) {
                            GetKitchenObject().DestroySelf();
                            _state = State.Idle;
                            OnStoveStateChanged?.Invoke(this, new OnStoveChangedEventArgs(_state));
                            OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs(0f));
                        }
                    }
                }
                else {
                    GetKitchenObject().SetParent(player);
                    _state = State.Idle;
                    OnStoveStateChanged?.Invoke(this, new OnStoveChangedEventArgs(_state));
                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs(0f));
                }
            }
            else {
                if (player.HasKitchenObject()) {
                    if (HasFryingRecipeWithKitchenObject(player.GetKitchenObject().ObjectData)) {
                        _state = State.Frying;
                        player.GetKitchenObject().SetParent(this);
                        OnStoveStateChanged?.Invoke(this, new OnStoveChangedEventArgs(_state));
                        _fryingRecipe = GetFryingRecipeWithInput(GetKitchenObject().ObjectData);
                        _fryingTimer = 0f;
                        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs(0f));
                    }
                }
            }
        }
        
        bool HasFryingRecipeWithKitchenObject(KitchenObjectSO kitchenObject) {
            return GetFryingRecipeWithInput(kitchenObject) != null;
        }

        private FryingRecipeSO GetFryingRecipeWithInput(KitchenObjectSO input) {
            return fryingRecipes.FirstOrDefault(recipe => recipe.Input.Name == input.Name);
        }
        
        bool HasBurningRecipeWithKitchenObject(KitchenObjectSO kitchenObject) {
            return GetBurningRecipeWithInput(kitchenObject) != null;
        }

        private BurningRecipeSO GetBurningRecipeWithInput(KitchenObjectSO input) {
            return burningRecipes.FirstOrDefault(recipe => recipe.Input.Name == input.Name);
        }

    }
}
