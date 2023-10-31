using System;
using UnityEngine;


/// <summary>
///   Handles the player's gravity tweaks like early jumping, max fall speed.
///   Also differs the gravity from ground to when player is in mid air give a more sensitiveness experience
/// </summary>
[RequireComponent(typeof(PlayerPhysicsController))]
public class PlayerGravityController : PlayerBase
{
    private CapsuleCollider2D _col;
    private bool _grounded;

    private void Start()
    {
        _col = GetComponent<CapsuleCollider2D>();
    }

    void FixedUpdate()
    {
        HandleGravity();
    }

    private void HandleGravity()
    {
        // if (Player._grounded && Player._frameVelocity.y <= 0f)
        // {
        //     Player._frameVelocity.y = Player.data.groundingForce;
        // }
        // else
        // {
        //     var inAirGravity = Player.data.fallAcceleration;
        //     if (Player._endedJumpEarly && Player._frameVelocity.y > 0)
        //         inAirGravity *= Player.data.jumpEndEarlyGravityModifier;
        //     Player._frameVelocity.y =
        //         Mathf.MoveTowards(Player._frameVelocity.y, -Player.data.maxFallSpeed,
        //             inAirGravity * Time.fixedDeltaTime);
        // }
    }
}