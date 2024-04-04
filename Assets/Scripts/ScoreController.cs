using AlbertoGarrido.Platformer.ScriptableObjects.Events;
using UnityEngine;
using UnityEngine.UI;

namespace AlbertoGarrido.Platformer
{
    public class ScoreController : MonoBehaviour
    {
        [SerializeField]
        private GameEvent _resetScoreEvent;
        [SerializeField]
        private IntGameEvent _addPointsToScoreEvent;
        [SerializeField]
        private Text _scoreText;

        private int _score = 0;

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

        private void ResetScore()
        {
            _score = 0;
            _scoreText.text = _score.ToString();
        }

        private void AddPointsToScore(int pointsToAdd)
        {
            Debug.Log($"add {pointsToAdd} points");
            _score += pointsToAdd;
            _scoreText.text = _score.ToString();
        }
    }
}