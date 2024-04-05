using AlbertoGarrido.Platformer.ScriptableObjects.Events;
using System;
using System.Threading.Tasks;
using UnityEngine;

namespace AlbertoGarrido.Platformer
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField]
        private GameObject[] _levels;
        [SerializeField]
        private GameEvent _levelCompletedEvent;
        [SerializeField]
        private GameEvent _gameOverEvent;
        [SerializeField]
        private Transform _loadingPanelTransform;
        [SerializeField]
        private float _loadingTime;
        [SerializeField]
        private GameEvent _initializeEvent;

        private int _levelIndex = 0;

        private void OnEnable()
        {
            _loadingPanelTransform.gameObject.SetActive(false);
            _levelCompletedEvent.OnRaise += LevelCompleted;
        }

        private void OnDisable()
        {
            _levelCompletedEvent.OnRaise -= LevelCompleted;
        }

        private void Start()
        {
            _levelIndex = 0;

            for (int i = 0; i < _levels.Length; i++)
            {
                _levels[i].gameObject.SetActive(i == _levelIndex);
            }
            _initializeEvent.Raise();
        }

        private async void LevelCompleted()
        {
            if (_levelIndex == _levels.Length - 1)
            {
                _gameOverEvent.Raise();
                return;
            }

            _loadingPanelTransform.gameObject.SetActive(true);
            _levels[_levelIndex].SetActive(false);
            _levelIndex++;
            _levels[_levelIndex].SetActive(true);
            _initializeEvent.Raise();

            await Task.Delay(TimeSpan.FromSeconds(_loadingTime));
            
            _loadingPanelTransform.gameObject.SetActive(false);
        }
    }
}