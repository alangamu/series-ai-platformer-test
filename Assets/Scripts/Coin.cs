using AlbertoGarrido.Platformer.ScriptableObjects.Events;
using UnityEngine;

namespace AlbertoGarrido.Platformer
{
    public class Coin : MonoBehaviour
    {
        [SerializeField]
        private AudioClipGameEvent _onCollectAudioEvent;
        [SerializeField]
        private AudioClip _collectSound;
        [SerializeField]
        private IntGameEvent _addPointsToScoreEvent;
        [SerializeField]
        private int _pointsAtCollect;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                _onCollectAudioEvent.Raise(_collectSound);
                _addPointsToScoreEvent.Raise(_pointsAtCollect);
                Destroy(gameObject);
            }
        }
    }
}