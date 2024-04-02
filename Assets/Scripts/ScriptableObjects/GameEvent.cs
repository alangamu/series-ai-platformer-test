using System;
using UnityEngine;

namespace Assets.Scripts.ScriptableObjects
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