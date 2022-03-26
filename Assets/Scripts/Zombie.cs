using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : Opponent
{
    [SerializeField] private GameObject attackPoint;
    [SerializeField] private float attackRadius;

    private void Start()
    {
        localScaleX = transform.localScale.x;
        xPositionOfRay = lineOfSight.transform.localPosition.x;
        currentHealth = maxHealth;
        distanceOfRay = lengthOfRay;
    }

    private void FixedUpdate()
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
                animator.SetBool(IS_ATTACKING, true);
                return;
            }
        }

        print("NOT ATTACKING");
        animator.SetBool(IS_ATTACKING, false);

        base.Move();
    }


    
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPoint.transform.position, attackRadius);
    }
}
