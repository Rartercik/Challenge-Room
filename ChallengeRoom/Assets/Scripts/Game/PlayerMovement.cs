using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Collider2D))]
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(Animator))]
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private Collider2D _jumpBox;
        [SerializeField] private float _speed;
        [SerializeField] private float _jumpPower;
        [SerializeField] private float _maxAngleToGround;
        [SerializeField] private float _maxSpeedToJump = 0.5f;
        [SerializeField] private LayerMask _groundLayer;
        [SerializeField] private float _distanceToGround;

        [Space(30)]
        [Header("Required Components:")]
        [Space(5)]
        [SerializeField] private Transform _transform;
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private Animator _animator;

        private bool _inLeft;

        [Button]
        private void SetRequiredComponents()
        {
            _transform = transform;
            _rigidbody = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();        
        }

        public bool CanJump => CheckJumpAvailable();

        public void Move(Vector2 direction)
        {
            _rigidbody.velocity = new Vector2(_speed * direction.x, _rigidbody.velocity.y);
            SetFlipping(direction);

            _animator.SetTrigger("Run");
        }

        public void StopMove()
        {
            _rigidbody.velocity = new Vector2(0, _rigidbody.velocity.y);
            _animator.ResetTrigger("Run");
            _animator.SetTrigger("Idle");
        }

        public void Jump()
        {
            _rigidbody.AddForce(Vector2.up * _jumpPower, ForceMode2D.Impulse);
            _animator.SetTrigger("Jump");
        }

        private bool CheckJumpAvailable()
        {
            var collidedGrounds = GetInteractedColliders(_jumpBox, _groundLayer);
            if (collidedGrounds.Count() == 0) return false;

            var boxCenter = _jumpBox.bounds.center;
            var groundAngle = GetMinimalAngle(collidedGrounds, boxCenter);

            return groundAngle < _maxAngleToGround && _rigidbody.velocity.y <= _maxSpeedToJump;
        }

        private IEnumerable<Collider2D> GetInteractedColliders(Collider2D target, LayerMask layer)
        {
            var collidedGrounds = new List<Collider2D>();
            var filter = new ContactFilter2D();
            filter.SetLayerMask(layer);
            target.OverlapCollider(filter, collidedGrounds);

            return collidedGrounds;
        }

        private float GetMinimalAngle(IEnumerable<Collider2D> colliders, Vector3 targetPoint)
        {
            var minAngle = 360f;

            foreach (var collider in colliders)
            {
                var angle = GetAngleTo(collider, targetPoint);
                minAngle = Mathf.Min(minAngle, angle);
            }

            return minAngle;
        }

        private float GetAngleTo(Collider2D target, Vector3 targetPoint)
        {
            var groundPoint = (Vector3)target.ClosestPoint(targetPoint);
            var vectorToGround = groundPoint - targetPoint;
            vectorToGround.z = 0;

            return Vector3.Angle(Vector3.down, vectorToGround);
        }

        private void SetFlipping(Vector2 playerDirection)
        {
            var inLeft = playerDirection.x < 0;
            var flipped = _inLeft != inLeft;

            if (flipped)
            {
                var localScale = _transform.localScale;
                localScale.x *= -1;
                _transform.localScale = localScale;
            }

            _inLeft = inLeft;
        }
    }
}