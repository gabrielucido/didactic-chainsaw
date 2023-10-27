using System;
using UnityEngine;

public struct FrameInput
{
    public bool JumpDown;
    public bool JumpHeld;
    public Vector2 Move;
    public bool Attack;
}

public interface IPlayerController
{
    public event Action<bool, float> GroundedChanged;

    public event Action Jumped;
    public Vector2 FrameInput { get; }

    public event Action Attacked;
    // public event Action LookedLeft;
    // public event Action LookedRight;

    public bool isFacingRight();
}

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class PlayerController : MonoBehaviour, IPlayerController
{
    [SerializeField] private PlayerControllerAttributes playerControllerAttributes;
    private Rigidbody2D _rb;
    private CapsuleCollider2D _col;
    private SpriteRenderer _spriteRenderer;
    private FrameInput _frameInput;
    private Vector2 _frameVelocity;
    private bool _cachedQueryStartInColliders;
    private bool _facingRight = true;
    private bool _attacking;

    #region Interface

    public Vector2 FrameInput => _frameInput.Move;
    public event Action<bool, float> GroundedChanged;
    public event Action Jumped;

    public event Action Attacked;

    // public event Action LookedLeft;
    // public event Action LookedRight;

    #endregion

    private float _time;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _col = GetComponent<CapsuleCollider2D>();
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _grounded = true;

        _cachedQueryStartInColliders = Physics2D.queriesStartInColliders;
    }

    private void Update()
    {
        _time += Time.deltaTime;
        GatherInput();
    }

    private void GatherInput()
    {
        _frameInput = new FrameInput
        {
            JumpDown = Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.C),
            JumpHeld = Input.GetButton("Jump") || Input.GetKey(KeyCode.C),
            Move = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")),
            Attack = Input.GetButtonDown("Attack") || Input.GetKeyDown(KeyCode.Mouse0)
        };

        if (playerControllerAttributes.SnapInput)
        {
            _frameInput.Move.x = Mathf.Abs(_frameInput.Move.x) < playerControllerAttributes.HorizontalDeadZoneThreshold
                ? 0
                : Mathf.Sign(_frameInput.Move.x);
            _frameInput.Move.y = Mathf.Abs(_frameInput.Move.y) < playerControllerAttributes.VerticalDeadZoneThreshold
                ? 0
                : Mathf.Sign(_frameInput.Move.y);
        }

        if (_frameInput.JumpDown)
        {
            _jumpToConsume = true;
            _timeJumpWasPressed = _time;
        }

        if (_frameInput.Move.x > 0 && !_facingRight) Flip();
        else if (_frameInput.Move.x < 0 && _facingRight) Flip();

        if (_frameInput.Attack)
        {
            _attacking = true;
        }
    }

    private void FixedUpdate()
    {
        CheckCollisions();

        HandleJump();
        HandleDirection();
        HandleGravity();

        ApplyMovement();
        HandleAttack();
        handleAttackCooldown();
    }

    #region Collisions

    private float _frameLeftGrounded = float.MinValue;
    private bool _grounded;

    private void CheckCollisions()
    {
        Physics2D.queriesStartInColliders = false;

        // Ground and Ceiling
        bool groundHit = Physics2D.CapsuleCast(_col.bounds.center, _col.size, _col.direction, 0, Vector2.down,
            playerControllerAttributes.GrounderDistance, ~playerControllerAttributes.PlayerLayer);
        bool ceilingHit = Physics2D.CapsuleCast(_col.bounds.center, _col.size, _col.direction, 0, Vector2.up,
            playerControllerAttributes.GrounderDistance, ~playerControllerAttributes.PlayerLayer);

        // Hit a Ceiling
        if (ceilingHit)
        {
            _frameVelocity.y = Mathf.Min(0, _frameVelocity.y);
        }

        // Landed on the Ground
        if (!_grounded && groundHit)
        {
            _grounded = true;
            _coyoteUsable = true;
            _bufferedJumpUsable = true;
            _endedJumpEarly = false;
            GroundedChanged?.Invoke(true, Mathf.Abs(_frameVelocity.y));
        }
        // Left the Ground
        else if (_grounded && !groundHit)
        {
            _grounded = false;
            _frameLeftGrounded = _time;
            GroundedChanged?.Invoke(false, 0);
        }

        Physics2D.queriesStartInColliders = _cachedQueryStartInColliders;
    }

    #endregion

    #region Jumping

    private bool _jumpToConsume;
    private bool _bufferedJumpUsable;
    private bool _endedJumpEarly;
    private bool _coyoteUsable;
    private float _timeJumpWasPressed;

    private bool HasBufferedJump =>
        _bufferedJumpUsable && _time < _timeJumpWasPressed + playerControllerAttributes.JumpBuffer;

    private bool CanUseCoyote =>
        _coyoteUsable && !_grounded && _time < _frameLeftGrounded + playerControllerAttributes.CoyoteTime;

    private void HandleJump()
    {
        if (!_endedJumpEarly && !_grounded && !_frameInput.JumpHeld && _rb.velocity.y > 0) _endedJumpEarly = true;

        if (!_jumpToConsume && !HasBufferedJump) return;

        if (_grounded || CanUseCoyote) ExecuteJump();
        _jumpToConsume = false;
    }

    private void ExecuteJump()
    {
        _endedJumpEarly = false;
        _timeJumpWasPressed = 0;
        _bufferedJumpUsable = false;
        _coyoteUsable = false;
        _frameVelocity.y = playerControllerAttributes.JumpPower;
        Jumped?.Invoke();
    }

    #endregion

    #region Horizontal

    private void HandleDirection()
    {
        if (_frameInput.Move.x == 0)
        {
            var deceleration = _grounded
                ? playerControllerAttributes.GroundDeceleration
                : playerControllerAttributes.AirDeceleration;
            _frameVelocity.x = Mathf.MoveTowards(_frameVelocity.x, 0, deceleration * Time.fixedDeltaTime);
        }
        else
        {
            _frameVelocity.x = Mathf.MoveTowards(_frameVelocity.x,
                _frameInput.Move.x * playerControllerAttributes.MaxSpeed,
                playerControllerAttributes.Acceleration * Time.fixedDeltaTime);
        }
    }

    private void Flip()
    {
        _facingRight = !_facingRight;
        _spriteRenderer.flipX = !_facingRight;
    }

    #endregion

    #region Gravity

    private void HandleGravity()
    {
        if (_grounded && _frameVelocity.y <= 0f)
        {
            _frameVelocity.y = playerControllerAttributes.GroundingForce;
        }
        else
        {
            var inAirGravity = playerControllerAttributes.FallAcceleration;
            if (_endedJumpEarly && _frameVelocity.y > 0)
                inAirGravity *= playerControllerAttributes.JumpEndEarlyGravityModifier;
            _frameVelocity.y =
                Mathf.MoveTowards(_frameVelocity.y, -playerControllerAttributes.MaxFallSpeed,
                    inAirGravity * Time.fixedDeltaTime);
        }
    }

    #endregion


    #region Attack

    private float _attackCooldown = 0;

    private void handleAttackCooldown()
    {
        if (_attackCooldown > 0)
        {
            _attackCooldown -= Time.fixedDeltaTime;
        }
        else
        {
            _attackCooldown = 0;
        }
    }

    private void HandleAttack()
    {
        if (_attacking && _attackCooldown == 0)
        {
            _attackCooldown = playerControllerAttributes.attackCooldown;
            ExecuteAttack();
            Attacked?.Invoke();
        }

        _attacking = false;
    }

    private void ExecuteAttack()
    {
        var hit = Physics2D.Raycast(transform.position, _facingRight ? Vector2.right : Vector2.left, 100f,
            ~LayerMask.GetMask("Player", "Ignore Raycast"));

        if (hit.collider)
        {
            if (hit.collider.gameObject.CompareTag("Enemy"))
            {
                hit.collider.gameObject.GetComponent<EnemyController>()
                    .TakeDamage(playerControllerAttributes.attackDamage);
            }
        }
    }

    #endregion

    private void ApplyMovement() => _rb.velocity = _frameVelocity;

    public bool isFacingRight() => _facingRight;

#if UNITY_EDITOR
    private void OnValidate()
    {
        if (playerControllerAttributes == null)
            Debug.LogWarning("Please assign a ScriptableStats asset to the Player Controller's Stats slot", this);
    }
#endif
}