using UnityEngine;

namespace AlbertoGarrido.Platformer
{
    /// <summary>
    /// This script controls the patrol behavior of an enemy character,
    /// moving it back and forth between two designated points.
    /// </summary>
    public class EnemyPatrolAI : MonoBehaviour
    {
        [SerializeField]
        private Rigidbody2D _rb; // Rigidbody component of the enemy character
        [SerializeField]
        private Transform _pointA; // Starting point of the enemy's patrol route
        [SerializeField] 
        private Transform _pointB; // Ending point of the enemy's patrol route
        [SerializeField]
        private float _speed; // Speed at which the enemy moves
        [SerializeField]
        private bool _isFacingRight; // Flag to track whether the enemy is facing right or left

        private Transform _currentPoint; // The current point the enemy is patrolling towards

        private void Awake()
        {
            _currentPoint = _pointA; // Set the initial patrol point to point A
            
            // Check if the enemy's initial position is to the left of the current point
            if (transform.position.x < _currentPoint.position.x)
            {
                // If the enemy is not facing right, flip its direction
                if (!_isFacingRight)
                {
                    Flip();
                }
            }
        }

        private void Update()
        {
            // Move the enemy towards the current patrol point with the specified speed
            _rb.velocity = new Vector2((_isFacingRight) ? _speed : -_speed, 0f);

            // Check if the enemy has reached the current patrol point
            if (Vector2.Distance(transform.position, _currentPoint.position) < 0.5f)
            {
                // Change direction by flipping and update the current patrol point
                Flip();
                // Change the current point to the next
                _currentPoint = _currentPoint == _pointB ? _pointA : _pointB;
            }
        }

        /// <summary>
        /// Method to flip the direction of the enemy visuals 
        /// </summary>
        private void Flip()
        {
            _isFacingRight = !_isFacingRight; // Toggle the facing direction flag
            Vector3 localScale = transform.localScale; // Get the current local scale of the enemy
            localScale.x *= -1; // Invert the scale on the x-axis to flip the direction
            transform.localScale = localScale; // Apply the new scale to the enemy
        }
    }
}