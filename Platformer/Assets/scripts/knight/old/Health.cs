using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public int health;
    public int numOfHearth;

    public Image[] hearth;
    public Sprite fullHearth;
    public Sprite emptyHearth;

    public bool canTakeDamage { get; private set; }

    public float damageCoolDown;
    private float lastDamageTime;

    private void Start()
    {
        health = numOfHearth;
        canTakeDamage = true;
    }
    private void Update()
    {
        for(int i = 0; i < hearth.Length; i++)
        {
            if(i<health)
            {
                hearth[i].sprite = fullHearth;
            }
            else
            {
                hearth[i].sprite = emptyHearth;
            }
            if(i<numOfHearth)
            {
                hearth[i].enabled = true;
            }
            else
            {
                hearth[i].enabled = false;
            }
        }

        if(health <= 0)
        {
            Destroy(gameObject);
        }
        if (GetComponent<characterController>().isDashing == true)
        {
            canTakeDamage = false;
        }
        else if (GetComponent<characterController>().isDashing == false)
        {
            canTakeDamage = true;
        }

    }


    public void TakeDamage(int damage, int direction)
    {
        if(Time.time > lastDamageTime + damageCoolDown && canTakeDamage)
        {
            lastDamageTime = Time.time;
            health -= damage;

            GetComponent<characterController>().KnockBack(direction);
        }
        
    }



}
