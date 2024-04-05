using AlbertoGarrido.Platformer.ScriptableObjects.Variables;
using UnityEngine;
using UnityEngine.UI;

namespace AlbertoGarrido.Platformer
{
    /// <summary>
    /// Updates the player health UI.
    /// </summary>
    public class PlayerHealthHUD : MonoBehaviour
    {
        [SerializeField]
        private IntVariable _playerHealthVariable; // Reference to the player's current health variable
        [SerializeField]
        private IntVariable _playerMaxHealthVariable; // Reference to the player's maximum health variable
        [SerializeField]
        private Image _playerHealthImage; // Reference to the image component representing player health


        private void OnEnable()
        {
            _playerHealthVariable.OnValueChanged += PlayerHealthChanged;
        }

        private void OnDisable()
        {
            _playerHealthVariable.OnValueChanged -= PlayerHealthChanged;
        }

        /// <summary>
        /// Updates the player health UI based on the current health value.
        /// </summary>
        /// <param name="playerHealth">The current health value of the player.</param>
        private void PlayerHealthChanged(int playerHealth)
        {
            // Calculate the fill amount of the health bar based on the current and maximum health values
            float fillAmount = (float)playerHealth / _playerMaxHealthVariable.Value;
            // Update the fill amount of the health bar image
            _playerHealthImage.fillAmount = fillAmount;
        }
    }
}