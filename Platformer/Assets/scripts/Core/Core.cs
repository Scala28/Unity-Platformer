using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Core : MonoBehaviour
{
    public Movement Movement { get; private set; }

    private void Awake()
    {
        Movement = GetComponentInChildren<Movement>();
        if (Movement == null)
            Debug.LogError("Missing Core component!");
    }
}
