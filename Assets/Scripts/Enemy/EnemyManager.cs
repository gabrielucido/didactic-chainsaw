using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public EnemyData data;
    
    #region Validation

#if UNITY_EDITOR
    private void OnValidate()
    {
        if (data == null)
        {
            Debug.LogError("Please assign a Enemy Data asset to the Enemy Manager data slot", this);
        }
    }
#endif

    #endregion
}