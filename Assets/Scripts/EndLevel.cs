using AlbertoGarrido.Platformer.ScriptableObjects.Events;
using UnityEngine;

namespace AlbertoGarrido.Platformer
{
    public class EndLevel : MonoBehaviour
    {
        [SerializeField]
        private GameEvent _levelCompletedEvent;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                _levelCompletedEvent.Raise();
            }
        }
    }
}