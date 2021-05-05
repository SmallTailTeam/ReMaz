using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.Pooling
{
    public class ObjectPool<T> where T : Object
    {
        private Dictionary<T, List<T>> _pool = new Dictionary<T, List<T>>();

        protected virtual T New(T prefab, Transform parent)
        {
            T instance = Object.Instantiate(prefab, parent);
            _pool[prefab].Add(instance);
            return instance;
        }

        protected virtual T Existent(T prefab, Transform parent)
        {
            return _pool[prefab].FirstOrDefault();
        }
        
        public virtual T Request(T prefab, Transform parent)
        {
            if (!_pool.ContainsKey(prefab))
            {
                _pool[prefab] = new List<T>();
                return New(prefab, parent);
            }

            return Existent(prefab, parent) ?? New(prefab, parent);
        }
    }
}