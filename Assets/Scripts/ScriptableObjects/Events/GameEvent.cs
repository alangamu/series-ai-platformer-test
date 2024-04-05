using System;
using UnityEngine;

namespace AlbertoGarrido.Platformer.ScriptableObjects.Events
{
    /// <summary>
    /// Basic game event without any parameters.
    /// </summary>
    [CreateAssetMenu(menuName = "Events/GameEvent")]
    public class GameEvent : ScriptableObject
    {
        // Event to be invoked when the game event is raised
        public event Action OnRaise;

        /// <summary>
        /// Raises the game event.
        /// </summary>
        public void Raise()
        {
            // Invoke the event, notifying any registered listeners
            OnRaise?.Invoke();
        }
    }
}