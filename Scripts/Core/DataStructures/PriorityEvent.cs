using System;

namespace CardGame.Core.DataStructures
{
    public class PriorityEvent
    {
        private readonly PriorityList<Action> handlers = new();

        public void Invoke()
        {
            foreach (var handler in handlers)
                handler();
        }

        public static PriorityEvent operator +(PriorityEvent e, Action handler)
        {
            e.handlers.Add(handler);
            return e;
        }

        public static PriorityEvent operator -(PriorityEvent e, Action handler)
        {
            e.handlers.Remove(handler);
            return e;
        }

        public void AddWithPriority(Action handler, int priority)
            => handlers.Add(handler, priority);
    }

    public class PriorityEvent<T>
    {
        private readonly PriorityList<Action<T>> handlers = new();

        public void Invoke(T value)
        {
            foreach (var handler in handlers)
                handler(value);
        }

        public static PriorityEvent<T> operator +(PriorityEvent<T> e, Action<T> handler)
        {
            e.handlers.Add(handler);
            return e;
        }

        public static PriorityEvent<T> operator -(PriorityEvent<T> e, Action<T> handler)
        {
            e.handlers.Remove(handler);
            return e;
        }

        public void AddWithPriority(Action<T> handler, int priority)
            => handlers.Add(handler, priority);
    }
}