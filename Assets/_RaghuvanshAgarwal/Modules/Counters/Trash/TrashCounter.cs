using System;
using _RaghuvanshAgarwal.Modules.Counters.Scripts;
using UnityEngine;

namespace _RaghuvanshAgarwal.Modules.Counters.Trash {
    public class TrashCounter : BaseCounter {
        public static event EventHandler OnAnyTrashDropped;
        public override void Interact(Player.Scripts.Player player) {
            if (!player.HasKitchenObject()) return;
            player.GetKitchenObject().DestroySelf();
            OnAnyTrashDropped?.Invoke(this, EventArgs.Empty);
        }
    }
}
