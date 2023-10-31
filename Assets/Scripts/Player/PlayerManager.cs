using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public PlayerData data;

    private void Update()
    {
        data.playerPosition = transform.position;
    }

    #region Validation

#if UNITY_EDITOR
    private void OnValidate()
    {
        if (data == null)
        {
            Debug.LogError("Please assign a Player Data asset to the Player Manager data slot", this);
        }
    }
#endif

    #endregion
}