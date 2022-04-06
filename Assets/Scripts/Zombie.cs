using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : Opponent
{
    [Header("Melee Enemy Properties")]
    [SerializeField] private GameObject attackPoint;
    [SerializeField] private float attackRadius;
    [SerializeField] private int attackDamage;
    private float cooldown = 3f;
    private float nextAttack = 1.8f;


    private void Start()
    {
        // Set the initial value to the x localScale of this gameObject.
        localScaleX = transform.localScale.x;
        
        // Set the initial value to the x position of lineOfSight gameObject.
        xPositionOfRay = lineOfSight.transform.localPosition.x;

        // This script is the actual script to be attached so that it still needs initialize 
        currentHealth = maxHealth;

        // Set the value of lengthOfRay
        distanceOfRay = lengthOfRay;
    }


    private void Update()
    {
        Attack();
    }


    /** An Attack function */
    public override void Attack()
    {
        // Array of gameObjects that is overlapped to the attackRange of this gameObject.
        Collider2D[] hitObjects = Physics2D.OverlapCircleAll(attackPoint.transform.position, attackRadius, layers);

        RaycastHit2D isSighted = Physics2D.Raycast(lineOfSight.transform.position, new Vector2(lengthOfRay, 0), distanceOfRay, layers);

        // If isSighted is true, then execute the RUN animation.
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
        base.Move();
    }


    /** The delay of the attack. */
    public void AttackDelay(Collider2D collider)
    {
        if (nextAttack > 0)
        {
            nextAttack -= Time.deltaTime;

            /** Since we cannot damage the player if the next attack is not ready, we still need to
             check if the collider is equal to Player so that we execute the attacking animation. */
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
