using AlbertoGarrido.Platformer.Intefaces;
using AlbertoGarrido.Platformer.ScriptableObjects.Events;
using UnityEngine;

namespace AlbertoGarrido.Platformer
{
    public class Coin : MonoBehaviour, ICollectable
    {
        [SerializeField]
        private AudioClipGameEvent _onCollectAudioEvent;
        [SerializeField]
        private AudioClip _collectSound;

        public void Collect()
        {
            _onCollectAudioEvent.Raise(_collectSound);
            Destroy(gameObject);
        }
    }
}