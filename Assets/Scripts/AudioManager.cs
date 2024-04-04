using AlbertoGarrido.Platformer.ScriptableObjects;
using UnityEngine;

namespace AlbertoGarrido.Platformer
{
    public class AudioManager : MonoBehaviour
    {
        [SerializeField]
        private AudioSource _soundFxAudioSource;
        [SerializeField]
        private AudioClipGameEvent _playSoundFxGameEvent;

        private void OnEnable()
        {
            _playSoundFxGameEvent.OnRaise += PlaySoundFx;
        }

        private void PlaySoundFx(AudioClip audioClip)
        {
            _soundFxAudioSource.clip = audioClip;
            _soundFxAudioSource.Play();
        }
    }
}