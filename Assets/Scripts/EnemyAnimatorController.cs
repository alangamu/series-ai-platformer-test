using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class EnemyAnimatorController : MonoBehaviour
    {
        [SerializeField]
        private Animator _animator;
        [SerializeField]
        private Rigidbody2D _rb;

        private void Update()
        {
            _animator.SetFloat("speed", _rb.velocity.magnitude);
        }
    }
}