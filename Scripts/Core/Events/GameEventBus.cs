using System;
using System.Collections.Generic;
using CardGame.Core.DataStructures;

namespace CardGame.Core.Events
{
    public class GameEventBus
    {
        private readonly Dictionary<Type, PriorityList<Delegate>> _handlers = new();
        private static GameEventBus _instance;
        
        public static GameEventBus Instance => _instance ??= new GameEventBus();

        public void Subscribe<T>(Action<T> handler, int priority = 0)
        {
            var type = typeof(T);

            if (!_handlers.ContainsKey(type))
                _handlers[type] = new PriorityList<Delegate>();
            
            _handlers[type].Add(handler, priority);
        }

        public void Unsubscribe<T>(Action<T> handler)
        {
            var type = typeof(T);
            if (!_handlers.ContainsKey(type))
                return;

            _handlers[type].Remove(handler);
        }

        public void Publish<T>(T gameEvent)
        {
            var type = typeof(T);
            if (!_handlers.ContainsKey(type))
                return;

            foreach (var handler in _handlers[type])
            {
                ((Action<T>)handler)(gameEvent);
            }
        }
    }
} 