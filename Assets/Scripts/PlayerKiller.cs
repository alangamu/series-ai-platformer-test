using AlbertoGarrido.Platformer.ScriptableObjects.Events;
using UnityEngine;

namespace AlbertoGarrido.Platformer
{
    /// <summary>
    /// Causes the player's death when collided with.
    /// </summary>
    public class PlayerKiller : MonoBehaviour
    {
        [SerializeField]
        private GameEvent _playerDeathEvent; // Event to raise when the player dies

        /// <summary>
        /// Called when a collision occurs.
        /// </summary>
        /// <param name="collision">The collision data.</param>
        private void OnCollisionEnter2D(Collision2D collision)
        {
            // Check if the collided object is the player
            if (collision.gameObject.CompareTag("Player"))
            {
                // Raise the player death event
                _playerDeathEvent.Raise();
            }
        }
    }
}