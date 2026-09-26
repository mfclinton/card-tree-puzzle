using System;
using CardGame.Core.Events;
using CardGame.Core.Events.Events;
using CardGame.Unity.Nodes.Visuals;
using CardGame.Unity.Registries;
using UnityEngine;
using CardGame.Core.Tree.Base;
using CardGame.Unity.Nodes.Visuals.Targets;
using CardGame.Core.Commands;
using CardGame.Core.Commands.Implementations;

namespace CardGame.Unity.Nodes
{
    public class TreeNodeVisualManager : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private TreeNodeVisual currentNodeVisual;
        [SerializeField] private TreeNodeVisual[] childNodeVisuals;
        [SerializeField] private TreeNodeVisualTargetManager targetManager;

        [Header("Visual Configurations")]
        [SerializeField] private TreeNodeVisualConfig defaultConfig;
        [SerializeField] private TreeNodeVisualConfig highlightedConfig;

        private void OnEnable()
        {
            // Game Events
            GameEventBus.Instance.Subscribe<GameStateEvents.NodeEnteredEvent>(HandleNodeEntered);
            GameEventBus.Instance.Subscribe<GameStateEvents.CardSelectedEvent>(HandleCardSelected);
            
            // TreeNodeVisualTargetManager Events
            targetManager.OnClicked += OnTargetSelectedChanged;
        }
        
        private void OnDisable()
        {
            // Game Events
            GameEventBus.Instance.Unsubscribe<GameStateEvents.NodeEnteredEvent>(HandleNodeEntered);
            GameEventBus.Instance.Unsubscribe<GameStateEvents.CardSelectedEvent>(HandleCardSelected);
            
            // TreeNodeVisualTargetManager Events
            targetManager.OnClicked -= OnTargetSelectedChanged;
        }

        #region Event Handlers

        private void HandleNodeEntered(GameStateEvents.NodeEnteredEvent e)
        {
            TreeNodeRegistry.NodeVisualMap.Clear();

            // Current Node Visual
            currentNodeVisual.SetActive(true);
            currentNodeVisual.SetConfig(defaultConfig);
            TreeNodeRegistry.NodeVisualMap.Add(e.Node, currentNodeVisual);

            // Child Node Visuals
            for (int i = 0; i < childNodeVisuals.Length; i++)
            {
                var nodeVisual = childNodeVisuals[i];
                bool isActiveNode = i < e.Node.Children.Count;
                
                nodeVisual.SetActive(isActiveNode);
                nodeVisual.SetConfig(defaultConfig);
                
                if (isActiveNode)
                {
                    TreeNodeRegistry.NodeVisualMap.Add(e.Node.Children[i], nodeVisual);
                }
            }
        }

        private void HandleCardSelected(GameStateEvents.CardSelectedEvent e)
        {
            // Reset
            currentNodeVisual.TransitionToConfig(defaultConfig);

            foreach (var nodeVisual in childNodeVisuals)
            {
                nodeVisual.TransitionToConfig(defaultConfig);
            }

            // Highlight
            foreach (TreeNode validTarget in e.ValidTargets)
            {
                if (TreeNodeRegistry.NodeVisualMap.Forward.TryGetValue(validTarget, out TreeNodeVisual nodeVisual))
                {
                    nodeVisual.TransitionToConfig(highlightedConfig);
                }
            }
        }
        
        private void OnTargetSelectedChanged(TreeNodeVisualTarget target)
        {
            if (target == null)
                return;

            // Get Data
            TreeNodeVisual nodeVisual = target.ParentTreeNodeVisual;
            TreeNodeRegistry.NodeVisualMap.Backward.TryGetValue(nodeVisual, out TreeNode node);

            if (node == null)
                return;

            // Trigger
            var command = new SelectNodeCommand(node);
            GameController.Instance.ExecuteCommand(command);
        }

        #endregion
    }
} 