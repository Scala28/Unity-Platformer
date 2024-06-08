using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newAttackData", menuName = "Data/State Data/Attack State")]
public class D_EnemyAttackState : ScriptableObject
{
    public float AttackDistance = 3f;
    public float AttackSpeed = 7f;
    public float AttackDuration = .3f;
    public float TimeBtwAttack = 1f;
    public float AttackDelay = .2f;
}
