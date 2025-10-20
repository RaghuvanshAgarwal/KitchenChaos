using _RaghuvanshAgarwal.Modules.Counters.Scripts;
using _RaghuvanshAgarwal.Modules.Delivery;
using _RaghuvanshAgarwal.Modules.Kitchen_Objects.Plate.Scripts;
using UnityEngine;

namespace _RaghuvanshAgarwal.Modules.Counters.Delivery.Scripts {
    public class DeliveryCounter : BaseCounter
    {
        public override void Interact(Player.Scripts.Player player) {
            if (player.HasKitchenObject()) {
                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject)) {
                    DeliveryManager.Instance.DeliverRecipe(plateKitchenObject);
                    plateKitchenObject.DestroySelf();
                }
            }
        }
    }
}
