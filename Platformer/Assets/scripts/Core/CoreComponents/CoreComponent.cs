using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreComponent : MonoBehaviour
{
    protected Core core;

    protected virtual void Awake()
    {
        if(!transform.parent.TryGetComponent<Core>(out core))
        {
            Debug.LogError("No Core found in parent!"); 
        }

    }
}
