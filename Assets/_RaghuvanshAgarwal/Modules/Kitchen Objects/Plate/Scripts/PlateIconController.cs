using _RaghuvanshAgarwal.Modules.Kitchen_Objects.Scripts;
using UnityEngine;
using UnityEngine.UI;

namespace _RaghuvanshAgarwal.Modules.Kitchen_Objects.Plate.Scripts {
    public class PlateIconController : MonoBehaviour
    {
        [SerializeField] private Image icon;

        public void SetKitchenObjectSO(KitchenObjectSO kitchenObjectSO) {
            icon.sprite = kitchenObjectSO.Icon;
        }
    }
}
