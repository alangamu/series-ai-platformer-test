using AlbertoGarrido.Platformer.Intefaces;
using AlbertoGarrido.Platformer.ScriptableObjects.Events;
using UnityEngine;

namespace AlbertoGarrido.Platformer.Player
{
    public class PlayerStomp : MonoBehaviour
    {
        [SerializeField]
        private GameEvent _resetJumpsEvent;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Enemy Kill"))
            {
                _resetJumpsEvent.Raise();
                if (collision.gameObject.TryGetComponent(out IDeath enemyDeath))
                {
                    enemyDeath.Death();
                }
            }
        }
    }
}