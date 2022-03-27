using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : Opponent
{
    [SerializeField] private GameObject attackPoint;
    [SerializeField] private float attackRadius;

    private float cooldown = 3f;
    private float nextAttack = 0f;

    private void Start()
    {
        localScaleX = transform.localScale.x;
        xPositionOfRay = lineOfSight.transform.localPosition.x;
        currentHealth = maxHealth;
        distanceOfRay = lengthOfRay;
    }

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
        nextAttack = 0f;
        base.Move();
    }

    public void AttackDelay(Collider2D collider)
    {
        if (nextAttack > 0)
        {
            nextAttack -= Time.deltaTime;
        }
        else if (nextAttack <= 0)
        {
            nextAttack = cooldown;
            animator.SetBool(IS_ATTACKING, true);
            collider.GetComponent<PlayerController>().TakeDamage(20);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPoint.transform.position, attackRadius);
    }
}
