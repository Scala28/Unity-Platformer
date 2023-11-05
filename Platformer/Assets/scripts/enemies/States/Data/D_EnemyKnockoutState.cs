using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newKnockoutData", menuName = "Data/State Data/Knockout State")]
public class D_EnemyKnockoutState : ScriptableObject
{
    public Vector2 KnockoutSpeed = new Vector2(15f, 15f);
    public float KnockoutDuration = .15f;
}
