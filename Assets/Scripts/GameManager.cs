using AlbertoGarrido.Platformer.ScriptableObjects.Events;
using System;
using System.Threading.Tasks;
using UnityEngine;

namespace AlbertoGarrido.Platformer
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField]
        private Transform _startingPoint;
        [SerializeField]
        private GameEvent _initializeEvent;
        [SerializeField]
        private Transform _playerTransform;
        [SerializeField]
        private GameEvent _playerDeathEvent;
        [SerializeField]
        private GameEvent _resetScoreEvent;
        [SerializeField]
        private GameEvent _gameOverEvent;
        [SerializeField]
        private Transform _gameOverPanelTransform;

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

        private void GameOver()
        {
            _gameOverPanelTransform.gameObject.SetActive(true);
        }

        private async void PlayerDeath()
        {
            await Task.Delay(TimeSpan.FromSeconds(1));
            _initializeEvent.Raise();
        }

        private void Initialize()
        {
            _playerTransform.position = _startingPoint.position;
            //_resetScoreEvent.Raise();
        }
    }
}