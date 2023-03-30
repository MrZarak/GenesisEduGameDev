using System.Collections;
using moving;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    [SerializeField] private PlayerMovementData data;

    private Rigidbody2D _rb;

    private bool _canDashInJump;
    private bool _jump;
    private bool _jumpStop;
    private float _timeSinceLastDashing;

    public InputSourcesHandler InputSourcesHandler { get; } = new();

    public float HorizontalMovement { get; private set; }
    public bool Dashing { get; private set; }
    public bool RotatedRight { get; private set; } = true;
    public bool OnGround { get; private set; }

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        OnGround = isOnGround();

        if (InputSourcesHandler.IsDown(InputCode.Dash) && (OnGround || _canDashInJump) &&
            _timeSinceLastDashing >= data.dashingCooldownTime)
        {
            StartCoroutine(RunDashingCoroutine());
            return;
        }

        HandleHorizontalMove();
        HandleJump();
    }

    private void FixedUpdate()
    {
        if (HandleDashingMovement())
        {
            return;
        }

        _timeSinceLastDashing += Time.fixedDeltaTime;
        HandleHorizontalMoveFixed();
        HandleJumpFixed();
    }

    private void HandleHorizontalMove()
    {
        HorizontalMovement = 0F;
        if (InputSourcesHandler.IsPressed(InputCode.MoveLeft))
        {
            HorizontalMovement -= 1F;
        }

        if (InputSourcesHandler.IsPressed(InputCode.MoveRight))
        {
            HorizontalMovement += 1F;
        }

        RotatedRight = HorizontalMovement switch
        {
            > 0 => true,
            < 0 => false,
            _ => RotatedRight
        };
    }

    private void HandleHorizontalMoveFixed()
    {
        if (HorizontalMovement == 0 && OnGround)
        {
            _rb.velocity = new Vector2(0, _rb.velocity.y);
            return;
        }

        var vx = HorizontalMovement * data.moveSpeed;

        if (!OnGround)
        {
            vx *= data.notGroundedMoveMultiplayer;
        }

        _rb.velocity = new Vector2(vx, _rb.velocity.y);
    }

    private void HandleJump()
    {
        if (!OnGround)
        {
            return;
        }

        _canDashInJump = true;

        var rbVelocity = _rb.velocity;

        if (InputSourcesHandler.IsDown(InputCode.Jump))
        {
            _canDashInJump = true;
            _rb.velocity = new Vector2(rbVelocity.x, data.jumpVelocity);
        }
        else if (InputSourcesHandler.IsUp(InputCode.Jump))
        {
            _rb.velocity = new Vector2(rbVelocity.x, rbVelocity.y * 0.5F);
        }
    }

    private void HandleJumpFixed()
    {
        var rbVelocity = _rb.velocity;

        if (_jump)
        {
            _jump = false;
            _rb.velocity = new Vector2(rbVelocity.x, data.jumpVelocity);
        }

        if (_jumpStop)
        {
            _jumpStop = false;
            _rb.velocity = new Vector2(rbVelocity.x, rbVelocity.y * 0.5F);
        }
    }

    private bool HandleDashingMovement()
    {
        if (!Dashing)
            return false;

        _rb.velocity = new Vector2(data.dashingPower * (RotatedRight ? 1 : -1), 0);

        return true;
    }

    private IEnumerator RunDashingCoroutine()
    {
        var previousGravity = _rb.gravityScale;

        Dashing = true;
        _rb.gravityScale = 0;
        _canDashInJump = false;

        yield return new WaitForSeconds(data.dashingTimeSeconds);

        if (_rb.gravityScale == 0)
        {
            _rb.gravityScale = previousGravity;
        }

        _timeSinceLastDashing = 0;
        Dashing = false;
    }

    private bool isOnGround()
    {
        return Physics2D.OverlapCircle(data.groundCheck.position, 0.2F, data.groundLayer);
    }
}