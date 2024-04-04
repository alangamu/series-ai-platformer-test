using AlbertoGarrido.Platformer.ScriptableObjects.Variables;
using AlbertoGarrido.Platformer.ScriptableObjects.Events;
using UnityEngine;

namespace AlbertoGarrido.Platformer.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerAnimationController : MonoBehaviour
    {
        [SerializeField]
        private Animator _animator;

        [SerializeField]
        private GameEvent _performJumpEvent;
        [SerializeField]
        private BoolVariable _isWallSlidingVariable;

        private Rigidbody2D _rb;

        private void Update()
        {
            _animator.SetFloat("yVelocity", _rb.velocity.y);
            _animator.SetFloat("magnitude", _rb.velocity.magnitude);
        }

        private void OnEnable()
        {
            _rb = GetComponent<Rigidbody2D>();
            _performJumpEvent.OnRaise += Jump;
            _isWallSlidingVariable.OnValueChanged += WallRiding;
        }

        private void OnDisable()
        {
            _performJumpEvent.OnRaise -= Jump;
            _isWallSlidingVariable.OnValueChanged -= WallRiding;
        }

        private void WallRiding(bool isWallSliding)
        {
            _animator.SetBool("isWallSliding", isWallSliding);
        }

        private void Jump()
        {
            _animator.SetTrigger("jump");
        }
    }
}