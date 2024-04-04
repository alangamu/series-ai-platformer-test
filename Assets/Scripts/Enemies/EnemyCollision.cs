﻿using AlbertoGarrido.Platformer.Intefaces;
using AlbertoGarrido.Platformer.ScriptableObjects;
using UnityEngine;

namespace AlbertoGarrido.Platformer.Enemies
{
    public class EnemyCollision : MonoBehaviour
    {
        [SerializeField]
        private AudioClipGameEvent _playSoundFxGameEvent;
        public float pushForce = 10f; // Magnitude of force applied to the player
        [SerializeField]
        private AudioClip _damageSound;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            // Check if the collision is with the player
            if (collision.gameObject.CompareTag("Player"))
            {
                _playSoundFxGameEvent.Raise(_damageSound);
                // Calculate the direction from the enemy to the player
                Vector3 pushDirection = (collision.transform.position - transform.position).normalized;
                pushDirection.y = 0f; // Ignore Y component

                // Apply a force to push the player away from the enemy only along the X-axis
                collision.gameObject.transform.position += pushDirection * pushForce;
            }

            if (collision.gameObject.CompareTag("Bullet"))
            {
                Destroy(collision.gameObject);
                if (TryGetComponent(out IDeath enemyDeath))
                {
                    enemyDeath.Death();
                }
            }
        }
    }
}