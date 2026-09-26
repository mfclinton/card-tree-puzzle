using System.Collections.Generic;
using CardGame.Core.Commands.Base;
using CardGame.Core.State;

namespace CardGame.Core.Commands
{
    public class CommandProcessor
    {
        // State
        private readonly Queue<IGameCommand> _commandQueue = new Queue<IGameCommand>();
        
        // Singleton
        private static CommandProcessor _instance;        
        public static CommandProcessor Instance => _instance ??= new CommandProcessor();

        public void EnqueueCommand(IGameCommand command)
        {
            _commandQueue.Enqueue(command);
        }

        public void ProcessCommands(GameState state)
        {
            while (_commandQueue.Count > 0)
            {
                var command = _commandQueue.Dequeue();
                command.Execute(state);
            }
        }
    }
} 