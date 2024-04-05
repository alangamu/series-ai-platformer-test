using AlbertoGarrido.Platformer.ScriptableObjects.Events;
using System;
using System.Threading.Tasks;
using UnityEngine;

namespace AlbertoGarrido.Platformer
{
    /// <summary>
    /// Manages the levels in the game, handling level completion, game over, and level loading.
    /// </summary>
    public class LevelManager : MonoBehaviour
    {
        [SerializeField]
        private GameObject[] _levels; // Array of the levels 
        [SerializeField]
        private GameEvent _levelCompletedEvent; // Event raised when a level is completed
        [SerializeField]
        private GameEvent _gameOverEvent; // Event raised when the game is over
        [SerializeField]
        private Transform _loadingPanelTransform; // Reference to the loading panel's transform
        [SerializeField]
        private float _loadingTime; // Loading time in seconds
        [SerializeField]
        private GameEvent _initializeEvent; // Event raised to initialize a level

        private int _levelIndex = 0; // Index of the current level

        private void OnEnable()
        {
            _loadingPanelTransform.gameObject.SetActive(false);
            _levelCompletedEvent.OnRaise += LevelCompleted; // Subscribe to the level completed event
        }

        private void OnDisable()
        {
            _levelCompletedEvent.OnRaise -= LevelCompleted; // Unsubscribe from the level completed event
        }

        private void Start()
        {
            _levelIndex = 0;

            // Activate the level and deactivate the others
            for (int i = 0; i < _levels.Length; i++)
            {
                _levels[i].gameObject.SetActive(i == _levelIndex);
            }

            // Raise the initialize event to set up the first level
            _initializeEvent.Raise();
        }

        /// <summary>
        /// Method called when a level is completed
        /// </summary>
        private async void LevelCompleted()
        {
            // Check if the current level is the last level
            if (_levelIndex == _levels.Length - 1)
            {
                // If it is the last level, raise the game over event
                _gameOverEvent.Raise();
                return;
            }
            
            _loadingPanelTransform.gameObject.SetActive(true); // Activate the loading panel

            _levels[_levelIndex].SetActive(false);// Deactivate the current level

            _levelIndex++; // Move to the next level

            _levels[_levelIndex].SetActive(true); // Activate the next level

            _initializeEvent.Raise(); // Raise the initialize event to set up the next level

            await Task.Delay(TimeSpan.FromSeconds(_loadingTime)); // Wait for the specified loading time

            _loadingPanelTransform.gameObject.SetActive(false); // Deactivate the loading panel
        }
    }
}