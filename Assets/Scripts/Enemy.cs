using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private SpriteRenderer renderer;
    [SerializeField] private Animator animator;
    private int maxHealth = 100;
    private int currentHealth;

    // Properties for attack point of this gameobject
    [SerializeField] private GameObject attackPoint;
    [SerializeField] private LayerMask layers;
    [SerializeField] private float radius;

    // Transform of the object to be followed
    [SerializeField] private Transform toFollow;

    // Movement Speed
    [SerializeField] private float movementSpeed = 3f;

    // LOCAL SCALE AT X
    private float localScaleX;

    private RaycastHit2D hit;
    public float direction = 5f;
    [SerializeField] private Transform lineOfSight;

    private void Start()
    {
        currentHealth = maxHealth;
        animator.SetBool("isWalking", true);
        localScaleX = transform.localScale.x;
    }

    private void Update()
    {
        Collider2D[] hit = Physics2D.OverlapCircleAll(attackPoint.transform.position, radius, layers);

        foreach (Collider2D collider in hit)
        {
            if (collider.gameObject.CompareTag("Player"))
            {
                animator.SetBool("isWalking", false);
                animator.SetBool("isRunning", false);
                animator.SetBool("isAttacking", true);
                return;
            }
        }

        animator.SetBool("isWalking", true);
        animator.SetBool("isAttacking", false);
        Move();
    }


    public void DecreaseHealth(int amount)
    {
        currentHealth = currentHealth - amount;
        animator.SetBool("isAttacking", false);
        animator.SetTrigger("Hit");

        if (currentHealth <= 0)
        {
            print("Zombie Died");
            animator.SetBool("isDying", true);
            animator.SetBool("isWalking", false);
            Invoke("DestroyObject", 1f);
        }
    }


    void LineOfSight()
    {

        //(Vector2 origin, Vector2 direction, float distance, int layerMask)
        hit = Physics2D.Raycast(lineOfSight.position, new Vector2(direction, 0), 3f);

        if (hit)
        {
            print("It is cast");
            animator.SetBool("isWalking", false);
            animator.SetBool("isRunning", true);
        }
        else
        {
            print("NOT CASTED");
            animator.SetBool("isWalking", true);
            animator.SetBool("isRunning", false);
        }

    }

    void Move()
    {
        Vector3 currentPosition = transform.position;

        if (toFollow.position.x < currentPosition.x)
        {
            currentPosition.x += movementSpeed * Time.deltaTime * -1;
            direction = -5f;
            FlipAttackPoint(true);
            FlipSprite(true);
            LineOfSight();
        }
        else
        {
            currentPosition.x += movementSpeed * Time.deltaTime;
            direction = 5f;
            FlipAttackPoint(false);
            FlipSprite(false);
            LineOfSight();
        }
        transform.position = currentPosition;
    }


    void DestroyObject()
    {
        Destroy(gameObject);
    }


    /** Flips the attack point of this enemy. */
    void FlipAttackPoint(bool isFlip)
    {
        Vector3 currentAttackPointPos = attackPoint.transform.localPosition;

        if (isFlip)
        {
            currentAttackPointPos.x = 0.118f;
            return;
        }
        currentAttackPointPos.x = -0.118f;
        attackPoint.transform.localPosition = currentAttackPointPos;
    }


    /** Flips the Sprite of this game object. */
    void FlipSprite(bool isFlip)
    {
        Vector3 currentScale = transform.localScale;

        if (isFlip)
            currentScale.x = -localScaleX;
        else
            currentScale.x = localScaleX;
        
        transform.localScale = currentScale;
    }


    private void OnDrawGizmosSelected()
    {
        if (attackPoint != null)
        {
            Gizmos.DrawWireSphere(attackPoint.transform.position, radius);
        }
        Gizmos.DrawRay(lineOfSight.position, new Vector3(direction, 0, 0));
    }
}
