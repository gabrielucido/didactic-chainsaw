using UnityEngine;

public class FollowPlayerController : MonoBehaviour
{
    [SerializeField] private PlayerData playerData;

    private Vector3 _cameraPosition;

    private void Awake()
    {
        if (playerData == null)
        {
            // In this case the all component "FollowPlayerController" will be disabled because the only purpose of this
            // component is to follow the player. If there is no player SO to follow, then there is no reason to keep this enabled
            // This behaviour only make sense because there nothing else here to be executed
            // this Awake objective is only to make the soPlayer reference to the external module completely optional although it
            // only makes sense with the player data, if this has another function it should still be working and no need to define the
            // SOPlayer reference
            enabled = false;
        }
    }

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
}