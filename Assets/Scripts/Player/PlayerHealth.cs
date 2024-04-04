using AlbertoGarrido.Platformer.ScriptableObjects.Variables;
using AlbertoGarrido.Platformer.ScriptableObjects.Events;
using UnityEngine;

namespace AlbertoGarrido.Platformer.Player
{
    public class PlayerHealth : MonoBehaviour
    {
        [SerializeField]
        private IntVariable _playerHealthVariable;
        [SerializeField]
        private IntVariable _playerMaxHealthVariable;
        [SerializeField]
        private IntGameEvent _damageToPlayerEvent;
        [SerializeField]
        private GameEvent _initializeEvent;
        [SerializeField]
        private GameEvent _playerDeathEvent;

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

        private void Initialize() 
        {
            _playerHealthVariable.SetValue(_playerMaxHealthVariable.Value);
        }

        private void DamagePlayer(int damagePoints)
        {
            int currentHealth = _playerHealthVariable.Value - damagePoints;
            if (currentHealth <= 0)
            {
                _playerHealthVariable.SetValue(0);
                _playerDeathEvent.Raise();
            }
            else
            {
                _playerHealthVariable.SetValue(currentHealth);
            }
        }
    }
}