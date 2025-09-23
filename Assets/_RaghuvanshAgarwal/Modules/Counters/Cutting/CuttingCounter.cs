using _RaghuvanshAgarwal.Modules.Counters.Scripts;
using _RaghuvanshAgarwal.Modules.Kitchen_Objects.Scripts;
using UnityEngine;

namespace _RaghuvanshAgarwal.Modules.Counters.Cutting {
    public class CuttingCounter : BaseCounter
    {
        [SerializeField] KitchenObjectSO data;
        public override void Interact(Player.Scripts.Player player) {
            if (HasKitchenObject()) {
                if (!player.HasKitchenObject()) {
                    GetKitchenObject().SetParent(player);
                }
            }
            else {
                if (player.HasKitchenObject()) {
                    player.GetKitchenObject().SetParent(this);
                }
            }
        }

        public override void InteractAlternate(Player.Scripts.Player player) {
            if (HasKitchenObject()) {
                GetKitchenObject().DestroySelf();
                KitchenObject.Spawn(this, data);
            }
        }
    }
}
