using AlbertoGarrido.Platformer.ScriptableObjects.Events;
using UnityEngine;

namespace AlbertoGarrido.Platformer
{
    /// <summary>
    /// Represents the end level trigger.
    /// </summary>
    public class EndLevel : MonoBehaviour
    {
        [SerializeField]
        private GameEvent _levelCompletedEvent; // Event to signal level completion

        /// <summary>
        /// Called when this collider contacts another collider with a Rigidbody2D attached.
        /// </summary>
        /// <param name="collision">The collision data associated with this event.</param>
        private void OnCollisionEnter2D(Collision2D collision)
        {
            // Check if the colliding game object is the player
            if (collision.gameObject.CompareTag("Player"))
            {
                // Raise the event to signal level completion
                _levelCompletedEvent.Raise();
            }
        }
    }
}