using CardGame.Core.Events;
using CardGame.Core.Events.Events;
using CardGame.Core.Tree.Base;
using CardGame.Unity.Nodes.UI;
using CardGame.Unity.Registries;
using CardGame.Core.Tree.Enums;
using UnityEngine;
using System.Collections.Generic;
using CardGame.Core.Tree.Helpers;

namespace CardGame.Unity.Nodes
{
    public class TreeNodeUIManager : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Transform nodeUIGroupParent;

        [Header("Prefabs")]
        [SerializeField] private GameObject nodeUIGroupPrefab;
        [SerializeField] private TreeNodeUI nodeUIPrefab;

        [Header("Settings")]
        [SerializeField] private int depth = 2;
        
        [Header("Visual Configurations")]
        [SerializeField] private TreeNodeUIConfig defaultConfig;
        [SerializeField] private TreeNodeUIConfig monsterConfig;
        [SerializeField] private TreeNodeUIConfig exitConfig;
        
        private void OnEnable()
        {
            // Game Events
            GameEventBus.Instance.Subscribe<GameStateEvents.NodeEnteredEvent>(HandleNodeEntered, priority: 1);
        }
        
        private void OnDisable()
        {
            // Game Events
            GameEventBus.Instance.Unsubscribe<GameStateEvents.NodeEnteredEvent>(HandleNodeEntered);
        }
        
        private void HandleNodeEntered(GameStateEvents.NodeEnteredEvent e)
        {
            // Clean Up
            TreeNodeRegistry.NodeUIMap.Clear();

            foreach (Transform child in nodeUIGroupParent)
                Destroy(child.gameObject);
            
            // Create UI Parents
            Transform[] nodeUIGroupParents = new Transform[depth + 1];
            for (int i = 0; i < depth + 1; i++)
            {
                nodeUIGroupParents[i] = Instantiate(nodeUIGroupPrefab, nodeUIGroupParent).transform;
                nodeUIGroupParents[i].name = $"Node UIGroup {i}";
            }

            // Create UI
            foreach (var treeNodeDepthResult in TreeNodeHelpers.GetNodesWithinDepthWithDistance(e.Node, depth))
            {
                TreeNode node = treeNodeDepthResult.Item1;
                int distance = treeNodeDepthResult.Item2;

                GameObject nodeUIGroup = CreateNodeUIGroup(node.Children);
                nodeUIGroup.transform.SetParent(nodeUIGroupParents[distance]);
            }
        }

        private GameObject CreateNodeUIGroup(IEnumerable<TreeNode> nodes)
        {
            GameObject dummyParent = new GameObject("Dummy Parent");
            dummyParent.AddComponent<RectTransform>();

            GameObject nodeUIGroup = Instantiate(nodeUIGroupPrefab, dummyParent.transform);
            foreach (var node in nodes)
            {
                // Create Node UI
                TreeNodeUI nodeUI = Instantiate(nodeUIPrefab, nodeUIGroup.transform);
                
                // Order
                nodeUI.transform.SetSiblingIndex(1); // TODO: Temp
                
                // Set Config
                TreeNodeUIConfig config;
                switch (node.Type)
                {
                    case NodeType.Monster:
                        config = monsterConfig;
                        break;
                    case NodeType.Exit:
                        config = exitConfig;
                        break;
                    default:
                        config = defaultConfig;
                        break;
                }
                
                nodeUI.SetConfig(config);
                
                // Register Node UI
                TreeNodeRegistry.NodeUIMap.Add(node, nodeUI);
            }

            return dummyParent;
        }
    }
}