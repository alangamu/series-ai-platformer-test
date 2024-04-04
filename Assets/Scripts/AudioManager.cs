using AlbertoGarrido.Platformer.ScriptableObjects.Events;
using UnityEngine;

namespace AlbertoGarrido.Platformer
{
    public class AudioManager : MonoBehaviour
    {
        // Reference to the AudioSource component for playing sound effects
        [SerializeField]
        private AudioSource _soundFxAudioSource;

        // Event for playing sound effects with specific AudioClip
        [SerializeField]
        private AudioClipGameEvent _playSoundFxGameEvent;

        private void OnEnable()
        {
            // Subscribe to the event for playing sound effects when this component is enabled
            _playSoundFxGameEvent.OnRaise += PlaySoundFx;
        }

        /// <summary>
        /// Method called when the event for playing sound effects is raised
        /// </summary>
        /// <param name="audioClip">clip to play</param>
        private void PlaySoundFx(AudioClip audioClip)
        {
            // Assign the given AudioClip to the AudioSource and play it
            _soundFxAudioSource.clip = audioClip;
            _soundFxAudioSource.Play();
        }
    }
}