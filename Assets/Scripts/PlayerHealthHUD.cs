using AlbertoGarrido.Platformer.ScriptableObjects.Variables;
using UnityEngine;
using UnityEngine.UI;

namespace AlbertoGarrido.Platformer
{
    public class PlayerHealthHUD : MonoBehaviour
    {
        [SerializeField]
        private IntVariable _playerHealthVariable;
        [SerializeField]
        private IntVariable _playerMaxHealthVariable;
        [SerializeField]
        private Image _playerHealthImage;


        private void OnEnable()
        {
            _playerHealthVariable.OnValueChanged += PlayerHealthChanged;
        }

        private void OnDisable()
        {
            _playerHealthVariable.OnValueChanged -= PlayerHealthChanged;
        }

        private void PlayerHealthChanged(int playerHealth)
        {
            float fillAmount = (float)playerHealth / _playerMaxHealthVariable.Value;
            _playerHealthImage.fillAmount = fillAmount;
        }
    }
}