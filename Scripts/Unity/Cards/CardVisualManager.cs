using System;
using CardGame.Core.Cards;
using CardGame.Core.Cards.Base;
using CardGame.Core.Commands.Implementations;
using CardGame.Core.Events;
using CardGame.Core.Events.Events;
using CardGame.Unity.Cards.Visuals;
using CardGame.Unity.Cards.Visuals.Hand;
using CardGame.Unity.Registries;
using UnityEngine;

namespace CardGame.Unity.Cards
{
    public class CardVisualManager : MonoBehaviour
    {
        [Header("Prefabs")]
        [SerializeField] private CardVisual cardPrefab;

        [Header("References")]
        [SerializeField] private HandManager handManager;

        [Header("Settings")]
        [SerializeField] private Transform cardParent;
        
        private void OnEnable()
        {
            // Game Events
            GameEventBus.Instance.Subscribe<GameStateEvents.CardDrawnEvent>(OnCardDrawn);
            GameEventBus.Instance.Subscribe<GameStateEvents.CardSelectedEvent>(OnCardSelected);
            GameEventBus.Instance.Subscribe<GameStateEvents.CardDiscardedEvent>(OnCardDiscarded);

            // HandManager Events
            handManager.OnClicked += OnHandSelectedChanged;
        }

        private void OnDisable()
        {
            // Game Events
            GameEventBus.Instance.Unsubscribe<GameStateEvents.CardDrawnEvent>(OnCardDrawn);
            GameEventBus.Instance.Unsubscribe<GameStateEvents.CardSelectedEvent>(OnCardSelected);

            // HandManager Events
            handManager.OnClicked -= OnHandSelectedChanged;
        }


        #region Card Management

        public CardVisual CreateCard(Card cardData)
        {
            // Create card visual
            CardVisual cardVisual = Instantiate(cardPrefab, cardParent);
            
            // Initialize
            cardVisual.Initialize(cardData);
            
            // Register
            CardRegistry.CardVisualMap.Add(cardData, cardVisual);
            
            return cardVisual;
        }
        
        #endregion
        
        #region Card State Management

        private void TryAddCardToHand(CardVisual cardVisual)
        {
            HandPlaceable handPlaceable = cardVisual.GetComponent<HandPlaceable>();
            if (handPlaceable != null)
                handManager.Register(handPlaceable);
        }
        
        private void TryRemoveCardFromHand(CardVisual cardVisual)
        {
            HandPlaceable handPlaceable = cardVisual.GetComponent<HandPlaceable>();
            if (handPlaceable != null)
                handManager.UnRegister(handPlaceable);
        }

        #endregion

        #region Event Handlers

        private void OnCardDrawn(GameStateEvents.CardDrawnEvent e)
        {
            CardVisual cardVisual = CreateCard(e.Card);
            TryAddCardToHand(cardVisual);
        }

        private void OnCardSelected(GameStateEvents.CardSelectedEvent e)
        {
            CardVisual cardVisual = null;
            if (e.Card != null)
                CardRegistry.CardVisualMap.Forward.TryGetValue(e.Card, out cardVisual);

            handManager.Selected = cardVisual?.GetComponent<HandPlaceable>();
        }
        
        private void OnCardDiscarded(GameStateEvents.CardDiscardedEvent obj)
        {
            CardRegistry.CardVisualMap.Forward.TryGetValue(obj.Card, out CardVisual cardVisual);
            if (cardVisual == null)
                return;
            
            // Unregister
            CardRegistry.CardVisualMap.Remove(obj.Card);
            TryRemoveCardFromHand(cardVisual);
            
            // Destroy
            Destroy(cardVisual.gameObject);
        }

        private void OnHandSelectedChanged(HandPlaceable handPlaceable)
        {
            Card card = null;
            if (handPlaceable != null)
            {
                CardVisual cardVisual = handPlaceable.GetComponent<CardVisual>();
                CardRegistry.CardVisualMap.Backward.TryGetValue(cardVisual, out card);
            }

            var command = new SelectCardCommand(card);
            GameController.Instance.ExecuteCommand(command);
        }

        #endregion
    }
} 