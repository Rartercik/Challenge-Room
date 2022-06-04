using UnityEngine;

public class JumpButton : MonoBehaviour
{
    [SerializeField] Player _player;

    public void TryJump()
    {
        if (_player.CanJump)
            _player.Jump();
    }
}
