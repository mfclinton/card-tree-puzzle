using CardGame.Core.Cards.Enums;
using CardGame.Core.Events;
using CardGame.Core.Events.Events;
using CardGame.Unity.Registries;
using CardGame.Core.Tree.Base;
using CardGame.Unity.Nodes.Visuals;
using TMPro;
using UnityEngine;

namespace CardGame.Unity.Information
{
    public class InformationManager : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private TextMeshProUGUI informationText;
        
        private void OnEnable()
        {
            // Game Events
            GameEventBus.Instance.Subscribe<GameStateEvents.NodeEnteredEvent>(OnNodeEntered);
            GameEventBus.Instance.Subscribe<GameStateEvents.InformationGainedEvent>(OnInformationGained);
        }
        
        private void OnDisable()
        {
            // Game Events
            GameEventBus.Instance.Unsubscribe<GameStateEvents.NodeEnteredEvent>(OnNodeEntered);
            GameEventBus.Instance.Unsubscribe<GameStateEvents.InformationGainedEvent>(OnInformationGained);
        }

        private void OnNodeEntered(GameStateEvents.NodeEnteredEvent e)
        {
            informationText.text = $"Node Entered: {e.Node.Type}";
        }

        private void OnInformationGained(GameStateEvents.InformationGainedEvent e)
        {
            // Get Info
            InformationType informationType = e.Information.Type;
            var informationValue = e.Information.Value;
            if (informationType == InformationType.Subtree && informationValue != null)
            {
                TreeNodeVisual treeNodeVisual = TreeNodeRegistry.NodeVisualMap.Forward[(TreeNode)informationValue];
                informationValue = treeNodeVisual.name;
            }
            
            // Update UI
            informationText.text = $"Information Gained: {informationType} - {informationValue}";
        }
    }
}