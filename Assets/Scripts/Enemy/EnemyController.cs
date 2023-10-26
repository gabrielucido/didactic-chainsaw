using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private EnemyStats scriptableStats;

    private void FixedUpdate()
    {
        HandleDeathCondition();
    }

    private void HandleDeathCondition()
    {
        if (scriptableStats.health_points <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void TakeDamage(int damage)
    {
        scriptableStats.health_points -= damage;
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        if (scriptableStats == null)
            Debug.LogWarning("Please assign a ScriptableStats asset to the Player Controller's Stats slot", this);
    }
#endif
}