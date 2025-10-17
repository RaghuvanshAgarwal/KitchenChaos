using System;
using System.Collections.Generic;
using System.Linq;
using _RaghuvanshAgarwal.Modules.Kitchen_Objects.Scripts;
using UnityEngine;

namespace _RaghuvanshAgarwal.Modules.Kitchen_Objects.Plate.Scripts {
    public class PlateCompleteVisual : MonoBehaviour {
        [Serializable]
        struct IngredientVisualData {
            public KitchenObjectSO objectData;
            public GameObject visual;
        }
        [SerializeField] private PlateKitchenObject plateKitchenObject;
        [SerializeField] private List<IngredientVisualData> ingredientVisualData;

        private void Awake() {
            foreach (IngredientVisualData data in ingredientVisualData) {
                data.visual.SetActive(false);
            }
        }

        private void Start() {
            plateKitchenObject.OnIngredientAddedOnPlate += PlateKitchenObjectOnIngredientAddedOnPlate;
        }

        private void OnDestroy() {
            plateKitchenObject.OnIngredientAddedOnPlate -= PlateKitchenObjectOnIngredientAddedOnPlate;
        }

        private void PlateKitchenObjectOnIngredientAddedOnPlate(object sender, IngredientAddedOnPlateEventArgs e) {
            IngredientVisualData data = ingredientVisualData.Find(x => x.objectData == e.Ingredient);
            if (ingredientVisualData.Any(i => i.objectData == e.Ingredient)) {
                data.visual.SetActive(true);
            }
        }
    }
}
