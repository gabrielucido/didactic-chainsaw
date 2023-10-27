using System;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class EnemyController : MonoBehaviour
{
    [SerializeField] private EnemyStats enemyStats;
    [SerializeField] private float healthPoints;


    private void Start()
    {
        healthPoints = enemyStats.max_health_points;
    }

    private void FixedUpdate()
    {
        HandleDeathCondition();
        HandleFollowPlayer();
        ExecuteFollowPlayer();
    }

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

    #region Follow Player

    [SerializeField] private float _targetDistanceToPlayer = 3;

    private float _distanceToPlayer = 0;
    private float _distanceToPlayerOffset;

    private GameObject _player;
    private bool _playerInRange;
    private bool _followPlayer;

    // private void OnCollisionEnter2D(Collision2D other)
    // { 
    //     Debug.Log("HIT!!!");
    // }

    private void HandleFollowPlayer()
    {
        // Player already in "distance" range but we still need to check if there is obstacles 
        if (_playerInRange)
        {
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
                if (hit.collider.CompareTag("Player"))
                {
                    _distanceToPlayer = Vector3.Distance(transform.position, _player.transform.position);
                    if (_distanceToPlayer > _targetDistanceToPlayer)
                    {
                        _followPlayer = true;
                    }
                    else
                    {
                        _followPlayer = false;
                    }
                }
                else
                {
                    _followPlayer = false;
                }
            }
        }
        else
        {
            _followPlayer = false;
        }
    }

    private void ExecuteFollowPlayer()
    {
        if (_followPlayer)
        {
            var localTransform = transform;
            var position = localTransform.position;
            var playerPosition = _player.transform.position;
            var deltaX = position.x - playerPosition.x;
            var direction = deltaX < 0 ? Vector2.right : Vector2.left;
            var velocity = direction * (enemyStats.speed * Time.fixedDeltaTime);
            localTransform.Translate(velocity);
        }
    }

    public void StartFollowingPlayer(GameObject player)
    {
        // var _playerController = player.GetComponent<PlayerController>();
        _player = player;
        _playerInRange = true;
    }

    public void StopFollowingPlayer()
    {
        // var _playerController = player.GetComponent<PlayerController>();
        _player = null;
        _playerInRange = false;
    }

    #endregion

#if UNITY_EDITOR
    private void OnValidate()
    {
        if (enemyStats == null)
            Debug.LogWarning("Please assign a Enemy Stats asset to the Enemy Controller's Enemy Stats slot", this);
    }
#endif
}