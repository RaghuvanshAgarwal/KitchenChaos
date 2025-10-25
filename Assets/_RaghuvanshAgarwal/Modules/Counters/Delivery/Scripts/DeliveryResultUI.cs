using System;
using _RaghuvanshAgarwal.Modules.Delivery;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _RaghuvanshAgarwal.Modules.Counters.Delivery.Scripts {
    public class DeliveryResultUI : MonoBehaviour
    {
        private static readonly int Popup = Animator.StringToHash("Popup");
        [SerializeField] Image background;
        [SerializeField] Image icon;
        [SerializeField] TextMeshProUGUI text;
        
        [SerializeField] Color successColor;
        [SerializeField] Color failureColor;
        [SerializeField] Sprite successSprite;
        [SerializeField] Sprite failureSprite;
        
        private Animator _animator;

        private void Awake() {
            _animator =  GetComponent<Animator>();
        }

        private void Start() {
            DeliveryManager.Instance.OnCorrectRecipeDelivered += DeliveryManager_OnCorrectRecipeDelivered;
            DeliveryManager.Instance.OnWrongRecipeDelivered += DeliveryManager_OnWrongRecipeDelivered;
            Hide();
        }

        private void OnDestroy() {
            DeliveryManager.Instance.OnCorrectRecipeDelivered -= DeliveryManager_OnCorrectRecipeDelivered;
            DeliveryManager.Instance.OnWrongRecipeDelivered -= DeliveryManager_OnWrongRecipeDelivered;
        }

        private void DeliveryManager_OnWrongRecipeDelivered(object sender, EventArgs e) {
            background.color = failureColor;
            icon.sprite = failureSprite;
            text.text = "Delivery\nFailed";
            _animator.SetTrigger(Popup);
            Show();
        }

        private void DeliveryManager_OnCorrectRecipeDelivered(object sender, EventArgs e) {
            background.color = successColor;
            icon.sprite = successSprite;
            text.text = "Delivery\nSuccess";
            _animator.SetTrigger(Popup);
            Show();
        }

        private void Show() {
            gameObject.SetActive(true);
        }

        private void Hide() {
            gameObject.SetActive(false);
        }
    }
}
