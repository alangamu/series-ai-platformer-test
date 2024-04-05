using AlbertoGarrido.Platformer.ScriptableObjects.Variables;
using AlbertoGarrido.Platformer.ScriptableObjects.Events;
using UnityEngine;

namespace AlbertoGarrido.Platformer.Player
{
    /// <summary>
    /// Manages player health, including taking damage and handling player death.
    /// </summary>
    public class PlayerHealth : MonoBehaviour
    {
        [SerializeField]
        private IntVariable _playerHealthVariable; // Variable to store player health
        [SerializeField]
        private IntVariable _playerMaxHealthVariable; // Variable to store player maximum health
        [SerializeField]
        private IntGameEvent _damageToPlayerEvent; // Event raised when player takes damage
        [SerializeField]
        private GameEvent _initializeEvent; // Event raised to initialize player health
        [SerializeField]
        private GameEvent _playerDeathEvent; // Event raised when player dies

        private void OnEnable()
        {
            _damageToPlayerEvent.OnRaise += DamagePlayer;
            _initializeEvent.OnRaise += Initialize;
        }

        private void OnDisable()
        {
            _damageToPlayerEvent.OnRaise -= DamagePlayer;
            _initializeEvent.OnRaise -= Initialize;
        }

        /// <summary>
        /// Method called to initialize player health
        /// </summary>
        private void Initialize() 
        {
            // Set player health to maximum health
            _playerHealthVariable.SetValue(_playerMaxHealthVariable.Value);
        }

        /// <summary>
        /// Method called when player takes damage
        /// </summary>
        /// <param name="damagePoints">damage to inflict</param>
        private void DamagePlayer(int damagePoints)
        {
            // Calculate current health after taking damage
            int currentHealth = _playerHealthVariable.Value - damagePoints;

            // If current health is less than or equal to 0, player dies
            if (currentHealth <= 0)
            {
                // Set player health to 0
                _playerHealthVariable.SetValue(0);
                // Raise player death event
                _playerDeathEvent.Raise();
            }
            else
            {
                // Set player health to current health
                _playerHealthVariable.SetValue(currentHealth);
            }
        }
    }
}