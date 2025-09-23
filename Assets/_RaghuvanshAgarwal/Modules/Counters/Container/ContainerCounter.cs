using System;
using _RaghuvanshAgarwal.Modules.Counters.Scripts;
using _RaghuvanshAgarwal.Modules.Kitchen_Objects.Scripts;
using UnityEngine;

namespace _RaghuvanshAgarwal.Modules.Counters.Container {
    public class ContainerCounter : BaseCounter {
        public event EventHandler OnPlayerGrabbedObject;
        [SerializeField] private KitchenObjectSO objectData;
        public KitchenObjectSO KitchenObject => objectData;
        
        public override void Interact(Player.Scripts.Player player) {
            if(player.HasKitchenObject()) return;
            Kitchen_Objects.Scripts.KitchenObject.Spawn(player, objectData);
            OnPlayerGrabbedObject?.Invoke(this, EventArgs.Empty);
        }
    }
}
