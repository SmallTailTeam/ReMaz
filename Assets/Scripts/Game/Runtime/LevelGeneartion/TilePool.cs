using System.Collections.Generic;
using UnityEngine;

namespace ReMaz.Game.LevelGeneration
{
    public class TilePool : MonoBehaviour
    {
        private Dictionary<GameObject, List<GameObject>> _pool = new Dictionary<GameObject, List<GameObject>>();
        
        public GameObject GetInstance(GameObject prefab)
        {
            List<GameObject> subPool;

            if (_pool.ContainsKey(prefab))
            {
                subPool = _pool[prefab];
            }
            else
            {
                subPool = new List<GameObject>();
                _pool[prefab] = subPool;
            }
            
            foreach (GameObject instance in subPool)
            {
                if (!instance.activeSelf)
                {
                    instance.SetActive(true);
                    return instance;
                }
            }

            GameObject newInstance = Instantiate(prefab, transform);
            subPool.Add(newInstance);

            return newInstance;
        }
    }
}