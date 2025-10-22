using System;
using System.Collections.Generic;
using _RaghuvanshAgarwal.Modules.GameManager;
using _RaghuvanshAgarwal.Modules.Recipes.Recipe;
using UnityEngine;

namespace _RaghuvanshAgarwal.Modules.Delivery {
    public class DeliveryManagerUI : MonoBehaviour
    {
        [SerializeField] private DeliveryRecipeUI recipeUiTemplate;
        [SerializeField] private GameObject container;
        [SerializeField] private CanvasGroup canvasGroup;
        private void Start() {
            DeliveryManager.Instance.OnRecipeAdded += UpdateUI;
            DeliveryManager.Instance.OnRecipeDelivered += UpdateUI;
            
            KitchenChaoGameManager.Instance.OnStateChanged += GameManager_OnStateChanged;
            
            Hide();
        }

        

        private void OnDestroy() {
            DeliveryManager.Instance.OnRecipeAdded -= UpdateUI;
            DeliveryManager.Instance.OnRecipeDelivered -= UpdateUI;
            KitchenChaoGameManager.Instance.OnStateChanged -= GameManager_OnStateChanged;
            
        }

        

        private void GameManager_OnStateChanged(object sender, EventArgs e) {
            if (KitchenChaoGameManager.Instance.IsGamePlaying()) {
                KitchenChaoGameManager.Instance.OnGamePaused += GameManager_OnGamePaused;
                KitchenChaoGameManager.Instance.OnGameResumed += GameManager_OnGameResumed;
                Show();
            }else {
                KitchenChaoGameManager.Instance.OnGamePaused -= GameManager_OnGamePaused;
                KitchenChaoGameManager.Instance.OnGameResumed -= GameManager_OnGameResumed;
                Hide();
            }
        }
        
        private void GameManager_OnGamePaused(object sender, EventArgs e) {
            Hide();
        }

        private void GameManager_OnGameResumed(object sender, EventArgs e) {
            Show();
        }


        private void Show() {
            canvasGroup.alpha = 1;
            canvasGroup.blocksRaycasts = true;
            canvasGroup.interactable = true;
        }

        private void Hide() {
            canvasGroup.alpha = 0;
            canvasGroup.blocksRaycasts = false;
            canvasGroup.interactable = false;
        }
        
        private void UpdateUI(object sender, EventArgs e) {
            for (int i = container.transform.childCount - 1; i >= 0; i--) {
                if(container.transform.GetChild(i) == recipeUiTemplate.transform) continue;
                Destroy(container.transform.GetChild(i).gameObject);
            }
            
            Dictionary<RecipeSO, int> recipes = new Dictionary<RecipeSO, int>();
            
            foreach (RecipeSO recipeSO in DeliveryManager.Instance.WaitingRecipeList) {
                if (!recipes.TryAdd(recipeSO, 1)) {
                    recipes[recipeSO]++;
                }
            }

            foreach (KeyValuePair<RecipeSO, int> keyValuePair in recipes) {
                DeliveryRecipeUI ui = Instantiate(recipeUiTemplate, container.transform);
                ui.gameObject.SetActive(true);
                ui.Initialize(keyValuePair.Key);
                ui.SetCount(keyValuePair.Value);
            }
        }

    }
}
