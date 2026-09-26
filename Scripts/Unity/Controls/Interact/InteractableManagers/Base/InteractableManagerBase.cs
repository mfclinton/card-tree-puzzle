using System;
using System.Reflection;
using CardGame.Core.DataStructures;
using CardGame.Unity.Controls.Input;
using CardGame.Unity.Controls.Interact.Interactables.Base;
using UnityEngine;

namespace CardGame.Unity.Controls.Interact.InteractableManagers.Base
{
    public abstract class InteractableManagerBase<T> : MonoBehaviour where T : InteractableBase
    {
        [Header("Settings")]
        [SerializeField] private bool processClickEvents = true;
        [SerializeField] private bool processHoverEvents = true;
        
        // State
        protected T hovered;
        protected T previousHovered;
        
        // Events
        public Action<T> OnClicked;
        public Action<T> OnHoveredChanged;

        // Priority
        protected virtual int ClickPriority => 0;
        protected virtual int HoverPriority => 0;
        
        // Internal
        protected OrderedSet<T> managedComponents = new();

        #region Initialization
        
        protected virtual void OnEnable()
        {
            // Get Method Info
            var classType = GetType();
            var clickedMethod = classType.GetMethod(nameof(OnInteractableClicked), BindingFlags.Instance | BindingFlags.NonPublic);
            var hoveredMethod = classType.GetMethod(nameof(OnInteractableHovered), BindingFlags.Instance | BindingFlags.NonPublic);

            // Subscribe To Events
            if (clickedMethod.DeclaringType != typeof(InteractableManagerBase<T>))
                InputManager.Instance.OnInteractableClicked.AddWithPriority(PreProcessInteractableClicked, ClickPriority);

            if (hoveredMethod.DeclaringType != typeof(InteractableManagerBase<T>))
                InputManager.Instance.OnInteractableHovered.AddWithPriority(PreProcessInteractableHovered, HoverPriority);
        }

        protected virtual void OnDisable()
        {
            // Get Method Info
            var classType = GetType();
            var clickedMethod = classType.GetMethod(nameof(OnInteractableClicked), BindingFlags.Instance | BindingFlags.NonPublic);
            var hoveredMethod = classType.GetMethod(nameof(OnInteractableHovered), BindingFlags.Instance | BindingFlags.NonPublic);

            // Unsubscribe From Events
            if (clickedMethod.DeclaringType != typeof(InteractableManagerBase<T>))
                InputManager.Instance.OnInteractableClicked -= PreProcessInteractableClicked;

            if (hoveredMethod.DeclaringType != typeof(InteractableManagerBase<T>))
                InputManager.Instance.OnInteractableHovered -= PreProcessInteractableHovered;
        }

        public virtual void Register(T component)
        {
            managedComponents.Add(component);
        }

        public virtual void UnRegister(T component)
        {
            managedComponents.Remove(component);
        }

        #endregion
        
        #region Interaction Events

        protected virtual void OnInteractableClicked(T target) { }
        protected virtual void OnInteractableHovered(T target) { }
        
        #endregion

        #region Processing
        
        protected virtual void PreProcessInteractableClicked(InteractableBase target)
        {
            if (!processClickEvents)
                return;
            
            var interactable = target as T;
            if (interactable != null && !interactable.IsInteractable)
                interactable = null;
            
            // Trigger
            OnInteractableClicked(interactable);
            
            // Event
            OnClicked?.Invoke(interactable);
        }
        
        protected virtual void PreProcessInteractableHovered(InteractableBase target)
        {
            if (!processHoverEvents)
                return;
            
            var interactable = target as T;
            if (interactable != null && !interactable.IsInteractable)
                interactable = null;
            
            // Set
            previousHovered = hovered;
            hovered = interactable;
            
            // Trigger
            OnInteractableHovered(interactable);
            
            // Event
            OnHoveredChanged?.Invoke(interactable);
        }

        #endregion
    }
}