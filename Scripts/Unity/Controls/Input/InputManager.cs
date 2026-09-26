using System;
using CardGame.Core.DataStructures;
using CardGame.Unity.Controls.Input.Enums;
using CardGame.Unity.Controls.Interact.Interactables.Base;
using UnityEngine;

namespace CardGame.Unity.Controls.Input
{
    public class InputManager : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private LayerMask gameplayLayers;
        
        // References
        private static InputManager _instance;
        public static InputManager Instance
        {
            get => _instance ??= FindAnyObjectByType<InputManager>();
        }

        private Camera activeCamera;
        private InputSystem_Actions inputActions;
        
        // State
        private InputState currentState;
        public InputState CurrentState
        {
            get => currentState;
            set
            {
                SetInputState(currentState);
            }
        }

        private Vector2 lastMousePosition;
        private InteractableBase hoveredInteractable;
        
        // Events
        public PriorityEvent<Vector3> OnWorldPositionClicked = new();
        public PriorityEvent<Vector2> OnMousePositionChanged = new();
        
        public PriorityEvent<InteractableBase> OnInteractableClicked = new();
        public PriorityEvent<InteractableBase> OnInteractableHovered = new();

        #region Configuration

        private void Awake()
        {
            activeCamera = Camera.main;
            inputActions = new InputSystem_Actions(); // TODO: try moving it
            SetupInputBindings();
        }

        private void OnEnable()
        {
            inputActions.Enable();
        }

        private void OnDisable()
        {
            inputActions.Disable();
        }
        
        private void SetupInputBindings()
        {
            inputActions.Player.Click.performed += ctx => HandleClick();
            inputActions.Player.Position.performed += ctx => HandleMousePosition(ctx.ReadValue<Vector2>());
        }

        public void SetInputState(InputState newState)
        {
            currentState = newState;
            
            switch (newState)
            {
                case InputState.Game:
                    inputActions.Player.Enable();
                    inputActions.UI.Disable();
                    break;
                case InputState.UI:
                    inputActions.Player.Disable();
                    inputActions.UI.Enable();
                    break;
                case InputState.Disabled:
                    inputActions.Player.Disable();
                    inputActions.UI.Disable();
                    break;
            }
        }

        #endregion

        #region Event Handlers

        private void Update()
        {
            HandleHover();
        }

        private void HandleHover()
        {
            if (currentState != InputState.Game)
                return;

            InteractableBase hitInteractable = null;

            // Raycast
            Ray ray = activeCamera.ScreenPointToRay(inputActions.Player.Position.ReadValue<Vector2>());
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, gameplayLayers))
                hitInteractable = hit.collider.GetComponent<InteractableBase>();

            // Hover
            if (hitInteractable == hoveredInteractable)
                return;
            
            hoveredInteractable = hitInteractable;
            OnInteractableHovered?.Invoke(hitInteractable);
        }

        private void HandleMousePosition(Vector2 newPosition)
        {
            if (newPosition == lastMousePosition)
                return;

            lastMousePosition = newPosition;
            OnMousePositionChanged?.Invoke(newPosition);
        }

        private void HandleClick()
        {
            if (currentState != InputState.Game)
                return;
            
            InteractableBase hitInteractable = null;

            // Raycast
            Ray ray = activeCamera.ScreenPointToRay(inputActions.Player.Position.ReadValue<Vector2>());
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, gameplayLayers))
                hitInteractable = hit.collider.GetComponent<InteractableBase>();

            // Click
            OnInteractableClicked?.Invoke(hitInteractable);
            OnWorldPositionClicked?.Invoke(hit.point);
        }

        #endregion
    }
}