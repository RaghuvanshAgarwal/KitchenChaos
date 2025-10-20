using System;
using System.Collections.Generic;
using _RaghuvanshAgarwal.Modules.Recipes.Recipe;
using UnityEngine;

namespace _RaghuvanshAgarwal.Modules.Delivery {
    public class DeliveryManagerUI : MonoBehaviour
    {
        [SerializeField] private DeliveryRecipeUI recipeUiTemplate;
        [SerializeField] private GameObject container;
        private void Start() {
            DeliveryManager.Instance.OnRecipeAdded += UpdateUI;
            DeliveryManager.Instance.OnRecipeDelivered += UpdateUI;
        }
        
        private void OnDestroy() {
            DeliveryManager.Instance.OnRecipeAdded -= UpdateUI;
            DeliveryManager.Instance.OnRecipeDelivered -= UpdateUI;
        }
        private void UpdateUI(object sender, EventArgs e) {
            for (int i = container.transform.childCount - 1; i >= 0; i--) {
                if(container.transform.GetChild(i) == recipeUiTemplate.transform) continue;
                Destroy(container.transform.GetChild(i).gameObject);
            }

            foreach (RecipeSO recipeSO in DeliveryManager.Instance.WaitingRecipeList) {
                DeliveryRecipeUI ui = Instantiate(recipeUiTemplate, container.transform);
                ui.gameObject.SetActive(true);
                ui.Initialize(recipeSO);
            }
        }

    }
}
