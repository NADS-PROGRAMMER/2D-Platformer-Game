using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [Header("Required Fields")]
    [SerializeField] private Rigidbody2D body;
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer renderer;
    [SerializeField] private int maxHealth;
    [SerializeField] private float speed = 4f;
    [SerializeField] private float jumpForce = 5f;

    
    [Header("Attack Properties")]
    [SerializeField] private GameObject attackPoint;
    [SerializeField] private LayerMask layers;
    [SerializeField] private float attackRange = 5f;

    // Class (Composition)
    [SerializeField] private HealthBar healthBar;

    private float currentAttackRate = 0f;
    private int currentHealth;
    private bool isGrounded = true;

    // Animation States
    private const string DIE = "DIE";
    private const string RUNNING = "Running";


    private void Start()
    {
        this.currentHealth = this.maxHealth;
        this.healthBar.SetCurrentHealth(this.currentHealth);
    }


    void Update()
    {
        MovePlayer();
        Attack();
        Jump();
    }


    /** A function that moves the Player. */
    void MovePlayer()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");

        Vector3 current = transform.position;
        current.x += horizontal * speed * Time.deltaTime;
        transform.position = current;

        AnimatePlayer(horizontal);
    }


    /** Animates the Player. */
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
                    if (collider.gameObject.CompareTag("Boss"))
                        collider.gameObject.GetComponent<Boss>().TakeDamage(20);
                    else
                        collider.gameObject.GetComponent<Opponent>().TakeDamage(20);
                }
                currentAttackRate = Time.time + 1;
            }
        }
    }


    /** Jump Function */
    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.C) && isGrounded)
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

        if (this.currentHealth <= 0)
        {
            gameObject.GetComponent<CapsuleCollider2D>().isTrigger = true;
            gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
            animator.SetBool(DIE, true);
            GameManager.instance.GameOver();
            Invoke("DestroyThisObject", .5f);
        }
    }


    /** Flips the attack point of this game object */
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

        if (collision.gameObject.CompareTag("Heart"))
        {
            this.currentHealth += collision.gameObject.GetComponent<Items>().value;
            this.healthBar.SetCurrentHealth(this.currentHealth);
            Destroy(collision.gameObject);
        }
    }


    private void OnDrawGizmosSelected()
    {
        if (attackPoint != null)
            Gizmos.DrawWireSphere(attackPoint.transform.position, attackRange);
    }


    public void DestroyThisObject()
    {
        Destroy(gameObject);
    }
}
