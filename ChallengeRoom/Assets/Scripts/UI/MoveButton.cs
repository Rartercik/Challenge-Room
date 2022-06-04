using UnityEngine;

namespace UI
{
    public class MoveButton : MonoBehaviour
    {
        [SerializeField] Player _player;
        [SerializeField] Vector2 _direction;
        [SerializeField] MoveButton _anotherButton;

        public bool IsActive { get; private set; }

        private void FixedUpdate()
        {
            if(IsActive)
                _player.Move(_direction);
        }

        public void TryMovePlayer()
        {
            if (_anotherButton.IsActive == false)
                IsActive = true;
        }

        public void StopMoving()
        {
            _player.StopMove();
            IsActive = false;
        }
    }
}
