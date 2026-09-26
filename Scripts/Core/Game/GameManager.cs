using System;
using System.Collections.Generic;
using System.Linq;
using CardGame.Core.Cards.Base;
using CardGame.Core.Commands;
using CardGame.Core.Commands.Base;
using CardGame.Core.Deck;
using CardGame.Core.Events.Events;
using CardGame.Core.State;
using CardGame.Core.Tree.Base;
using CardGame.Core.Tree.Generator.Interfaces;

namespace CardGame.Core.Game
{
    public class GameManager
    {
        // Initializers
        private readonly ITreeGenerator _treeGenerator;
        private readonly IDeckBuilder _deckBuilder;

        // Game State
        public GameState state { get; private set; }

        public GameManager(ITreeGenerator treeGenerator, IDeckBuilder deckBuilder)
        {
            // Set Initializers
            _treeGenerator = treeGenerator;
            _deckBuilder = deckBuilder;
            
            // Initialize State
            state = new GameState();
        }

        public void StartNewGame(GameManagerConfig managerConfig)
        {
            // Setup Game
            var tree = _treeGenerator.GenerateTree(managerConfig.TreeConfig);
            var startingDeck = _deckBuilder.BuildStartingDeck(managerConfig.DeckConfig);
            
            // Start Game
            state.InitializeGameState(tree, startingDeck, managerConfig.InitialHandSize);
        }
    }
}