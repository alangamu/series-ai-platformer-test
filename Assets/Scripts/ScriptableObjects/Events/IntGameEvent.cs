using UnityEngine;

namespace AlbertoGarrido.Platformer.ScriptableObjects.Events
{
    /// <summary>
    /// Game event specifically for passing around integer values.
    /// </summary>
    [CreateAssetMenu(menuName = "Events/Int GameEvent")]
    public class IntGameEvent : BaseGameEvent<int>
    {

    }
}