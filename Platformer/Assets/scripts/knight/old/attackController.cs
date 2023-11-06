using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attackController : MonoBehaviour
{
    public int damage;

    public Transform attackPoint;
    public float attackRange;
    public LayerMask enemyLayer;


    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Attack();
        }
    }

    void Attack()
    {
        Debug.Log("attack");
        DetectEnemies();
    }
    void DetectEnemies()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);

        foreach(Collider2D enemy in hitEnemies)
        {
            //enemy.GetComponent<enemyHealth>().TakeDamage(damage);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

}
