using UnityEngine;

namespace AlbertoGarrido.Platformer.Platforms
{
    /// <summary>
    /// This script controls the movement of a platform between two designated points,
    /// and allows the player to ride on it.
    /// </summary>
    public class MovingPlatformController : MonoBehaviour
    {
        // Starting point of the platform
        [SerializeField]
        private Transform _pointA; 

        // Finish point of the platform
        [SerializeField]
        private Transform _pointB; 

        // Speed of the platform's movement
        [SerializeField]
        private float _moveSpeed = 2f; 

        // Destination position for the platform
        private Vector3 _nextPosition; 

        private void Start()
        {
            // Set the initial destination to point B
            _nextPosition = _pointB.position; 
        }

        private void Update()
        {
            // Move the platform towards the next position
            transform.position = Vector3.MoveTowards(transform.position, _nextPosition, _moveSpeed * Time.deltaTime);

            // Check if the platform has reached its destination
            if (transform.position == _nextPosition) 
            {
                // Set the next destination based on current position
                _nextPosition = (_nextPosition == _pointA.position) ? _pointB.position : _pointA.position; //set the next destination
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            // Check if the collided object is the player
            if (collision.gameObject.CompareTag("Player")) 
            {
                // Make the player a child of the platform
                collision.gameObject.transform.parent = transform;
            }
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            // Check if the collided object is the player
            if (collision.gameObject.CompareTag("Player"))
            {
                // Unparent the player from the platform
                collision.gameObject.transform.parent = null;
            }
        }
    }
}