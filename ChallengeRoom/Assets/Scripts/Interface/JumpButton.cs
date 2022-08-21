using UnityEngine;
using Game;

namespace Interface
{
    public class JumpButton : MonoBehaviour
    {
        [SerializeField] private Player _player;

        public void TryJump()
        {
            if (_player.CanJump)
            {
                _player.Jump();
            }
        }
    }
}
