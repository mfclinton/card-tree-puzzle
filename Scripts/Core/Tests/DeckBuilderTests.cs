using System.Collections.Generic;
using CardGame.Core.Cards.Base;
using CardGame.Core.Cards.Effects.Interfaces;
using CardGame.Core.Cards.Enums;
using CardGame.Core.Deck;
using NUnit.Framework;

namespace CardGame.Core.Tests
{
    public class DeckBuilderTests
    {
        private DeckBuilder _deckBuilder;
        private DeckConfig _deckConfig;
        private Card _testCard;

        [SetUp]
        public void Setup()
        {
            _deckBuilder = new DeckBuilder();
            _deckConfig = new DeckConfig();
            _testCard = new Card("Test Card", TargetType.All, new List<ICardEffect>());
        }

        [Test]
        public void BuildStartingDeck_EmptyConfig_ReturnsEmptyDeck()
        {
            // Arrange
            var deck = _deckBuilder.BuildStartingDeck(_deckConfig);
            
            // Assert
            Assert.That(deck, Is.Empty);
        }

        [Test]
        public void BuildStartingDeck_SingleCard_ReturnsCorrectCount()
        {
            // Arrange
            const int expectedCount = 3;
            _deckConfig.AddCard(_testCard, expectedCount);

            // Act
            var deck = _deckBuilder.BuildStartingDeck(_deckConfig);
            
            // Assert
            Assert.That(deck, Has.Count.EqualTo(expectedCount));
        }

        [Test]
        public void BuildStartingDeck_MultipleCards_ReturnsCorrectTotalCount()
        {
            // Arrange
            var testCard2 = new Card("Test Card 2", TargetType.Location, new List<ICardEffect>());
            
            _deckConfig.AddCard(_testCard, 2);
            _deckConfig.AddCard(testCard2, 3);

            // Act
            var deck = _deckBuilder.BuildStartingDeck(_deckConfig);
            
            // Assert
            Assert.That(deck, Has.Count.EqualTo(5));
        }

        [Test]
        public void BuildStartingDeck_CreatesNewInstances()
        {
            // Arrange
            _deckConfig.AddCard(_testCard, 2);

            // Act
            var deck = _deckBuilder.BuildStartingDeck(_deckConfig);
            
            // Assert
            Assert.That(deck[0], Is.Not.SameAs(deck[1]), "Cards should be different instances");
            Assert.That(deck[0].Name, Is.EqualTo(deck[1].Name), "Cards should have same properties");
        }
    }
} 