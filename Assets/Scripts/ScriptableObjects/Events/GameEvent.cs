using System;
using UnityEngine;

namespace AlbertoGarrido.Platformer.ScriptableObjects.Events
{
    [CreateAssetMenu(menuName = "Events/GameEvent")]
    public class GameEvent : ScriptableObject
    {
        public event Action OnRaise;

        public void Raise()
        {
            OnRaise?.Invoke();
        }
    }
}