using _RaghuvanshAgarwal.Modules.Counters.Scripts;
using _RaghuvanshAgarwal.Modules.Kitchen_Objects.Plate.Scripts;
using _RaghuvanshAgarwal.Modules.Kitchen_Objects.Scripts;
using UnityEngine;

namespace _RaghuvanshAgarwal.Modules.Counters.Clear {
    public class ClearCounter : BaseCounter {
        [SerializeField] private KitchenObjectSO objectData;
        
        public override void Interact(Player.Scripts.Player player) {
            if (HasKitchenObject()) {
                // Counter has kitchen object
                if (player.HasKitchenObject()) {
                    // Player has kitchen Object
                    if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject playerPlate)) {
                        if (playerPlate.TryAddIngredient(GetKitchenObject().ObjectData)) {
                            GetKitchenObject().DestroySelf();
                        }
                    }
                    else if (GetKitchenObject().TryGetPlate(out PlateKitchenObject counterPlate)) {
                        // Counter has Plate on it
                        if (counterPlate.TryAddIngredient(player.GetKitchenObject().ObjectData)) {
                            player.GetKitchenObject().DestroySelf();
                        }
                    }
                }
                else {
                    // Player Does not have kitchen object
                    GetKitchenObject().SetParent(player);
                }
            }
            else {
                // Counter does not have object
                if (player.HasKitchenObject()) {
                    // Player has kitchen object
                    player.GetKitchenObject().SetParent(this);
                }
            }
        }
    }
}
