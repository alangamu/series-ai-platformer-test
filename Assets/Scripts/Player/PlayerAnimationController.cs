using AlbertoGarrido.Platformer.ScriptableObjects.Variables;
using AlbertoGarrido.Platformer.ScriptableObjects.Events;
using UnityEngine;

namespace AlbertoGarrido.Platformer.Player
{
    /// <summary>
    /// Controls player animations based on Rigidbody2D velocity and jump events.
    /// </summary>
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerAnimationController : MonoBehaviour
    {
        [SerializeField]
        private Animator _animator; // Reference to the Animator component for controlling animations

        [SerializeField]
        private GameEvent _performJumpEvent; // Event raised when the player performs a jump
        [SerializeField]
        private BoolVariable _isWallSlidingVariable; // Variable indicating whether the player is wall sliding

        private Rigidbody2D _rb; // Reference to the Rigidbody2D component

        private void Update()
        {
            // Set the "yVelocity" parameter in the Animator based on the Rigidbody2D velocity.y
            _animator.SetFloat("yVelocity", _rb.velocity.y);
            // Set the "magnitude" parameter in the Animator based on the Rigidbody2D velocity magnitude
            _animator.SetFloat("magnitude", _rb.velocity.magnitude);
        }

        private void OnEnable()
        {
            // Get the Rigidbody2D component attached to the player
            _rb = GetComponent<Rigidbody2D>();
            _performJumpEvent.OnRaise += Jump;
            _isWallSlidingVariable.OnValueChanged += WallRiding;
        }

        private void OnDisable()
        {
            _performJumpEvent.OnRaise -= Jump;
            _isWallSlidingVariable.OnValueChanged -= WallRiding;
        }

        /// <summary>
        /// Method called when the isWallSliding variable changes
        /// </summary>
        /// <param name="isWallSliding">indicates if the player is wall sliding</param>
        private void WallRiding(bool isWallSliding)
        {
            // Set the "isWallSliding" parameter in the Animator based on the isWallSliding variable
            _animator.SetBool("isWallSliding", isWallSliding);
        }

        /// <summary>
        /// Method called when the player performs a jump
        /// </summary>
        private void Jump()
        {
            // Trigger the "jump" trigger in the Animator to play the jump animation
            _animator.SetTrigger("jump");
        }
    }
}