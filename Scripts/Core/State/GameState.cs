using System;
using System.Collections.Generic;
using CardGame.Core.Cards.Base;
using CardGame.Core.Cards.Effects.Interfaces;
using CardGame.Core.Cards.Enums;
using CardGame.Core.Events.Events;
using CardGame.Core.Tree.Base;
using CardGame.Core.Events;
using System.Linq;

namespace CardGame.Core.State
{
public class GameState
    {
        // Area State
        public TreeNode CurrentNode { get; private set; }
        public Dictionary<TreeNode, Dictionary<InformationType, object>> RevealedInformation { get; } = new();
        
        // Cards
        public List<Card> Hand { get; } = new();
        public List<Card> Deck { get; } = new();
        public List<Card> DiscardPile { get; } = new();
        
        // Player State
        public Card SelectedCard { get; private set; }
        
        public void InitializeGameState(TreeNode startNode, List<Card> startingDeck, int initialHandSize)
        {            
            // Deck
            Deck.Clear();
            Deck.AddRange(startingDeck);
            ShuffleDeck();
            
            // Hand
            DrawInitialHand(initialHandSize);
            
            // Area
            MoveToNode(startNode);
        }

        private void ShuffleDeck()
        {
            // Fisher-Yates shuffle
            var random = new Random();

            int n = Deck.Count;
            while (n > 1)
            {
                n--;
                int k = random.Next(n + 1);
                (Deck[k], Deck[n]) = (Deck[n], Deck[k]);
            }
        }

        private void DrawInitialHand(int initialHandSize)
        {
            for (int i = 0; i < initialHandSize; i++)
            {
                DrawCard();
            }
        }

        public void DrawCard()
        {
            if (Deck.Count == 0 && DiscardPile.Count > 0)
            {
                ReshuffleDiscard();
            }

            if (Deck.Count > 0)
            {
                Card drawnCard = Deck[0];
                Hand.Add(drawnCard);
                Deck.RemoveAt(0);
                GameEventBus.Instance.Publish(new GameStateEvents.CardDrawnEvent(drawnCard));
            }
        }

        private void ReshuffleDiscard()
        {
            Deck.AddRange(DiscardPile);
            DiscardPile.Clear();
            ShuffleDeck();
        }

        public void MoveToNode(TreeNode targetNode)
        {
            CurrentNode = targetNode;
            targetNode.OnEnter();

            // Send Event
            GameEventBus.Instance.Publish(new GameStateEvents.NodeEnteredEvent(targetNode));
        }
        
        public void PlayCard(Card card, TreeNode target)
        {
            // Apply Effects
            foreach (var effect in card.Effects)
            {
                ApplyEffect(effect, target);
            }

            // Discard
            Hand.Remove(card);
            DiscardPile.Add(card);
            GameEventBus.Instance.Publish(new GameStateEvents.CardDiscardedEvent(card));
        }
        
        public void SelectCard(Card card)
        {
            // Update Selected Card
            SelectedCard = SelectedCard == card ? null : card;
            
            // Alert Valid Targets
            IEnumerable<TreeNode> validTargets = SelectedCard == null 
                ? new List<TreeNode>() 
                : GetValidTargets(SelectedCard);
            
            GameEventBus.Instance.Publish(new GameStateEvents.CardSelectedEvent(SelectedCard, validTargets));
        }
        
        public void SelectNode(TreeNode node)
        {
            bool cardSelected = SelectedCard != null;
            if (cardSelected)
            {
                bool isValidTarget = SelectedCard.ValidateTarget(node, this);
                if (!isValidTarget)
                    return;
                
                PlayCard(SelectedCard, node);
            }
            else if (node != CurrentNode)
            {
                MoveToNode(node);
                DrawCard();
            }
        }

        private void ApplyEffect(ICardEffect effect, TreeNode target)
        {
            // World Effect
            if (effect is IWorldEffect worldEffect)
            {
                worldEffect.ApplyEffect(target, this);
            }
                
            // Information Effect
            else if (effect is IInformationEffect informationEffect)
            {
                var information = informationEffect.GetInformation(target, this);
                GameEventBus.Instance.Publish(new GameStateEvents.InformationGainedEvent(information));
            }
        }

        public IEnumerable<TreeNode> GetValidTargets(Card card)
        {
            var validTargets = CurrentNode.Children.Where(child => card.ValidateTarget(child, this));
            if (card.ValidateTarget(CurrentNode, this))
                validTargets = validTargets.Append(CurrentNode);

            return validTargets;
        }
    }
}