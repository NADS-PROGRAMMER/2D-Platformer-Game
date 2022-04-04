using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : Opponent
{
    [SerializeField] private GameObject attackPoint;
    [SerializeField] private float attackRadius;
    [SerializeField] private int attackDamage;

    private float cooldown = 3f;
    private float nextAttack = 1.8f;


    private void Start()
    {
        localScaleX = transform.localScale.x;
        xPositionOfRay = lineOfSight.transform.localPosition.x;
        currentHealth = maxHealth;
        distanceOfRay = lengthOfRay;
    }


    // MAKE IT SOLID
    private void Update()
    {
        Attack();
    }


    public override void Attack()
    {
        Collider2D[] hitObjects = Physics2D.OverlapCircleAll(attackPoint.transform.position, attackRadius, layers);

        RaycastHit2D isSighted = Physics2D.Raycast(lineOfSight.transform.position, new Vector2(lengthOfRay, 0), distanceOfRay, layers);

        if (isSighted)
        {
            animator.SetBool(IS_RUNNING, true);
            animator.SetBool(IS_WALKING, false);
        }
        else
        {
            animator.SetBool(IS_WALKING, true);
            animator.SetBool(IS_RUNNING, false);
        }

        foreach (Collider2D collider in hitObjects)
        {
            if (collider.gameObject.CompareTag("Player"))
            {
                AttackDelay(collider);
                return;
            }
        }

        animator.SetBool(IS_ATTACKING, false);
        //nextAttack = 0f;
        base.Move();
    }


    public void AttackDelay(Collider2D collider)
    {
        if (nextAttack > 0)
        {
            nextAttack -= Time.deltaTime;
            if (collider.gameObject.CompareTag("Player"))
            {
                animator.SetBool(IS_ATTACKING, true);
            }
        }
        else if (nextAttack <= 0)
        {
            nextAttack = cooldown;
            animator.SetBool(IS_ATTACKING, true);
            collider.GetComponent<PlayerController>().TakeDamage(attackDamage);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPoint.transform.position, attackRadius);
    }
}
