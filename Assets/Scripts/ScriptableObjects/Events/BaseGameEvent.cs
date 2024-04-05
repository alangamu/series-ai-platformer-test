using System;
using UnityEngine;

namespace AlbertoGarrido.Platformer.ScriptableObjects.Events
{
    /// <summary>
    /// Base class for a generic game event that can pass a parameter of type T.
    /// </summary>
    /// <typeparam name="T">Type of the parameter passed by the event.</typeparam>
    public class BaseGameEvent<T> : ScriptableObject
    {
        // Event to be invoked when the game event is raised, passing a parameter of type T
        public event Action<T> OnRaise;

        /// <summary>
        /// Raises the game event with the specified parameter.
        /// </summary>
        /// <param name="type">The parameter to pass to listeners.</param>
        public virtual void Raise(T type)
        {
            // Invoke the event, passing the specified parameter to any registered listeners
            OnRaise?.Invoke(type);
        }
    }
}