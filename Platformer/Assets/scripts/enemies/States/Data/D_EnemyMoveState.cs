using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newMoveStateData", menuName = "Data/State Data/Move State")]
public class D_EnemyMoveState : ScriptableObject
{
    [Header("Move state")]
    public float movementSpeed = 7f;

}
