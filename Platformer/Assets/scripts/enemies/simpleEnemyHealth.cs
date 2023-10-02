using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class simpleEnemyHealth : MonoBehaviour
{
    public int maxHealth;
    public int currentHealth;

    private bool canTakeDamage;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        canTakeDamage = true;
    }
    private void Update()
    {
        if (currentHealth <= 0)
        {
            Die();
        }

        if(GetComponent<simpleEnemy>().isAttacking == true)
        {
            canTakeDamage = false;
        }
        else
        {
            canTakeDamage = true;
        }
    }
    public void TakeDamage(int damage)
    {
        if (canTakeDamage)
        {
            currentHealth -= damage;
            //play hurt animation

        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
