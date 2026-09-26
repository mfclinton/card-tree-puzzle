using System;
using CardGame.Core.Commands.Base;
using CardGame.Core.Deck;
using CardGame.Core.Events.Events;
using CardGame.Core.Game;
using UnityEngine;
using CardGame.Core.State;
using CardGame.Core.Tree.Generator;
using CardGame.Core.Tree.Generator.Settings;
using CardGame.Unity.ScriptableObjects.Cards;
using CardGame.Core.Events;
using CardGame.Core.Commands;

namespace CardGame.Unity
{
    public class GameController : MonoBehaviour
    {
        // Config
        [SerializeField] private DeckConfigSO startingDeckConfig;
        [SerializeReference] private GenerationConfig treeGenerationConfig;
        [SerializeField] private int initialHandSize = 5;
        
        // References
        private static GameController _instance;
        public static GameController Instance
        {
            get => _instance ??= FindAnyObjectByType<GameController>();
        }

        // State
        private GameManager _gameManager;

        private void Awake()
        {
            var deckBuilder = new DeckBuilder();
            var treeGenerator = new TreeGenerator();
            _gameManager = new GameManager(treeGenerator, deckBuilder);
        }

        private void Start()
        {
            // Create Config
            var config = new GameManagerConfig
            {
                TreeConfig = treeGenerationConfig,
                DeckConfig = startingDeckConfig.CreateDeckConfig(),
                InitialHandSize = initialHandSize
            };
            
            // Start Game
            _gameManager.StartNewGame(config);
        }

        public void ExecuteCommand(IGameCommand command)
        {
            CommandProcessor.Instance.EnqueueCommand(command);
            CommandProcessor.Instance.ProcessCommands(_gameManager.state);
        }
    }
} 