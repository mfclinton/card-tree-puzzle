using System.Linq;
using CardGame.Core.Cards.Base;
using CardGame.Unity.Cards.Visuals.Hand;
using CardGame.Unity.Registries;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CardGame.Unity.Cards.Visuals
{
    public class CardVisual : MonoBehaviour
    {
        [Header("UI References")]
        [SerializeField] private TextMeshPro titleTextComponent;
        [SerializeField] private TextMeshPro descriptionTextComponent;
        [SerializeField] private MeshRenderer cardMainImageComponent;
        [SerializeField] private MeshRenderer cardSecondaryImageComponent;
        
        public void Initialize(Card cardData)
        {
            // Set Card Data
            titleTextComponent.text = cardData.Name;
            descriptionTextComponent.text = string.Join("\n", cardData.Effects.Select(e => e.GetDescription()));
        }

        void OnDestroy()
        {
            CardRegistry.CardVisualMap.TryRemove(this);
        }
    }
} 