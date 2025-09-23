using System;
using _RaghuvanshAgarwal.Modules.Counters.Clear;
using _RaghuvanshAgarwal.Modules.Counters.Scripts;
using _RaghuvanshAgarwal.Modules.Kitchen_Objects.Scripts;
using UnityEngine;

namespace _RaghuvanshAgarwal.Modules.Player.Scripts {

    public class OnSelectedCounterChangedEventArgs : EventArgs {
        public readonly BaseCounter SelectedCounter;

        public OnSelectedCounterChangedEventArgs(BaseCounter selectedCounter) {
            SelectedCounter = selectedCounter;
        }
    }
    
    public class Player : MonoBehaviour, IKitchenObjectParent {
        [SerializeField] private float moveSpeed = 5f;
        [SerializeField] private GameInput gameInput;
        [SerializeField] private LayerMask counterLayerMask;
        [SerializeField] private Transform kitchenObjectFollowTransform;

        public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;
        public static Player Instance { get; private set; }

        private bool _isWalking = false;
        private Vector3 _lastInteractionDirection;
        private BaseCounter _selectedCounter;
        private KitchenObject _kitchenObject;

        private void Awake() {
            if (Instance != null) {
                Debug.LogError("There can only be one instance of Player!");
                Destroy(gameObject);
            }
            Instance = this;
        }

        private void Start() {
            gameInput.OnInteractAction += GameInputOnInteractAction;
        }
        
        private void Update() {
            HandleMovement();
            HandleInteractions();
        }
        private void OnDestroy() {
            gameInput.OnInteractAction -= GameInputOnInteractAction;
        }


        public bool IsWalking() {
            return _isWalking;
        }
        
        
        private void GameInputOnInteractAction(object sender, EventArgs e) {
            if (_selectedCounter != null) {
                _selectedCounter.Interact(this);
            }
        }


        private void HandleInteractions() {
            Vector2 inputVector = gameInput.GetMovementVectorNormalized();
            Vector3 movDir = new Vector3(inputVector.x, 0, inputVector.y);

            if (movDir != Vector3.zero) {
                _lastInteractionDirection = movDir;
            }
            
            const float interactionDistance = 2f;
            if (Physics.Raycast(transform.position, _lastInteractionDirection, out RaycastHit hit, interactionDistance, counterLayerMask)) {
                if (hit.transform.TryGetComponent(out BaseCounter counter)) {
                    if (counter != _selectedCounter) {
                        SetSelectedCounter(counter);
                    }
                }
                else {
                    _selectedCounter = null;
                    SetSelectedCounter(null);
                }
            }
            else {
                _selectedCounter = null;
                SetSelectedCounter(null);
            }
        }

        private void HandleMovement() {
            Vector2 inputVector = gameInput.GetMovementVectorNormalized();
            Vector3 movDir = new Vector3(inputVector.x, 0, inputVector.y);
            float maxDistance = moveSpeed * Time.deltaTime;
            const float playerHeight = 2f;
            const float playerRadius = 0.7f;
            bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, movDir, maxDistance);
            if (!canMove) {
                Vector3 xAttempt = new Vector3(movDir.x, 0, 0).normalized;
                canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, xAttempt, maxDistance);
                if (canMove) {
                    movDir = xAttempt;
                }
                else {
                    Vector3 zAttempt = new Vector3(0, 0, movDir.z).normalized;
                    canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, zAttempt, maxDistance);
                    if (canMove) {
                        movDir = zAttempt;
                    }
                }
            }

            if (_isWalking) {
                const float rotationSpeed = 10f;
                transform.forward = Vector3.Slerp(transform.forward, movDir, Time.deltaTime * rotationSpeed);
            }

            if (!canMove) return;
            transform.position += movDir * (moveSpeed * Time.deltaTime);
            _isWalking = movDir != Vector3.zero;
        }
        
        private void SetSelectedCounter(BaseCounter selectedCounter) {
            _selectedCounter = selectedCounter;
            OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs(_selectedCounter));
        }

        public Transform GetKitchenObjectFollowTransform() => kitchenObjectFollowTransform;
        public void SetKitchenObject(KitchenObject kitchenObject) => _kitchenObject = kitchenObject;
        public KitchenObject GetKitchenObject() => _kitchenObject;
        public void ClearKitchenObject() => _kitchenObject = null;
        public bool HasKitchenObject() => _kitchenObject != null;
    }
}