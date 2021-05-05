using UnityEngine;

namespace Game.Pooling
{
    public class ClearAtPoint : MonoBehaviour
    {
        [SerializeField] private float _clearAtZ;

        private void FixedUpdate()
        {
            if (transform.position.z <= _clearAtZ)
            {
                gameObject.SetActive(false);
            }
        }
    }
}