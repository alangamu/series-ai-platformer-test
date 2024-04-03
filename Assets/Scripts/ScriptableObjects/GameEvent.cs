using System;
using UnityEngine;

namespace AlbertoGarrido.Platformer.ScriptableObjects
{
    [CreateAssetMenu]
    public class GameEvent : ScriptableObject
    {
        public event Action OnRaise;

        public void Raise()
        {
            OnRaise?.Invoke();
        }
    }
}