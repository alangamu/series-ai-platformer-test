using AlbertoGarrido.Platformer.ScriptableObjects.Events;
using UnityEngine;

namespace AlbertoGarrido.Platformer.Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField]
        private GameEvent _playerDeathEvent;

        private void OnEnable()
        {
            _playerDeathEvent.OnRaise += PlayerDeath;
        }

        private void OnDisable()
        {
            _playerDeathEvent.OnRaise -= PlayerDeath;
        }

        private void PlayerDeath()
        {
            
        }
    }
}