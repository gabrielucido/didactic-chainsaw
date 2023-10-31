using UnityEngine;

[CreateAssetMenu]
public abstract class CharacterData : ScriptableObject
{
    [Header("Attack")]
    [Tooltip(
         "Makes all Input snap to an integer. Prevents gamepads from walking slowly. Recommended value is true to ensure gamepad/keybaord parity."),
     Range(.1f, 60)]
    public float attackCooldown = 1;
    
    [Tooltip(
        "Damage dealt by attack.")]
    public int attackDamage = 40;
}