using UnityEngine;

[CreateAssetMenu]
public class EnemyData : ScriptableObject
{
    [Header("General Stats")]
    [Tooltip(
        "Makes all Input snap to an integer. Prevents gamepads from walking slowly. Recommended value is true to ensure gamepad/keybaord parity.")]
    public int max_health_points = 100;

    [Tooltip(
         "The movement speed of the Enemy."), Range(0.01f, 20)]
    public float speed = 5;
    
    [Tooltip(
         "The damage dealt by the Enemy in each attack."), Range(1, 100)]
    public int attackDamage = 5;
    
    [Tooltip(
         "The Cooldown in seconds between each each attack."), Range(0.01f, 10)]
    public float attackCooldown = 1;
}