using System.Collections;
using System.Collections.Generic;

namespace CardGame.Core.DataStructures
{
    public class Map<T1, T2> : IEnumerable<KeyValuePair<T1, T2>>
    {
        // Internal
        private readonly Dictionary<T1, T2> _forward = new Dictionary<T1, T2>();
        private readonly Dictionary<T2, T1> _backward = new Dictionary<T2, T1>();

        // Accessors
        public IReadOnlyDictionary<T1, T2> Forward => _forward;
        public IReadOnlyDictionary<T2, T1> Backward => _backward;
        
        public void Add(T1 t1, T2 t2)
        {
            _forward.Add(t1, t2);
            _backward.Add(t2, t1);
        }

        #region Remove
        
        public void Remove(T1 t1)
        {
            _backward.Remove(_forward[t1]);
            _forward.Remove(t1);
        }

        public void Remove(T2 t2)
        {
            _forward.Remove(_backward[t2]);
            _backward.Remove(t2);
        }

        public bool TryRemove(T1 t1)
        {
            if (_forward.TryGetValue(t1, out T2 revKey))
            {
                Remove(t1);
                return true;
            }

            return false;
        }
    
        public bool TryRemove(T2 t2)
        {
            if (_backward.TryGetValue(t2, out T1 forwardKey))
            {
                Remove(t2);
                return true;
            }

            return false;
        }
        
        public void Clear()
        {
            _forward.Clear();
            _backward.Clear();
        }

        #endregion

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<KeyValuePair<T1, T2>> GetEnumerator()
        {
            return _forward.GetEnumerator();
        }
    }
}