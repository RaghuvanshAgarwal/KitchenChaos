using System;
using UnityEngine;
using UnityEngine.UI;

namespace _RaghuvanshAgarwal.Modules.Kitchen_Objects.Plate.Scripts {
    public class PlateIconsUI : MonoBehaviour
    {
        [SerializeField] PlateKitchenObject plate;
        [SerializeField] PlateIconController template;

        private void Start() {
            plate.OnIngredientAddedOnPlate += PlateOnIngredientAddedOnPlate;
        }

        private void OnDestroy() {
            plate.OnIngredientAddedOnPlate -= PlateOnIngredientAddedOnPlate;
        }

        private void PlateOnIngredientAddedOnPlate(object sender, IngredientAddedOnPlateEventArgs e) {
            PlateIconController plateIconController = Instantiate(template, transform);
            plateIconController.gameObject.SetActive(true);
            plateIconController.SetKitchenObjectSO(e.Ingredient);
        }
    }
}
