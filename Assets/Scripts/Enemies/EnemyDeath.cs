using AlbertoGarrido.Platformer.Intefaces;
using AlbertoGarrido.Platformer.ScriptableObjects.Events;
using UnityEngine;

namespace AlbertoGarrido.Platformer.Enemies
{
    public class EnemyDeath : MonoBehaviour, IDeath
    {
        [SerializeField]
        private AudioClipGameEvent _playSoundFxGameEvent;
        [SerializeField]
        private AudioClip _deathSound;
        [SerializeField]
        private GameObject _deathFxPrefab;
        [SerializeField]
        private SpriteRenderer _spriteRenderer;

        public void Death()
        {
            _playSoundFxGameEvent.Raise(_deathSound);
            GameObject deathFxPrefab = Instantiate(_deathFxPrefab, transform);
            _spriteRenderer.enabled = false;
            Destroy(deathFxPrefab, 1f);
            Destroy(gameObject, 1f);
        }
    }
}