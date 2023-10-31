using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class EnemyController : MonoBehaviour
{
    [SerializeField] private EnemyData enemyData;

    private void Start()
    {
        healthPoints = enemyData.max_health_points;
    }

    private void FixedUpdate()
    {
        HandleDeathCondition();
        HandleFollowPlayer();
        ExecuteFollowPlayer();
        HandleAttack();
        HandleAttackCooldown();
    }

    #region Stats and Death Condition

    [SerializeField] private int healthPoints;

    private void HandleDeathCondition()
    {
        if (healthPoints <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void TakeDamage(int damage)
    {
        healthPoints -= damage;
    }

    #endregion

    #region Engage Player

    [SerializeField] private float targetDistanceToPlayer = 3;

    private float _distanceToPlayer;
    private float _targetDistanceToPlayerOffset;

    private GameObject _player;
    private bool _playerInRange;
    private bool _obstaclesBetweenPlayer;
    private bool _engagePlayer;

    private void HandleFollowPlayer()
    {
        if (_playerInRange)
        {
            // create the raycast
            var localTransform = transform;
            var position = localTransform.position;
            var playerPosition = _player.transform.position;
            var boxCastSize = new Vector2(.1f, localTransform.localScale.y - .2f);
            var deltaX = position.x - playerPosition.x;
            var hit = Physics2D.BoxCast(position, boxCastSize, 0f,
                deltaX < 0 ? Vector2.right : Vector2.left,
                100f, ~LayerMask.GetMask("Enemy", "Ignore Raycast"));

            if (hit.collider)
            {
                _obstaclesBetweenPlayer = !hit.collider.CompareTag("Player");
            }
        }
    }

    private void ExecuteFollowPlayer()
    {
        // TODO: _player is only set when _playerInRange is true; So if I forget to check _playerInRange, I could get a NullReferenceException
        if (_playerInRange)
        {
            _distanceToPlayer = Vector3.Distance(transform.position, _player.transform.position);
            _engagePlayer = _playerInRange && !_obstaclesBetweenPlayer &&
                            _distanceToPlayer > (targetDistanceToPlayer + _targetDistanceToPlayerOffset);
            if (_engagePlayer)
            {
                var localTransform = transform;
                var position = localTransform.position;
                var playerPosition = _player.transform.position;
                var deltaX = position.x - playerPosition.x;
                var direction = deltaX < 0 ? Vector2.right : Vector2.left;
                var velocity = direction * (enemyData.speed * Time.fixedDeltaTime);
                localTransform.Translate(velocity);
            }
        }
    }

    public void StartFollowingPlayer(GameObject player)
    {
        _player = player;
        _playerInRange = true;
        _targetDistanceToPlayerOffset = Random.Range(0.4f, 1.5f);
    }

    public void StopFollowingPlayer()
    {
        _playerInRange = false;
    }

    #endregion

    #region Attack

    [SerializeField] private float attackCooldown;

    private void HandleAttack()
    {
        if (_playerInRange && !_obstaclesBetweenPlayer && attackCooldown == 0)
        {
            attackCooldown = enemyData.attackCooldown;
            // TODO: Invoke Attack Event;
            ExecuteAttack();
        }
    }

    private void ExecuteAttack()
    {
        // enemyStats.attackDamage
        // _player.GetComponent<PlayerManager>().TakeDamage(enemyData.attackDamage);
    }

    private void HandleAttackCooldown()
    {
        if (attackCooldown > 0)
        {
            attackCooldown -= Time.fixedDeltaTime;
        }
        else
        {
            attackCooldown = 0;
        }
    }

    #endregion

    #region Unity Editor

#if UNITY_EDITOR
    private void OnValidate()
    {
        if (enemyData == null)
            Debug.LogWarning("Please assign a Enemy Stats asset to the Enemy Controller's Enemy Stats slot", this);
    }
#endif

    #endregion
}