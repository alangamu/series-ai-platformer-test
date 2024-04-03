using UnityEngine;

namespace AlbertoGarrido.Platformer
{
    public class EnemyPatrolAI : MonoBehaviour
    {
        [SerializeField]
        private Rigidbody2D _rb;
        [SerializeField]
        private Transform _pointA;
        [SerializeField] 
        private Transform _pointB;
        [SerializeField]
        private float _speed;
        [SerializeField]
        private bool _isFacingRight;

        private Transform _currentPoint;

        private void Awake()
        {
            _currentPoint = _pointB;
            if (transform.position.x < _currentPoint.position.x)
            {
                if (!_isFacingRight)
                {
                    Flip();
                }
            }
        }

        private void Update()
        {
            //Vector2 point = _currentPoint.position - transform.position;
            
            _rb.velocity = new Vector2((_isFacingRight) ? _speed : -_speed, 0f);

            if (Vector2.Distance(transform.position, _currentPoint.position) < 0.5f)
            {
                Flip();
                _currentPoint = _currentPoint == _pointB ? _pointA : _pointB;
            }
        }

        private void Flip()
        {
            _isFacingRight = !_isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1;
            transform.localScale = localScale;
        }
    }
}