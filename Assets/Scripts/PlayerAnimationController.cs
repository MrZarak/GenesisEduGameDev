using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    [SerializeField] private PlayerAnimationData data;

    private PlayerMovementController _movementController;
    private Animator _animator;
    private Rigidbody2D _rb;
    private SpriteRenderer _spriteRenderer;

    private void Start()
    {
        _movementController = GetComponent<PlayerMovementController>();
        _animator = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void LateUpdate()
    {
        _animator.SetBool("running", _movementController.HorizontalMovement != 0);
        _animator.SetBool("grounded", _movementController.OnGround);
        _animator.SetFloat("yVelocity", _rb.velocity.y);

        _spriteRenderer.flipX = !_movementController.RotatedRight;
        data.dashTrail.emitting = _movementController.Dashing;
    }
}