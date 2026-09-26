using System;
using System.Collections.Generic;
using System.Linq;
using CardGame.Unity.Controls.Input;
using CardGame.Unity.Controls.Interact.InteractableManagers.Base;
using CardGame.Unity.Controls.Interact.Interactables.Base;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Splines;

namespace CardGame.Unity.Cards.Visuals.Hand
{
    public class HandManager : InteractableManagerBase<HandPlaceable>
    {
        [Header("References")]
        [SerializeField] private SplineContainer splineContainer;
        
        [Header("Tween Settings")]
        [SerializeField] private float handTimeToMove = 0.25f;
        [SerializeField] private float handTimeToRotate = 0.25f;
        
        [Header("Offset Settings")]
        [SerializeField] private float heightOffsetSelected = 0.5f;
        [SerializeField] private float heightOffsetHovered = 0.25f;

        // State
        private HandPlaceable selected;
        public HandPlaceable Selected
        {
            get => selected;
            set
            {
                selected = value;
                UpdateHandPositions();
            }
        }
        
        #region Visualization Functions
        
        [Button("Update Hand Positions")]
        public void UpdateHandPositions()
        {
            if(managedComponents.Count == 0)
                return;
            
            Spline spline = splineContainer.Spline;
            
            // Setup
            float spacing = 1f / managedComponents.Count;
            float firstP = 0.5f - (spacing * (managedComponents.Count - 1) / 2);

            int index = 0;
            foreach (var placeable in managedComponents)
            {
                // Calculate
                float p = firstP + index * spacing;
                                
                Vector3 splineForward = spline.EvaluateTangent(p);
                Vector3 splineUp = spline.EvaluateUpVector(p);
                Quaternion splineRotation = Quaternion.LookRotation(splineUp, Vector3.Cross(splineForward, splineUp));
                
                float heightOffset = GetHeightOffset(placeable);
                Vector3 splinePosition = (Vector3)spline.EvaluatePosition(p);
                splinePosition += splineContainer.transform.position + splineUp * heightOffset;

                // Trigger
                placeable.transform.DOMove(splinePosition, handTimeToMove);
                placeable.transform.DOLocalRotateQuaternion(splineRotation, handTimeToRotate);

                index++;
            }
        }
        
        #endregion

        #region Helpers

        private float GetHeightOffset(HandPlaceable placeable)
        {
            if (placeable == selected)
                return heightOffsetSelected;
            else if (placeable == hovered)
                return heightOffsetHovered;

            return 0f;
        }

        #endregion
        
        #region InteractableManagerBase Implementation

        protected override void OnInteractableClicked(HandPlaceable placeable)
        {
            UpdateHandPositions();
        }
        
        protected override void OnInteractableHovered(HandPlaceable placeable)
        {
            UpdateHandPositions();
        }
        
        #endregion
    }
}
