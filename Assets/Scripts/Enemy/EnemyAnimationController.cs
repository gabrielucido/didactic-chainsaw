using UnityEngine;

public class EnemyAnimationController : MonoBehaviour
{
    private PlayerManager _playerManager;
    public GameObject projectile;

    void Awake()
    {
        _playerManager = GetComponentInParent<PlayerManager>();
    }

    private void OnEnable()
    {
        // _playerManager.Jumped += OnJumped;
        // _playerManager.Attacked += OnAttacked;
        // _player.GroundedChanged += OnGroundedChanged;
        //
        // _moveParticles.Play();
    }

    private void OnDisable()
    {
        // _playerManager.Jumped -= OnJumped;
        // _playerManager.Attacked -= OnAttacked;
        // _player.GroundedChanged -= OnGroundedChanged;

        // _moveParticles.Stop();
    }

    // private void OnJumped()
    // {
    //     Debug.Log("Jumped animation!!!!");
    // }

    // private void OnAttacked()
    // {
    //     var facingRight = _playerManager.IsFacingRight();
    //     var auxSpawnPosition = transform.position;
    //     auxSpawnPosition.x = auxSpawnPosition.x + (facingRight ? 1 : -1);
    //     var instance =
    //         Instantiate(projectile, auxSpawnPosition, transform.rotation); //, auxSpawnPosition, Quaternion.identity);
    //     instance.GetComponent<ProjectileController>().goingLeft = !facingRight;
    // }


#if UNITY_EDITOR
    private void OnValidate()
    {
        if (projectile == null)
            Debug.LogWarning("Please assign a Projectile Prefab to the Player Animation Projectile slot", this);
    }
#endif
}