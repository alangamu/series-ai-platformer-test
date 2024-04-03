using UnityEngine;

namespace Assets.Scripts
{
    public class MovingPlatform : MonoBehaviour
    {
        public float speed = 2f; // Speed of the platform movement
        public Vector2 direction = Vector2.right; // Direction in which the platform moves

        private Vector2 initialPosition; // Initial position of the platform
        private Vector2 targetPosition; // Target position towards which the platform moves

        void Start()
        {
            // Store the initial position of the platform
            initialPosition = transform.position;

            // Calculate the target position based on the initial position and direction
            targetPosition = initialPosition + direction;
        }

        void Update()
        {
            // Move the platform towards the target position with given speed
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

            // If the platform reaches the target position, swap initial and target positions
            if ((Vector2)transform.position == targetPosition)
            {
                Debug.Log("swap");
                Vector2 temp = initialPosition;
                initialPosition = targetPosition;
                targetPosition = temp + direction;
            }
        }

        // Draw gizmos to visualize the movement path of the platform
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(initialPosition, targetPosition);
        }
    }
}