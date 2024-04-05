using AlbertoGarrido.Platformer.ScriptableObjects.Events;
using UnityEngine;

namespace AlbertoGarrido.Platformer
{
    /// <summary>
    /// Represents a collectible coin.
    /// </summary>
    public class Coin : MonoBehaviour
    {
        [SerializeField]
        private AudioClipGameEvent _onCollectAudioEvent; // Event to play collect sound
        [SerializeField]
        private AudioClip _collectSound; // Sound to play when collected
        [SerializeField]
        private IntGameEvent _addPointsToScoreEvent; // Event to add points to score
        [SerializeField]
        private int _pointsAtCollect; // Points gained when collected

        /// <summary>
        /// Called when another collider enters the trigger.
        /// </summary>
        /// <param name="collision">The collider that entered the trigger.</param>
        private void OnTriggerEnter2D(Collider2D collision)
        {
            // Check if the collider belongs to the player
            if (collision.gameObject.CompareTag("Player"))
            {
                // Raise the audio event to play the collect sound
                _onCollectAudioEvent.Raise(_collectSound);
                // Raise the event to add points to the score
                _addPointsToScoreEvent.Raise(_pointsAtCollect);
                // Destroy the coin game object
                Destroy(gameObject);
            }
        }
    }
}