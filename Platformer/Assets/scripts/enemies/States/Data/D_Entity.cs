using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newEntityData", menuName ="Data/Entity Data/Base Data")]
public class D_Entity : ScriptableObject
{
    public float WallCheckDistance = .2f;
    public float LedgeCheckDistance = .4f;

    public LayerMask
        WhatIsWall,
        WhatIsGround,
        WhatIsPlayer;
}
