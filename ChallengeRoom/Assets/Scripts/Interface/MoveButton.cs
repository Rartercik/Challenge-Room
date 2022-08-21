using UnityEngine;
using Game;

namespace Interface
{
    public class MoveButton : MonoBehaviour
    {
        [SerializeField] private Player _player;
        [SerializeField] private Vector2 _direction;
        [SerializeField] private MoveButton _anotherButton;

        public bool IsActive { get; private set; }

        private void FixedUpdate()
        {
            if (IsActive)
            {
                _player.Move(_direction);
            }
        }

        public void TryMovePlayer()
        {
            if (_anotherButton.IsActive == false)
            {
                IsActive = true;
            }
        }

        public void StopMoving()
        {
            _player.StopMove();
            IsActive = false;
        }
    }
}
