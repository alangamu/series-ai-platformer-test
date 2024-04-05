using AlbertoGarrido.Platformer.ScriptableObjects.Events;
using UnityEngine;
using UnityEngine.UI;

namespace AlbertoGarrido.Platformer
{
    /// <summary>
    /// Manages the player's score.
    /// </summary>
    public class ScoreController : MonoBehaviour
    {
        [SerializeField]
        private GameEvent _resetScoreEvent; // Event to reset the score
        [SerializeField]
        private IntGameEvent _addPointsToScoreEvent; // Event to add points to the score
        [SerializeField]
        private Text _scoreText; // Text UI element to display the score

        private int _score = 0; // Current score


        private void OnEnable()
        {
            _addPointsToScoreEvent.OnRaise += AddPointsToScore;
            _resetScoreEvent.OnRaise += ResetScore;
        }

        private void OnDisable()
        {
            _addPointsToScoreEvent.OnRaise -= AddPointsToScore;
            _resetScoreEvent.OnRaise -= ResetScore;
        }

        /// <summary>
        /// Resets the score to zero.
        /// </summary>
        private void ResetScore()
        {
            _score = 0;
            _scoreText.text = _score.ToString(); // Update the score display
        }

        /// <summary>
        /// Adds points to the score.
        /// </summary>
        /// <param name="pointsToAdd">The points to add.</param>
        private void AddPointsToScore(int pointsToAdd)
        {
            _score += pointsToAdd; // Increment the score by the specified points
            _scoreText.text = _score.ToString(); // Update the score display
        }
    }
}