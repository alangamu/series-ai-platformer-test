using System;
using UnityEngine;

namespace AlbertoGarrido.Platformer.ScriptableObjects.Variables
{
    /// <summary>
    /// Base class for creating ScriptableObject variables with change events.
    /// </summary>
    /// <typeparam name="T">Type of the variable.</typeparam>
    public class BaseVariable<T> : ScriptableObject
    {
        // Event to be invoked when the value of the variable changes
        public event Action<T> OnValueChanged;

        // Current value of the variable
        public T Value => _value;

        // Serialized field to hold the value of the variable
        [SerializeField]
        private T _value;

        /// <summary>
        /// Sets the value of the variable and invokes the change event.
        /// </summary>
        /// <param name="value">The new value of the variable.</param>
        public void SetValue(T value)
        {
            // Update the value
            _value = value;
            // Invoke the change event, passing the new value
            OnValueChanged?.Invoke(value);
        }
    }
}