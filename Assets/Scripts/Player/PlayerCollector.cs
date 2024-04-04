using AlbertoGarrido.Platformer.Intefaces;
using UnityEngine;

namespace AlbertoGarrido.Platformer
{
    public class PlayerCollector : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            ICollectable collectable = collision.GetComponent<ICollectable>();
            if (collectable != null)
            {
                collectable.Collect();
            }
        }
    }
}