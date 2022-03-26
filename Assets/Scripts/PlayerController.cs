using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D body;
    [SerializeField] private Animator animator;
    [SerializeField] private float speed = 4f;
    [SerializeField] private float jumpForce = 5f;
    private bool isGrounded = true;
    [SerializeField] private SpriteRenderer renderer;
    private const string RUNNING = "Running";

    // FOR ATTACKING
    [SerializeField] private GameObject attackPoint;
    [SerializeField] private LayerMask layers;
    [SerializeField] private float attackRange = 5f;

    void Update()
    {
        MovePlayer();
    }

    private void FixedUpdate()
    {
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

        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetTrigger("Attack");

            Collider2D[] hit = Physics2D.OverlapCircleAll(attackPoint.transform.position, attackRange, layers);

            foreach (Collider2D collider in  hit) {

                collider.gameObject.GetComponent<Opponent>().TakeDamage(20);
            }
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            float currentAttackRange = 3f;

            animator.SetTrigger("Skill_1");

            Collider2D[] hit = Physics2D.OverlapCircleAll(attackPoint.transform.position, currentAttackRange, layers);

            foreach (Collider2D collider in hit)
            {
                collider.gameObject.GetComponent<Opponent>().TakeDamage(30);
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
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
    private void OnDrawGizmosSelected()
    {
        if (attackPoint != null)
            Gizmos.DrawWireSphere(attackPoint.transform.position, attackRange);
    }
}
