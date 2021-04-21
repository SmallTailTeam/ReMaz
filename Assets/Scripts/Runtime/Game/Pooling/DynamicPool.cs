using System.Collections.Generic;
using UnityEngine;

namespace ReMaz.Game.Pooling
{
    public class DynamicPool
    {
        private Dictionary<GameObject, List<GameObject>> _pool = new Dictionary<GameObject, List<GameObject>>();

        private GameObject New(GameObject prefab, Transform parent)
        {
            GameObject instance = Object.Instantiate(prefab, parent);
            _pool[prefab].Add(instance);
            return instance;
        }

        private GameObject Existent(GameObject prefab, Transform parent)
        {
            foreach (GameObject instance in _pool[prefab])
            {
                if (!instance.activeSelf)
                {
                    instance.SetActive(true);
                    instance.transform.parent = parent;
                    return instance;
                }
            }

            return null;
        }
        
        public GameObject Request(GameObject prefab, Transform parent)
        {
            if (!_pool.ContainsKey(prefab))
            {
                _pool[prefab] = new List<GameObject>();
                return New(prefab, parent);
            }

            return Existent(prefab, parent) ?? New(prefab, parent);
        }
    }
}