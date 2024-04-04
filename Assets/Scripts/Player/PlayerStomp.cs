﻿using AlbertoGarrido.Platformer.Intefaces;
using AlbertoGarrido.Platformer.ScriptableObjects;
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
                //TODO: maje a small jump to emulate bouncing
                if (collision.gameObject.TryGetComponent(out IDeath enemyDeath))
                {
                    enemyDeath.Death();
                }
            }
        }
    }
}