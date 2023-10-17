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

    [Header("Check variables")]
    public float GroundCheckRadius = .4f;
    public LayerMask WhatIsGround;
}
