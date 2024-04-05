using UnityEngine;

namespace AlbertoGarrido.Platformer
{
    /// <summary>
    /// Controls enemy animations based on its Rigidbody2D velocity magnitude.
    /// </summary>
    public class EnemyAnimationController : MonoBehaviour
    {
        [SerializeField]
        private Animator _animator; // Reference to the Animator component for controlling animations
        [SerializeField]
        private Rigidbody2D _rb; // Reference to the Rigidbody2D component for accessing velocity

        private void Update()
        {
            // Set the "speed" parameter in the Animator based on the Rigidbody2D velocity magnitude
            _animator.SetFloat("speed", _rb.velocity.magnitude);
        }
    }
}