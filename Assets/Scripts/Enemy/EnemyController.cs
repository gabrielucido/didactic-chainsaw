using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class EnemyController : MonoBehaviour
{
    [SerializeField] private EnemyData enemyData;

    private void Start()
    {
        // healthPoints = enemyData.maxHealthPMaxHealthPoints;
    }

    private void FixedUpdate()
    {
        // HandleDeathCondition();
        // HandleFollowPlayer();
        // ExecuteFollowPlayer();
        // HandleAttack();
        // HandleAttackCooldown();
    }
    
    #region Attack
    
    [SerializeField] private float attackCooldown;
    
    private void HandleAttack()
    {
        // if (_playerInRange && !_obstaclesBetweenPlayer && attackCooldown == 0)
        // {
        //     attackCooldown = enemyData.attackCooldown;
        //     // TODO: Invoke Attack Event;
        //     ExecuteAttack();
        // }
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