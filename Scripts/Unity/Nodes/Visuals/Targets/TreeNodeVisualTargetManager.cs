using CardGame.Unity.Controls.Interact.InteractableManagers.Base;
using UnityEngine;

namespace CardGame.Unity.Nodes.Visuals.Targets
{
    public class TreeNodeVisualTargetManager : InteractableManagerBase<TreeNodeVisualTarget>
    {
        // Event Priority
        protected override int ClickPriority => -1;

        #region InteractableManagerBase Implementation

        protected override void OnInteractableClicked(TreeNodeVisualTarget target)
        {
            
        }
        
        protected override void OnInteractableHovered(TreeNodeVisualTarget target)
        {
            
        }
        
        #endregion
    }
}