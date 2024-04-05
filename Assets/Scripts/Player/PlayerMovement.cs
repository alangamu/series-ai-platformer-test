using AlbertoGarrido.Platformer.ScriptableObjects.Variables;
using AlbertoGarrido.Platformer.ScriptableObjects.Events;
using UnityEngine;
using UnityEngine.InputSystem;

namespace AlbertoGarrido.Platformer.Player
{
    /// <summary>
    /// Controls player movement, including walking, jumping, wall sliding, and wall jumping.
    /// </summary>
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField]
        private GameEvent _performJumpEvent;  // Event raised when the player performs a jump
        [SerializeField]
        private GameEvent _resetJumpsEvent; // Event raised to reset the number of jumps

        [Header("Movement")]
        [SerializeField]
        private float _speed = 5f; // Player movement speed

        [Header("Jumping")]
        [SerializeField]
        private float _jumpForce = 10f; // Force applied when the player jumps
        [SerializeField]
        private int _maxJumps = 2; // Maximum number of jumps allowed

        [Header("Ground Check")]
        [SerializeField]
        private Transform _groundCheckPos; // Position to check if the player is grounded
        [SerializeField]
        private Vector2 _groundCheckSize = new Vector2(0.5f, 0.05f); // Size of the ground check box
        [SerializeField]
        private LayerMask _platformsLayer; // Layer mask for platforms

        [Header("Wall Check")]
        [SerializeField]
        private Transform _wallCheckPos; // Position to check if the player is against a wall
        [SerializeField]
        private Vector2 _wallCheckSize = new Vector2(0.5f, 0.05f); // Size of the wall check box

        [Header("Gravity")]
        [SerializeField]
        private float _baseGravity = 2f; // Base gravity
        [SerializeField]
        private float _maxFallSpeed = 18f; // Maximum fall speed
        [SerializeField]
        private float _fallSpeedMultiplier = 2f; // Multiplier for fall speed

        [Header("Wall Movement")]
        [SerializeField]
        private float _wallSlideSpeed = 2f; // Speed when sliding against a wall

        [Header("Wall Jumping")]
        [SerializeField]
        private Vector2 _wallJumpForce = new Vector2(5f, 10f); // Force applied when wall jumping

        [SerializeField]
        private BoolVariable _isFacingRightVariable; // Variable indicating if the player is facing right

        private float _horizontalMovement; // Input for horizontal movement
        private Rigidbody2D _rb; // Rigidbody component
        private int _jumpsRemaining; // Number of jumps remaining
        private bool _isWallSliding; // Indicates if the player is sliding against a wall
        private bool _isGrounded; // Indicates if the player is grounded

        private bool _isWallJumping; // Indicates if the player is currently wall jumping
        private float _wallJumpDirection; // Direction of wall jump
        private float _wallJumpTime = 0.5f; // Duration of wall jump
        private float _wallJumpTimer; // Timer for wall jump duration

        /// <summary>
        /// This funtion is called by the new Player Input when moving
        /// </summary>
        /// <param name="context">contains the value of the movement</param>
        public void Move(InputAction.CallbackContext context)
        {
            _horizontalMovement = context.ReadValue<Vector2>().x;
        }

        /// <summary>
        /// Callback method for jump input.
        /// </summary>
        /// <param name="context">The context of the input.</param>
        public void Jump(InputAction.CallbackContext context)
        {
            if (_jumpsRemaining > 0)
            {
                if (context.performed)
                {
                    // Execute a full jump when the jump button is held down
                    _rb.velocity = new Vector2(_rb.velocity.x, _jumpForce);
                    _jumpsRemaining--;
                    _performJumpEvent.Raise();
                }
                else if (context.canceled)
                {
                    // Execute a short jump when the jump button is tapped
                    _rb.velocity = new Vector2(_rb.velocity.x, _rb.velocity.y * 0.5f);
                    _jumpsRemaining--;
                    _performJumpEvent.Raise();
                }
            }

            //Wal jump
            if (context.performed && _wallJumpTimer > 0f)
            {
                _isWallJumping = true;
                _rb.velocity = new Vector2(_wallJumpDirection * _wallJumpForce.x, _wallJumpForce.y); //Jump away from wall
                _wallJumpTimer = 0f;
                _performJumpEvent.Raise();

                //Force flip
                if (transform.localScale.x != _wallJumpDirection)
                {
                    _isFacingRightVariable.SetValue(!_isFacingRightVariable.Value);
                    Vector3 ls = transform.localScale;
                    ls.x *= -1f;
                    transform.localScale = ls;
                }

                // Schedule the cancellation of the wall jump after a short delay
                Invoke(nameof(CancelWallJump), _wallJumpTime + 0.1f); //Wall jump = 0.5 -> jump again = 0.6
            }
        }

        void Start()
        {
            // Initialize player properties
            _rb = GetComponent<Rigidbody2D>();
            _isFacingRightVariable.SetValue(true);
        }

        void Update()
        {
            // Read inputs and control the character
            GroundedCheck();
            ProcessGravity();
            ProcessWallSlide();
            ProcessWallJump();

            // Move the player horizontally
            _rb.velocity = new Vector2(_speed * _horizontalMovement, _rb.velocity.y);
            if (!_isWallJumping)
            {
                _rb.velocity = new Vector2(_speed * _horizontalMovement, _rb.velocity.y);
                Flip();
            }
        }

        private void OnEnable()
        {
            ResetJumps();
            _rb = GetComponent<Rigidbody2D>();
            _resetJumpsEvent.OnRaise += ResetJumps;
        }

        private void OnDisable()
        {
            _resetJumpsEvent.OnRaise -= ResetJumps;
        }

        /// <summary>
        /// Checks if the player is in contact with a wall.
        /// </summary>
        /// <returns>True if the player is in contact with a wall, otherwise false</returns>
        private bool IsInWall()
        {
            return Physics2D.OverlapBox(_wallCheckPos.position, _wallCheckSize, 0, _platformsLayer);
        }

        /// <summary>
        /// Checks if the player is grounded and updates the grounded state accordingly.
        /// </summary>
        private void GroundedCheck()
        {
            bool isGrounded = Physics2D.OverlapBox(_groundCheckPos.position, _groundCheckSize, 0, _platformsLayer);

            // Update the grounded state if it has changed
            if (isGrounded != _isGrounded)
            {
                _isGrounded = isGrounded;
                ResetJumps(); // Reset jumps if the player becomes grounded
            }
        }

        /// <summary>
        /// Processes the player's ability to slide against a wall.
        /// </summary>
        private void ProcessWallSlide()
        {
            //Not grounded and on a wall and movement != 0
            if (!_isGrounded && IsInWall())
            {
                // Player is not grounded, touching a wall, and moving
                _isWallSliding = true;
                _rb.velocity = new Vector2(_rb.velocity.x, Mathf.Max(_rb.velocity.y, -_wallSlideSpeed)); //Caps fall rate
            }
            else
            {
                // Player is not wall sliding
                _isWallSliding = false;
            }
        }

        /// <summary>
        /// Processes the player's ability to wall jump.
        /// </summary>
        private void ProcessWallJump()
        {
            if (_isWallSliding)
            {
                // Player is wall sliding, prepare for a wall jump
                _isWallJumping = false;
                _wallJumpDirection = -transform.localScale.x; // Determine the direction of the wall jump
                _wallJumpTimer = _wallJumpTime;

                // Cancel any scheduled wall jump cancellation
                CancelInvoke(nameof(CancelWallJump));
            }
            else if (_wallJumpTimer > 0f)
            {
                // Countdown the wall jump timer
                _wallJumpTimer -= Time.deltaTime;
            }
        }

        /// <summary>
        /// Cancels the wall jump.
        /// </summary>
        private void CancelWallJump()
        {
            _isWallJumping = false;
        }

        /// <summary>
        /// Processes gravity affecting the player.
        /// </summary>
        private void ProcessGravity()
        {
            if (_rb.velocity.y < 0)
            {
                // Increase fall speed when falling
                _rb.gravityScale = _baseGravity * _fallSpeedMultiplier; //Fall faster
                _rb.velocity = new Vector2(_rb.velocity.x, Mathf.Max(_rb.velocity.y, -_maxFallSpeed));
            }
            else
            {
                // Reset gravity scale when not falling
                _rb.gravityScale = _baseGravity;
            }
        }

        /// <summary>
        /// Flips the player's facing direction based on input.
        /// </summary>
        private void Flip()
        {
            // Flip the player's facing direction if moving left or right
            if (_isFacingRightVariable.Value && _horizontalMovement < 0 || !_isFacingRightVariable.Value && _horizontalMovement > 0)
            {
                _isFacingRightVariable.SetValue(!_isFacingRightVariable.Value);
                Vector3 ls = transform.localScale;
                ls.x *= -1f;
                transform.localScale = ls;
            }
        }

        /// <summary>
        /// Resets the number of jumps remaining to the maximum allowed.
        /// </summary>
        private void ResetJumps()
        {
            _jumpsRemaining = _maxJumps;
        }
    }
}
