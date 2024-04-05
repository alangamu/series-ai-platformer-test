using AlbertoGarrido.Platformer.Intefaces;
using AlbertoGarrido.Platformer.ScriptableObjects.Events;
using UnityEngine;

namespace AlbertoGarrido.Platformer.Enemies
{
    /// <summary>
    /// Handles enemy death behavior, including playing death effects, sounds, adding points to the score, and disabling the enemy.
    /// Implements the IDeath interface.
    /// </summary>
    public class EnemyDeath : MonoBehaviour, IDeath
    {
        [SerializeField]
        private AudioClipGameEvent _playSoundFxGameEvent; // Event for playing sound effects with specific AudioClip
        [SerializeField]
        private AudioClip _deathSound; // Sound to play when the enemy dies
        [SerializeField]
        private GameObject _deathFxPrefab; // Prefab for the death visual effects
        [SerializeField]
        private SpriteRenderer _spriteRenderer; // Reference to the SpriteRenderer component
        [SerializeField]
        private IntGameEvent _addPointsToScoreEvent; // Event for adding points to the score
        [SerializeField]
        private int _pointsAtDeath; // Points to add to the score when the enemy dies
        [SerializeField]
        private BoxCollider2D _enemyCollider; // Reference to the BoxCollider2D component of the enemy

        /// <summary>
        /// Method called when the enemy dies.
        /// </summary>
        public void Death()
        {
            // Disable the collider to prevent further interactions
            _enemyCollider.enabled = false;

            // Raise the event to play the death sound
            _playSoundFxGameEvent.Raise(_deathSound);

            // Instantiate the death visual effects
            GameObject deathFxPrefab = Instantiate(_deathFxPrefab, transform);

            // Disable the sprite renderer to hide the enemy sprite
            _spriteRenderer.enabled = false;

            // Raise the event to add points to the score
            _addPointsToScoreEvent.Raise(_pointsAtDeath);

            // Destroy the death visual effects after a delay
            Destroy(deathFxPrefab, 1f);

            // Destroy the enemy object after a delay
            Destroy(gameObject, 1f);
        }
    }
}