using _RaghuvanshAgarwal.Modules.Kitchen_Objects.Plate.Scripts;
using _RaghuvanshAgarwal.Modules.Kitchen_Objects.Scripts;
using _RaghuvanshAgarwal.Modules.Recipes.Recipe;
using TMPro;
using UnityEngine;

namespace _RaghuvanshAgarwal.Modules.Delivery {
    public class DeliveryRecipeUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI recipeName;
        [SerializeField] private TextMeshProUGUI countLabel;
        [SerializeField] private PlateIconController iconTemplate;
        [SerializeField] private Transform iconContainer;
        

        public void Initialize(RecipeSO recipe) {
            recipeName.text = recipe.name;
            foreach (KitchenObjectSO recipeIngredient in recipe.ingredients) {
                PlateIconController iconController = Instantiate(iconTemplate, iconContainer);
                iconController.gameObject.SetActive(true);
                iconController.SetKitchenObjectSO(recipeIngredient);
            }
        }

        public void SetCount(int count) {
            countLabel.text = count.ToString();
        }
    }
}
