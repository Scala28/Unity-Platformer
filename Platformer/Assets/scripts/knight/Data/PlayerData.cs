using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="newPlayerData", menuName ="Data/Player Data/Base Data")]
public class PlayerData : ScriptableObject
{
    [Header("Move state")]
    public float MovementSpeed = 17.0f;

    [Header("Jump state")]
    public float JumpForce = 14.0f;
    public float JumpHoldTime = .2f;
    public int AmountOfJumps = 1;

    [Header("In Air state")]
    public float InAirMovementControl = .3f;

    [Header("Wall slide state")]
    public float WallSlideSpeed = .5f;

    [Header("Wall jump state")]
    public float WallJumpForce = 10.0f;
    public Vector2 WallJumpAngle = new Vector2(3f, 3f);
    public float WallJumpTime = .2f;

    [Header("Dash state")]
    public float DashCoolDown = 1.5f;
    public float DashSpeed = 50f;
    public float DashTime = .15f;

    [Header("Check variables")]
    public float GroundCheckRadius = .4f;
    public LayerMask WhatIsGround;

    public float WallCheckRadius = .4f;
    public LayerMask WhatIsWall;
}
