using System;
using CardGame.Unity.Controls.Interact.Interactables.Base;
using UnityEngine;

namespace CardGame.Unity.Nodes.Visuals.Targets
{
    public class TreeNodeVisualTarget : InteractableBase
    {
        [SerializeField] private TreeNodeVisual parentTreeNodeVisual;
        public TreeNodeVisual ParentTreeNodeVisual => parentTreeNodeVisual;

        private void OnValidate()
        {
            if (parentTreeNodeVisual == null)
            {
                parentTreeNodeVisual = GetComponentInParent<TreeNodeVisual>();
            }
        }
    }
}