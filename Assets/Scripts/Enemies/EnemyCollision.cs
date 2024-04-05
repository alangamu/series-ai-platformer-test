using AlbertoGarrido.Platformer.Intefaces;
using AlbertoGarrido.Platformer.ScriptableObjects.Events;
using UnityEngine;

namespace AlbertoGarrido.Platformer.Enemies
{
    /// <summary>
    /// Handles collisions with enemies, causing damage to the player and reacting to bullet collisions.
    /// </summary>
    public class EnemyCollision : MonoBehaviour
    {
        [SerializeField]
        private IntGameEvent _damageToPlayerEvent; // Event for causing damage to the player

        [SerializeField]
        private int _damageToPlayer; // Amount of damage inflicted to the player
        [SerializeField]
        private AudioClipGameEvent _playSoundFxGameEvent; // Event for playing sound effects with specific AudioClip
        [SerializeField]
        private float _pushForce = 10f; // Magnitude of force applied to the player
        [SerializeField]
        private AudioClip _damageSound; // Sound to play when the enemy damages the player

        /// <summary>
        /// Method called when a collision occurs
        /// </summary>
        /// <param name="collision">Information about the collision.</param>
        private void OnCollisionEnter2D(Collision2D collision)
        {
            // Check if the collision is with the player
            if (collision.gameObject.CompareTag("Player"))
            {
                // Raise the event to cause damage to the player
                _damageToPlayerEvent.Raise(_damageToPlayer);

                // Raise the event to play the damage sound effect
                _playSoundFxGameEvent.Raise(_damageSound);
                // Calculate the direction from the enemy to the player
                Vector3 pushDirection = (collision.transform.position - transform.position).normalized;
                pushDirection.y = 0f; // Ignore Y component

                // Apply a force to push the player away from the enemy only along the X-axis
                collision.gameObject.transform.position += pushDirection * _pushForce;
            }

            // Check if the collision is with a bullet
            if (collision.gameObject.CompareTag("Bullet"))
            {
                // Destroy the bullet
                Destroy(collision.gameObject);

                // Check if the enemy implements the IDeath interface
                if (TryGetComponent(out IDeath enemyDeath))
                {
                    // Call the Death method on the enemy
                    enemyDeath.Death();
                }
            }
        }
    }
}