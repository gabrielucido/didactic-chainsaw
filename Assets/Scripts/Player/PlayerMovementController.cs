using UnityEngine;

[RequireComponent(typeof(PlayerPhysicsController))]
public class PlayerMovementController : PlayerBase
{
    private Rigidbody2D _rb;
    private PlayerPhysicsController _playerPhysicsController;

    // private Vector2 _move = Vector2.zero;
    private Vector2 _frameVelocity;
    private bool _grounded;

    // public event Action Jumped;

    protected override void Awake()
    {
        base.Awake();
        _rb = GetComponent<Rigidbody2D>();
        _playerPhysicsController = GetComponent<PlayerPhysicsController>();
    }

    void FixedUpdate()
    {
        HandleMovement();
        ApplyMovement();
    }

    private void OnEnable()
    {
        _playerPhysicsController.GroundedChanged += OnGroundedChanged;
        _playerPhysicsController.HitCeiling += OnHitCeiling;
    }

    private void OnDisable()
    {
        _playerPhysicsController.GroundedChanged -= OnGroundedChanged;
        _playerPhysicsController.HitCeiling -= OnHitCeiling;
    }

    #region Horizontal

    private void HandleMovement()
    {
        if (Player.data.move.x == 0)
        {
            var deceleration = _grounded
                ? Player.data.groundDeceleration
                : Player.data.airDeceleration;
            _frameVelocity.x = Mathf.MoveTowards(_frameVelocity.x, 0, deceleration * Time.fixedDeltaTime);
        }
        else
        {
            _frameVelocity.x = Mathf.MoveTowards(_frameVelocity.x,
                Player.data.move.x * Player.data.maxSpeed,
                Player.data.acceleration * Time.fixedDeltaTime);
        }
    }

    #endregion

    // #region Jump
    //
    // public bool _jumpToConsume;
    // public bool _bufferedJumpUsable;
    //
    // public bool _endedJumpEarly;
    // private bool _coyoteUsable;
    // private float _jumpPressedTime;
    //
    // private bool HasBufferedJump =>
    //     _bufferedJumpUsable && Player.time < _jumpPressedTime + playerData.jumpBuffer;
    //
    // private bool CanUseCoyote =>
    //     _coyoteUsable && !_grounded && _time < _frameLeftGrounded + playerData.coyoteTime;
    //
    // private void HandleJump()
    // {
    //     if (!_endedJumpEarly && !_grounded && !_frameInput.JumpHeld && _rb.velocity.y > 0) _endedJumpEarly = true;
    //
    //     if (!_jumpToConsume && !HasBufferedJump) return;
    //
    //     if (_grounded || CanUseCoyote) ExecuteJump();
    //     _jumpToConsume = false;
    // }
    //
    // private void ExecuteJump()
    // {
    //     _endedJumpEarly = false;
    //     _jumpPressedTime = 0;
    //     _bufferedJumpUsable = false;
    //     _coyoteUsable = false;
    //     _frameVelocity.y = Player.data.jumpPower;
    //     Jumped?.Invoke();
    // }
    //
    // #endregion

    #region Event Handling

    private void OnGroundedChanged(bool grounded)
    {
        _grounded = grounded;
        // if (grounded)
        // {
        //     _coyoteUsable = true;
        //     _bufferedJumpUsable = true;
        //     _endedJumpEarly = false;
        // }
    }

    private void OnHitCeiling()
    {
        _frameVelocity.y = 0;
    }

    #endregion


    private void ApplyMovement()
    {
        _rb.velocity = _frameVelocity;
    }
}