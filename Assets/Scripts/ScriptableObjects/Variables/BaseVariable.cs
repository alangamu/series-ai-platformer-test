using System;
using UnityEngine;

namespace AlbertoGarrido.Platformer.ScriptableObjects.Variables
{
    public class BaseVariable<T> : ScriptableObject
    {
        public event Action<T> OnValueChanged;
        public T Value => _value;

        [SerializeField]
        private T _value;

        public void SetValue(T value)
        {
            _value = value;
            OnValueChanged?.Invoke(value);
        }
    }
}