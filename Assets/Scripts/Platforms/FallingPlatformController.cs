using AlbertoGarrido.Platformer.ScriptableObjects.Events;
using System;
using System.Threading.Tasks;
using UnityEngine;

namespace AlbertoGarrido.Platformer.Platforms
{
    /// <summary>
    /// This script controls the behavior of a platform that falls after the player steps on it.
    /// </summary>
    public class FallingPlatformController : MonoBehaviour
    {
        // Time to wait before the platform falls after the player steps on it
        [SerializeField]
        private float _fallWait = 1f;

        // Time to wait before the platform vanishes after falling
        [SerializeField]
        private float _vanishWait = 1f;

        [SerializeField]
        private SpriteRenderer _visuals; // Reference to the SpriteRenderer component for visuals

        [SerializeField]
        private GameEvent _initializeEvent; // Event raised to initialize the platform

        // Flag to track if the platform is currently falling
        private bool _isFalling;
        // Rigidbody component of the platform
        private Rigidbody2D _rb;
        // Starting position of the platform
        private Vector2 _startingPosition;

        private void OnEnable()
        {
            _initializeEvent.OnRaise += Initialize;
        }

        private void OnDisable()
        {
            _initializeEvent.OnRaise -= Initialize;
        }

        /// <summary>
        /// Method called to initialize the platform
        /// </summary>
        private void Initialize()
        {
            // Reset platform position, enable visuals, and set falling flag to false
            transform.position = _startingPosition;
            _visuals.enabled = true;
            _isFalling = false;
            _rb.bodyType = RigidbodyType2D.Static; // Set Rigidbody type to Static initially
        }

        private void Awake()
        {
            _startingPosition = transform.position;
            // Get the Rigidbody component attached to the platform
            _rb = GetComponent<Rigidbody2D>();
        }

        /// <summary>
        /// Method called when a collision occurs
        /// </summary>
        /// <param name="collision">Information about the collision.</param>
        private async void OnCollisionEnter2D(Collision2D collision)
        {
            // Check if the platform is not already falling and if the collision involves the player
            if (!_isFalling && collision.gameObject.CompareTag("Player"))
            {
                // Await the platform to fall
                await Fall();
            }
        }

        /// <summary>
        /// Asynchronous method to make the platform fall
        /// </summary>
        /// <returns></returns>
        private async Task Fall()
        {
            // Set the flag indicating the platform is falling
            _isFalling = true;
            // Wait for the specified time before the platform falls
            await Task.Delay(TimeSpan.FromSeconds(_fallWait));
            // Change the Rigidbody type to make the platform fall
            _rb.bodyType = RigidbodyType2D.Dynamic;
            // Wait for the specified time before disabling the visuals (vanishing)
            await Task.Delay(TimeSpan.FromSeconds(_vanishWait)); 
            _visuals.enabled = false;
        }
    }
}