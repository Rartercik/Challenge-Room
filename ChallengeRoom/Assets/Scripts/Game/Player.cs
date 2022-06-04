using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
public class Player : MonoBehaviour
{
    [SerializeField] float _speed;
    [SerializeField] float _jumpPower;
    [SerializeField] LayerMask _groundLayer;
    [SerializeField] float _distanceToGround;
    [SerializeField] GameObject _gameOverWindow;

    private Rigidbody2D _rigidbody;
    private Animator _animator;
    private SpriteRenderer _renderer;
    private Transform _transform;

    public bool CanJump => Physics2D.Raycast(_transform.position, Vector2.down, _distanceToGround, _groundLayer);

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _renderer = GetComponent<SpriteRenderer>();
        _transform = transform;
    }

    public void StartDying()
    {
        if(_animator.GetCurrentAnimatorStateInfo(0).IsName("Death") == false)
            _animator.SetTrigger("Die");
    }

    public void Move(Vector2 diraction)
    {
        _rigidbody.velocity = new Vector2(_speed * diraction.x, _rigidbody.velocity.y);
        var isLeft = diraction.x < 0;
        _renderer.flipX = isLeft;
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
        _rigidbody.AddForce(Vector2.up * _jumpPower);
        _animator.SetTrigger("Jump");
    }

    private void Die()
    {
        Time.timeScale = 0;
        _gameOverWindow.SetActive(true);
    }
}
