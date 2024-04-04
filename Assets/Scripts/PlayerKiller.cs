using AlbertoGarrido.Platformer.ScriptableObjects.Events;
using UnityEngine;

namespace AlbertoGarrido.Platformer
{
    public class PlayerKiller : MonoBehaviour
    {
        [SerializeField]
        private GameEvent _playerDeathEvent;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                _playerDeathEvent.Raise();
            }
        }
    }
}