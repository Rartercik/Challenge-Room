using Sirenix.OdinInspector;
using UnityEngine;

namespace Game
{
    [RequireComponent(typeof(PlayerMovement))]
    [RequireComponent(typeof(PlayerInteraction))]
    public class Player : MonoBehaviour
    {
        [Header("Required Components:")]
        [Space(5)]
        [SerializeField] private PlayerMovement _movement;
        [SerializeField] private PlayerInteraction _interaction;

        [Button]
        private void SetRequiredComponents()
        {
            _movement = GetComponent<PlayerMovement>();
            _interaction = GetComponent<PlayerInteraction>();
        }

        public bool CanJump => _movement.CanJump;

        public void Move(Vector2 direction)
        {
            _movement.Move(direction);
        }

        public void StopMove()
        {
            _movement.StopMove();
        }

        public void Jump()
        {
            _movement.Jump();
        }

        public void TryApplyDamage()
        {
            _interaction.TryApplyDamage();
        }

        public void MakeImmortal()
        {
            _interaction.MakeImmortal();
        }
    }
}
