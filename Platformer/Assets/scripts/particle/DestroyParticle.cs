using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyParticle : MonoBehaviour
{
    [SerializeField]
    private float durationTime;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, durationTime);
    }

}
