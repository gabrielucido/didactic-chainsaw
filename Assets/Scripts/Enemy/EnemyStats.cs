using UnityEngine;

[CreateAssetMenu]
public class EnemyStats : ScriptableObject
{
    [Header("General Stats")]
    [Tooltip(
        "Makes all Input snap to an integer. Prevents gamepads from walking slowly. Recommended value is true to ensure gamepad/keybaord parity.")]
    public int health_points = 10;
}