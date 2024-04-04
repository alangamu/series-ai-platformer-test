using System;
using UnityEngine;

namespace AlbertoGarrido.Platformer.ScriptableObjects
{
    [CreateAssetMenu(menuName = "Events/AudioClip GameEvent")]
    public class AudioClipGameEvent : ScriptableObject
    {
        public event Action<AudioClip> OnRaise;

        public virtual void Raise(AudioClip audioClip)
        {
            OnRaise?.Invoke(audioClip);
        }
    }
}