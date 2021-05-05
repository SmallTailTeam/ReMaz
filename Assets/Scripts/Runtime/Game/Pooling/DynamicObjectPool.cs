using System.Collections.Generic;
using UnityEngine;

namespace Game.Pooling
{
    public class DynamicObjectPool : ObjectPool<GameObject>
    {
        private Dictionary<GameObject, List<GameObject>> _pool = new Dictionary<GameObject, List<GameObject>>();
        
        protected override GameObject Existent(GameObject prefab, Transform parent)
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
    }
}