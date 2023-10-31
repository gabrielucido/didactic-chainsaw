using UnityEngine;

/// <summary>
/// Base class for all player controllers 
/// </summary>
/// <remarks>
/// This automatically sets a reference to the main PlayerManager script to be used within a controller.
///  For example to access the player's data scriptable object, you can use PlayerController.playerData
/// </remarks>
[RequireComponent(typeof(EnemyManager))]
public abstract class EnemyBase : MonoBehaviour
{
    protected EnemyManager Enemy;

    protected virtual void Awake()
    {
        Enemy = GetComponentInParent<EnemyManager>();
    }
}