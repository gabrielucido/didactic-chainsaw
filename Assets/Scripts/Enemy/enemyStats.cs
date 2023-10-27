using UnityEngine;

[CreateAssetMenu]
public class EnemyStats : ScriptableObject
{
    [Header("General Stats")]
    [Tooltip(
        "Makes all Input snap to an integer. Prevents gamepads from walking slowly. Recommended value is true to ensure gamepad/keybaord parity.")]
    public int max_health_points = 100;

    [Tooltip(
         "The movement speed of the Enemy."), Range(0.01f, 100)]
    public float speed = 5;
}