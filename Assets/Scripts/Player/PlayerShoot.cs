using AlbertoGarrido.Platformer.ScriptableObjects.Events;
using AlbertoGarrido.Platformer.ScriptableObjects.Variables;
using UnityEngine;
using UnityEngine.InputSystem;

namespace AlbertoGarrido.Platformer.Player
{
    public class PlayerShoot : MonoBehaviour
    {
        [SerializeField]
        private BoolVariable _isFacingRightVariable;
        [SerializeField]
        private AudioClipGameEvent _onShootAudioEvent;
        [SerializeField]
        private AudioClip _shootSound;

        public GameObject bulletPrefab; // Bullet prefab
        public Transform firePoint; // Point of origin for the shot
        public float bulletSpeed = 20f; // Force of the shot
        public float shootInterval = 0.5f; // Interval between shots

        private float shootTimer = 0f; // Timer to control the interval between shots

        void Update()
        {
            // Update the timer
            shootTimer += Time.deltaTime;
        }

        public void OnFire(InputAction.CallbackContext context)
        {
            // Check if shooting should occur
            if (context.started && shootTimer >= shootInterval)
            {
                Shoot();
                shootTimer = 0f; // Reset the timer
            }
        }

        void Shoot()
        {
            // Instantiate the bullet at the fire point
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, (_isFacingRightVariable.Value ? firePoint.rotation : Quaternion.Euler(Vector3.up * 180f)));

            _onShootAudioEvent.Raise(_shootSound);

            // Get the Rigidbody2D component of the bullet
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

            // Set the velocity of the bullet to move forward
            rb.velocity = (_isFacingRightVariable.Value ? firePoint.right : firePoint.right * -1) * bulletSpeed;
            
        }
    }
}