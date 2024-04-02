using UnityEngine;
using UnityEngine.InputSystem;

namespace AlbertoGarrido.Platformer
{
    public class PlayerController : MonoBehaviour
    {
        // Define properties like speed, jumpForce, etc here
        [Header("Movement")]
        [SerializeField]
        private float _speed = 5f;

        [Header("Jumping")]
        [SerializeField]
        private float _jumpForce = 10f;
        [SerializeField]
        private int _maxJumps = 2;

        [Header("Ground Check")]
        [SerializeField]
        private Transform _groundCheckPos;
        [SerializeField]
        private Vector2 _groundCheckSize = new Vector2(0.5f, 0.05f);
        [SerializeField]
        private LayerMask _groundLayer;

        [Header("Wall Check")]
        [SerializeField]
        private Transform _wallCheckPos;
        [SerializeField]
        private Vector2 _wallCheckSize = new Vector2(0.5f, 0.05f);
        [SerializeField]
        private LayerMask _wallLayer;

        [Header("Gravity")]
        [SerializeField]
        private float _baseGravity = 2f;
        [SerializeField]
        private float _maxFallSpeed = 18f;
        [SerializeField]
        private float _fallSpeedMultiplier = 2f;

        [Header("Wall Movement")]
        [SerializeField]
        private float _wallSlideSpeed = 2f;

        [Header("Wall Jumping")]
        [SerializeField]
        private Vector2 _wallJumpForce = new Vector2(5f, 10f);

        [Header("Animation")]
        [SerializeField]
        private Animator _animator;

        private float _horizontalMovement;
        private Rigidbody2D _rb;
        private int _jumpsRemaining;
        private bool _isFacingRight = true;
        private bool _isWallSliding;
        private bool _isGrounded;

        private bool _isWallJumping;
        private float _wallJumpDirection;
        private float _wallJumpTime = 0.5f;
        private float _wallJumpTimer;

        /// <summary>
        /// This funtion is called by the new Player Input when moving
        /// </summary>
        /// <param name="context">contains the value of the movement</param>
        public void Move(InputAction.CallbackContext context)
        {
            _horizontalMovement = context.ReadValue<Vector2>().x;
        }

        /// <summary>
        /// This funtion is called by the new Player Input when jumping 
        /// </summary>
        /// <param name="context">contains the value if jump was pressed</param>
        public void Jump(InputAction.CallbackContext context)
        {
            if (_jumpsRemaining > 0)
            {
                if (context.performed)
                {
                    //Hold down jump button
                    _rb.velocity = new Vector2(_rb.velocity.x, _jumpForce);
                    _jumpsRemaining--;
                    _animator.SetTrigger("jump");
                }
                else if (context.canceled)
                {
                    //Light tap of jump button
                    _rb.velocity = new Vector2(_rb.velocity.x, _rb.velocity.y * 0.5f);
                    _jumpsRemaining--;
                    _animator.SetTrigger("jump");
                }
            }

            //Wal jump
            if (context.performed && _wallJumpTimer > 0f)
            {
                _isWallJumping = true;
                _rb.velocity = new Vector2(_wallJumpDirection * _wallJumpForce.x, _wallJumpForce.y); //Jump away from wall
                _wallJumpTimer = 0f;
                _animator.SetTrigger("jump");

                //Force flip
                if (transform.localScale.x != _wallJumpDirection)
                {
                    //TODO: dont repeat code
                    _isFacingRight = !_isFacingRight;
                    Vector3 ls = transform.localScale;
                    ls.x *= -1f;
                    transform.localScale = ls;
                }

                Invoke(nameof(CancelWallJump), _wallJumpTime + 0.1f); //Wall jump = 0.5 -> jump again = 0.6
            }
        }

        void Start()
        {
            // Initialize player properties
            _rb = GetComponent<Rigidbody2D>();
        }

        void Update()
        {
            // Read inputs and control the character
            GroundedCheck();
            ProcessGravity();
            ProcessWallSlide();
            ProcessWallJump();

            if (!_isWallJumping)
            {
                _rb.velocity = new Vector2(_speed * _horizontalMovement, _rb.velocity.y);
                Flip();
            }

            _animator.SetFloat("yVelocity", _rb.velocity.y);
            _animator.SetFloat("magnitude", _rb.velocity.magnitude);

            _animator.SetBool("isWallSliding", _isWallSliding);
        }

        //TODO: change name by conventions
        private bool WallCheck()
        {
            return Physics2D.OverlapBox(_wallCheckPos.position, _wallCheckSize, 0, _wallLayer);
        }

        private void GroundedCheck()
        {
            //Check if the tranform is touching any object with the layer "ground"
            if (Physics2D.OverlapBox(_groundCheckPos.position, _groundCheckSize, 0, _groundLayer))
            {
                _jumpsRemaining = _maxJumps;
                _isGrounded = true;
            }
            else 
            { 
                _isGrounded = false; 
            }
        }

        private void ProcessWallSlide()
        {
            //Not grounded and on a wall and movement != 0
            if (!_isGrounded && WallCheck() && _horizontalMovement != 0)
            //if (!_isGrounded && WallCheck())
            {
                //Debug.Log("is wall sliding");
                _isWallSliding = true;
                _rb.velocity = new Vector2(_rb.velocity.x, Mathf.Max(_rb.velocity.y, -_wallSlideSpeed)); //Caps fall rate
            }
            else
            {
                _isWallSliding = false;
            }
        }
        
        private void ProcessWallJump()
        {
            if (_isWallSliding)
            {
                _isWallJumping = false;
                _wallJumpDirection = -transform.localScale.x;
                _wallJumpTimer = _wallJumpTime;

                CancelInvoke(nameof(CancelWallJump));
            }
            else if (_wallJumpTimer > 0f)
            {
                _wallJumpTimer -= Time.deltaTime;
            }
        }

        private void CancelWallJump()
        {
            _isWallJumping = false;
        }

        private void ProcessGravity()
        {
            if (_rb.velocity.y < 0)
            {
                _rb.gravityScale = _baseGravity * _fallSpeedMultiplier; //Fall faster
                _rb.velocity = new Vector2(_rb.velocity.x, Mathf.Max(_rb.velocity.y, -_maxFallSpeed));
            }
            else
            {
                _rb.gravityScale = _baseGravity;
            }
        }

        private void Flip()
        {
            if (_isFacingRight && _horizontalMovement < 0 || !_isFacingRight && _horizontalMovement > 0)
            {
                _isFacingRight = !_isFacingRight;
                Vector3 ls = transform.localScale;
                ls.x *= -1f;
                transform.localScale = ls;
            }
        }

        //TODO: remove when player have visuals
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.white;
            Gizmos.DrawWireCube(_groundCheckPos.position, _groundCheckSize);
            Gizmos.color = Color.blue;
            Gizmos.DrawWireCube(_wallCheckPos.position, _wallCheckSize);
        }
    }
}
