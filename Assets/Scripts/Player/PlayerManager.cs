using UnityEngine;

public struct FrameInput
{
    // public bool JumpDown;
    public bool JumpHeld;
    public Vector2 Move;
    public bool Attack;
}

public class PlayerManager : MonoBehaviour
{
    public PlayerData data;

    private void Awake()
    {
        // _rb = GetComponent<Rigidbody2D>();
        // _col = GetComponent<CapsuleCollider2D>();
        // _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        // _grounded = true;
        // healthPoints = playerData.maxHealthPoints;
    }

    private void Update()
    {
        data.playerPosition = transform.position;
    }

    // #region Collisions
    //
    // public float _frameLeftGrounded = float.MinValue;
    // public bool _grounded;
    //
    // #endregion
    //
    // #region Jumping
    //
    // public bool _jumpToConsume;
    // public bool _bufferedJumpUsable;
    //
    // public bool _endedJumpEarly;
    // private bool _coyoteUsable;
    // private float _timeJumpWasPressed;

    // private bool HasBufferedJump =>
    //     _bufferedJumpUsable && _time < _timeJumpWasPressed + playerData.jumpBuffer;

    // private bool CanUseCoyote =>
    //     _coyoteUsable && !_grounded && _time < _frameLeftGrounded + playerData.coyoteTime;

    // private void HandleJump()
    // {
    //     if (!_endedJumpEarly && !_grounded && !_frameInput.JumpHeld && _rb.velocity.y > 0) _endedJumpEarly = true;
    //
    //     if (!_jumpToConsume && !HasBufferedJump) return;
    //
    //     if (_grounded || CanUseCoyote) ExecuteJump();
    //     _jumpToConsume = false;
    // }

    // private void ExecuteJump()
    // {
    //     _endedJumpEarly = false;
    //     _timeJumpWasPressed = 0;
    //     _bufferedJumpUsable = false;
    //     _coyoteUsable = false;
    //     _frameVelocity.y = playerData.jumpPower;
    //     Jumped?.Invoke();
    // }

    // #endregion

    #region Horizontal

    // private void Flip()
    // {
    //     _facingRight = !_facingRight;
    //     _spriteRenderer.flipX = !_facingRight;
    // }

    // public bool IsFacingRight() => _facingRight;

    #endregion

    #region Attack

    // private float _attackCooldown = 0;

    // private void handleAttackCooldown()
    // {
    //     if (_attackCooldown > 0)
    //     {
    //         _attackCooldown -= Time.fixedDeltaTime;
    //     }
    //     else
    //     {
    //         _attackCooldown = 0;
    //     }
    // }

    // private void HandleAttack()
    // {
    //     if (_attacking && _attackCooldown == 0)
    //     {
    //         _attackCooldown = playerData.attackCooldown;
    //         ExecuteAttack();
    //         Attacked?.Invoke();
    //     }
    //
    //     _attacking = false;
    // }

    // private void ExecuteAttack()
    // {
    //     var hit = Physics2D.Raycast(transform.position, _facingRight ? Vector2.right : Vector2.left, 100f,
    //         ~GetMask("Player", "Ignore Raycast"));
    //
    //     if (hit.collider)
    //     {
    //         if (hit.collider.gameObject.CompareTag("Enemy"))
    //         {
    //             // ReSharper disable once Unity.PerformanceCriticalCodeInvocation
    //             hit.collider.gameObject.GetComponent<EnemyController>()
    //                 .TakeDamage(playerData.attackDamage);
    //         }
    //     }
    // }

    #endregion

    #region Death Condition

    // [SerializeField] private int healthPoints;

    // private void HandleDeath()
    // {
    //     if (healthPoints <= 0)
    //     {
    //         Destroy(gameObject);
    //         SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    //         Debug.Log("Player is dead!");
    //         // TODO: Invoke Death Event
    //     }
    // }

    // public void TakeDamage(int damage)
    // {
    //     healthPoints -= damage;
    // }

    #endregion

    #region Validation

#if UNITY_EDITOR
    private void OnValidate()
    {
        if (data == null)
        {
            Debug.LogError("Please assign a Player Data asset to the Player Manager data slot", this);
        }
    }
#endif

    #endregion
}