using UnityEngine;

public class FollowPlayerController : MonoBehaviour
{
    [SerializeField] private PlayerData playerData;

    private Vector3 _cameraPosition;

    private void Update()
    {
        HandleCameraPosition();
        ExecuteCameraMovement();
    }

    private void HandleCameraPosition()
    {
        _cameraPosition = new Vector3(playerData.playerPosition.x, playerData.playerPosition.y, transform.position.z);
    }

    private void ExecuteCameraMovement()
    {
        transform.position = _cameraPosition;
    }

    #region Validation

#if UNITY_EDITOR
    private void OnValidate()
    {
        if (playerData == null)
        {
            Debug.LogError("Please assign a Player Data asset to the Follow Player Controller Player Data slot", this);
            enabled = false;
        }
    }
#endif

    #endregion
}