using AlbertoGarrido.Platformer.ScriptableObjects.Variables;
using AlbertoGarrido.Platformer.ScriptableObjects.Events;
using UnityEngine;
using UnityEngine.InputSystem;

namespace AlbertoGarrido.Platformer.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerMovement : MonoBehaviour
    {
        // Define properties like speed, jumpForce, etc here
        [SerializeField]
        private GameEvent _performJumpEvent;
        [SerializeField]
        private GameEvent _resetJumpsEvent;

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
        private LayerMask _platformsLayer;

        [Header("Wall Check")]
        [SerializeField]
        private Transform _wallCheckPos;
        [SerializeField]
        private Vector2 _wallCheckSize = new Vector2(0.5f, 0.05f);

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

        [SerializeField]
        private BoolVariable _isFacingRightVariable;

        private float _horizontalMovement;
        private Rigidbody2D _rb;
        private int _jumpsRemaining;
        //private bool _isFacingRight = true;
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

        public void Jump(InputAction.CallbackContext context)
        {
            if (_jumpsRemaining > 0)
            {
                if (context.performed)
                {
                    //Hold down jump button
                    _rb.velocity = new Vector2(_rb.velocity.x, _jumpForce);
                    _jumpsRemaining--;
                    _performJumpEvent.Raise();
                }
                else if (context.canceled)
                {
                    //Light tap of jump button
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
                    //TODO: dont repeat code
                    _isFacingRightVariable.SetValue(!_isFacingRightVariable.Value);
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
            _isFacingRightVariable.SetValue(true);
        }

        void Update()
        {
            // Read inputs and control the character
            GroundedCheck();
            ProcessGravity();
            ProcessWallSlide();
            ProcessWallJump();

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

        private bool IsInWall()
        {
            return Physics2D.OverlapBox(_wallCheckPos.position, _wallCheckSize, 0, _platformsLayer);
        }

        private void GroundedCheck()
        {
            bool isGrounded = Physics2D.OverlapBox(_groundCheckPos.position, _groundCheckSize, 0, _platformsLayer);

            if (isGrounded != _isGrounded)
            {
                _isGrounded = isGrounded;
                ResetJumps();
            }
        }

        private void ProcessWallSlide()
        {
            //Not grounded and on a wall and movement != 0
            if (!_isGrounded && IsInWall())
            {
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
            if (_isFacingRightVariable.Value && _horizontalMovement < 0 || !_isFacingRightVariable.Value && _horizontalMovement > 0)
            {
                //_isFacingRight = !_isFacingRight;
                _isFacingRightVariable.SetValue(!_isFacingRightVariable.Value);
                Vector3 ls = transform.localScale;
                ls.x *= -1f;
                transform.localScale = ls;
            }
        }

        private void ResetJumps()
        {
            _jumpsRemaining = _maxJumps;
        }
    }
}
