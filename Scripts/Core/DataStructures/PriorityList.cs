using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CardGame.Core.DataStructures
{
    class PriorityList<T> : IEnumerable<T>
    {
        // Internal
        private SortedDictionary<int, List<T>> prioritiesList = new();
    
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<T> GetEnumerator()
        {
            foreach (var priorityGroup in prioritiesList)
                foreach (var item in priorityGroup.Value)
                    yield return item;
        }
        
        public void Add(T value, int priority = 0)
        {
            if (!prioritiesList.ContainsKey(priority))
                prioritiesList[priority] = new List<T>();

            prioritiesList[priority].Add(value);
        }
    
        public void Remove(T value)
        {
            foreach (List<T> priorityGroup in prioritiesList.Values)
                priorityGroup.Remove(value);
        }
    }
}