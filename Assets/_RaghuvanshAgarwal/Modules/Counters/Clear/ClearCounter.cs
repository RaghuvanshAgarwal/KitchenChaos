using _RaghuvanshAgarwal.Modules.Counters.Scripts;
using _RaghuvanshAgarwal.Modules.Kitchen_Objects.Scripts;
using UnityEngine;

namespace _RaghuvanshAgarwal.Modules.Counters.Clear {
    public class ClearCounter : BaseCounter {
        [SerializeField] private KitchenObjectSO objectData;
        
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
    }
}
