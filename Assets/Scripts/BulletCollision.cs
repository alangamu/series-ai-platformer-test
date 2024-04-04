using UnityEngine;

namespace AlbertoGarrido.Platformer
{
    public class BulletCollision : MonoBehaviour
    {
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (!collision.gameObject.CompareTag("Player"))
            {
                Destroy(gameObject);
            }
        }
    }
}