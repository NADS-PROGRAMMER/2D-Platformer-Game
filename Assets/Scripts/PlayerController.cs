using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [Header("Required Fields")]
    [SerializeField] private int maxHealth;
    [SerializeField] private Rigidbody2D body;
    [SerializeField] private Animator animator;
    [SerializeField] private float speed = 4f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private SpriteRenderer renderer;
    
    [Header("Attack Properties")]
    [SerializeField] private GameObject attackPoint;
    [SerializeField] private LayerMask layers;
    [SerializeField] private float attackRange = 5f;
    [SerializeField] private HealthBar healthBar;

    private float currentAttackRate = 0f;
    private const string RUNNING = "Running";
    private int currentHealth;
    private bool isGrounded = true;

    private void Start()
    {
        this.currentHealth = this.maxHealth;
    }

    void Update()
    {
        MovePlayer();
        Attack();
        Jump();
    }

    void MovePlayer()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");

        Vector3 current = transform.position;
        current.x += horizontal * speed * Time.deltaTime;
        transform.position = current;

        AnimatePlayer(horizontal);
    }

    void AnimatePlayer(float horizontal)
    {
        if (horizontal > 0)
        {
            FlipAttackPoint(true);

            animator.SetBool(RUNNING, true);
            renderer.flipX = false;
        }
        else if (horizontal < 0)
        {
            FlipAttackPoint(false);

            animator.SetBool(RUNNING, true);
            renderer.flipX = true;
        }
        else
        {
            animator.SetBool(RUNNING, false);
        }
    }

    // Attack Function
    void Attack()
    {
        if (Time.time >= currentAttackRate)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                animator.SetTrigger("Attack");

                Collider2D[] hit = Physics2D.OverlapCircleAll(attackPoint.transform.position, attackRange, layers);

                foreach (Collider2D collider in hit)
                {
                    collider.gameObject.GetComponent<Opponent>().TakeDamage(20);
                }
                currentAttackRate = Time.time + 1;
            }

            if (Input.GetKey(KeyCode.Q))
            {
                float currentAttackRange = 3f;

                animator.SetTrigger("Skill_1");

                Collider2D[] hit = Physics2D.OverlapCircleAll(attackPoint.transform.position, currentAttackRange, layers);

                foreach (Collider2D collider in hit)
                {
                    collider.gameObject.GetComponent<Opponent>().TakeDamage(30);
                }
                currentAttackRate = Time.time + 1;
            }
        }
    }

    /** Jump Function */
    void Jump()
    {

        if (Input.GetKey(KeyCode.C) && isGrounded)
        {
            animator.SetTrigger("isJumping");
            body.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            isGrounded = false;
        }
    }

    /** Decrease the health of this game object */
    public void TakeDamage(int damage)
    {
        this.currentHealth -= damage;

        this.healthBar.SetCurrentHealth(this.currentHealth);
    }

    void FlipAttackPoint(bool isFlip)
    {
        Vector3 currentAttackPointPosition = attackPoint.transform.localPosition;

        if (isFlip)
        {
            currentAttackPointPosition.x = 0.125f;
        }
        else
        {
            currentAttackPointPosition.x = -0.125f;
        }
        attackPoint.transform.localPosition = currentAttackPointPosition;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint != null)
            Gizmos.DrawWireSphere(attackPoint.transform.position, attackRange);
    }
}
