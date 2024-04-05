using AlbertoGarrido.Platformer.Intefaces;
using AlbertoGarrido.Platformer.ScriptableObjects.Events;
using UnityEngine;

namespace AlbertoGarrido.Platformer.Player
{
    /// <summary>
    /// This class handles player stomping behavior, triggering events when the player stomps on enemies.
    /// </summary>
    public class PlayerStomp : MonoBehaviour
    {
        [SerializeField]
        private GameEvent _resetJumpsEvent; // Event to reset player jumps when they stomp on an enemy

        /// <summary>
        /// Called when a collision occurs.
        /// </summary>
        /// <param name="collision">Information about the collision.</param>
        private void OnCollisionEnter2D(Collision2D collision)
        {
            // Check if the collision is with an object tagged as "Enemy Kill"
            if (collision.gameObject.CompareTag("Enemy Kill"))
            {
                // Raise the event to reset player jumps
                _resetJumpsEvent.Raise();
                // Check if the collided object implements the IDeath interface
                if (collision.gameObject.TryGetComponent(out IDeath enemyDeath))
                {
                    // Call the Death method on the collided object
                    enemyDeath.Death();
                }
            }
        }
    }
}