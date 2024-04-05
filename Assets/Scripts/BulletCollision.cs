using UnityEngine;

namespace AlbertoGarrido.Platformer
{
    /// <summary>
    /// Handles collision logic for bullets.
    /// </summary>
    public class BulletCollision : MonoBehaviour
    {
        /// <summary>
        /// Called when a collision occurs.
        /// </summary>
        /// <param name="collision">The collision data.</param>
        private void OnCollisionEnter2D(Collision2D collision)
        {
            // Check if the collided object is not a player
            if (!collision.gameObject.CompareTag("Player"))
            {
                // Destroy the bullet game object
                Destroy(gameObject);
            }
        }
    }
}