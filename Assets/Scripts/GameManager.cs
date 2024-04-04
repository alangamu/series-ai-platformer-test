using AlbertoGarrido.Platformer.ScriptableObjects.Events;
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

        private void Start()
        {
            _initializeEvent.Raise();
        }

        private void OnEnable()
        {
            _initializeEvent.OnRaise += Initialize;
            _playerDeathEvent.OnRaise += PlayerDeath;
        }

        private void OnDisable()
        {
            _initializeEvent.OnRaise -= Initialize;
            _playerDeathEvent.OnRaise -= PlayerDeath;
        }

        private void PlayerDeath()
        {
            _initializeEvent.Raise();
        }

        private void Initialize()
        {
            _playerTransform.position = _startingPoint.position;
        }
    }
}