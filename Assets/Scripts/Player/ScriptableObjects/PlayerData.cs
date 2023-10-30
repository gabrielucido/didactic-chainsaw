using UnityEngine;

[CreateAssetMenu]
public class PlayerData : ScriptableObject
{
    [Header("General")]
    [Tooltip(
        "Lorem Ipsum")]
    public Vector3 playerPosition = Vector3.zero;
}