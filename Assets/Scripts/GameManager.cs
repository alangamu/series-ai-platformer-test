using AlbertoGarrido.Platformer.ScriptableObjects.Events;
using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;

namespace AlbertoGarrido.Platformer
{
    /// <summary>
    /// Manages the game state, including player initialization, player death, and game over.
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        [SerializeField]
        private Transform _startingPoint; // Starting point for the player
        [SerializeField]
        private GameEvent _initializeEvent; // Event raised to initialize the game
        [SerializeField]
        private Transform _playerTransform; // Transform of the player
        [SerializeField]
        private GameEvent _playerDeathEvent; // Event raised when the player dies
        [SerializeField]
        private GameEvent _resetScoreEvent; // Event raised to reset the score
        [SerializeField]
        private GameEvent _gameOverEvent; // Event raised when the game is over
        [SerializeField]
        private Transform _gameOverPanelTransform; // Transform of the game over panel
        [SerializeField]
        private PlayerInput _playerInput; // Reference to the PlayerInput component

        private void OnEnable()
        {
            _gameOverPanelTransform.gameObject.SetActive(false);
            _initializeEvent.OnRaise += Initialize;
            _playerDeathEvent.OnRaise += PlayerDeath;
            _gameOverEvent.OnRaise += GameOver;
        }

        private void OnDisable()
        {
            _initializeEvent.OnRaise -= Initialize;
            _playerDeathEvent.OnRaise -= PlayerDeath;
            _gameOverEvent.OnRaise -= GameOver;
        }

        /// <summary>
        /// Method called when the game is over
        /// </summary>
        private void GameOver()
        {
            // Activate the game over panel and disable player input
            _gameOverPanelTransform.gameObject.SetActive(true);
            _playerInput.enabled = false;
        }

        /// <summary>
        /// Method called when the player dies
        /// </summary>
        private async void PlayerDeath()
        {
            // Disable player input temporarily
            _playerInput.enabled = false;

            // Wait for a short delay
            await Task.Delay(TimeSpan.FromSeconds(1));

            // Re-enable player input and raise the initialize event to restart the game
            _playerInput.enabled = true;
            _initializeEvent.Raise();
        }

        /// <summary>
        /// Method called to initialize the game
        /// </summary>
        private void Initialize()
        {
            // Reset player position to the starting point
            _playerTransform.position = _startingPoint.position;
        }
    }
}