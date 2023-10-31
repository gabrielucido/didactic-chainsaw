using UnityEngine;

[RequireComponent(typeof(PlayerMovementController))]
public class PlayerAnimationController : PlayerBase
{
    private PlayerMovementController _movementController;
    private PlayerPhysicsController _physicsController;

    protected override void Awake()
    {
        base.Awake();
        _movementController = GetComponent<PlayerMovementController>();
        _physicsController = GetComponent<PlayerPhysicsController>();
    }

    private void OnEnable()
    {
        _movementController.Jumped += OnJumped;
        _physicsController.GroundedChanged += OnGroundChanged;
        // Player.Jumped += OnJumped;
        // _playerManager.Attacked += OnAttacked;
        // _player.GroundedChanged += OnGroundedChanged;
        //
        // _moveParticles.Play();
    }

    private void OnDisable()
    {
        _movementController.Jumped -= OnJumped;
        // _playerManager.Attacked -= OnAttacked;
        // _player.GroundedChanged -= OnGroundedChanged;
        //
        // _moveParticles.Stop();
    }

    private void OnJumped()
    {
        Debug.Log("Jumped animation!!!!");
    }

    private void OnGroundChanged(bool grounded)
    {
        Debug.Log("Grounded animation!!!!");
    }

    // private void OnAttacked()
    // {
    //     var facingRight = _playerManager.IsFacingRight();
    //     var auxSpawnPosition = transform.position;
    //     auxSpawnPosition.x = auxSpawnPosition.x + (facingRight ? 1 : -1);
    //     var instance =
    //         Instantiate(projectile, auxSpawnPosition, transform.rotation); //, auxSpawnPosition, Quaternion.identity);
    //     instance.GetComponent<ProjectileController>().goingLeft = !facingRight;
    // }
}