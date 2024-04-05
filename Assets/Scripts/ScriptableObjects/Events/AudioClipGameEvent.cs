using UnityEngine;

namespace AlbertoGarrido.Platformer.ScriptableObjects.Events
{
    /// <summary>
    /// Game event specifically for passing around AudioClips.
    /// </summary>
    [CreateAssetMenu(menuName = "Events/AudioClip GameEvent")]
    public class AudioClipGameEvent : BaseGameEvent<AudioClip>
    {

    }
}