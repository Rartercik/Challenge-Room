using UnityEngine;
using Game;

namespace Interface
{
    public class InputPC : MonoBehaviour
    {
        [SerializeField] private Player _player;

        private bool _stopped;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space) && _player.CanJump)
            {
                _player.Jump();
            }
        }

        private void FixedUpdate()
        {
            if (Input.GetKey(KeyCode.A))
            {
                _player.Move(Vector2.left);
                _stopped = false;
            }
            else if (Input.GetKey(KeyCode.D))
            {
                _player.Move(Vector2.right);
                _stopped = false;
            }
            else if (_stopped == false)
            {
                _player.StopMove();
                _stopped = true;
            }
        }
    }
}
