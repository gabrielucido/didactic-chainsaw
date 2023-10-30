using System;
using UnityEngine;

[CreateAssetMenu]
public class PlayerEvents : ScriptableObject
{
    // the main goal of this class is to be a container for the events that the player can trigger
    // this class is a ScriptableObject because it will be used by the PlayerController and the PlayerAnimationController
    // if we can make a integration test of events in Player/Camera for example with a Camera shake we only need to define
    // the same PlayerEvents.asset to the CameraController and Player and it will work

    // [Header("Movement Events")]
    // [Tooltip(
    //     "Lorem Ipsum")]
    // public float moveSpeed = 5f;
    
    [Tooltip(
        "Jump Event")]
    public event Action Jumped;
    
    [Tooltip(
        "Attack event")]
    public event Action Attacked;
}