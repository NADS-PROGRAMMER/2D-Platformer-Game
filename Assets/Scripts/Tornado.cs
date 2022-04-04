using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tornado : MonoBehaviour
{
    [Header("Attack Properties")]
    public GameObject attackPoint;
    public LayerMask layers;
    public float radius;

    [Header("Game Object Properties")]
    public SpriteRenderer renderer;
    public Animator animator;
    public float speed;
    public int attackDamage;

    private void Start()
    {
        /** Invoke after 1 seconds so that
         * the animation for building up the tornado
         have time to execute before going to another state. */
        Invoke("Continue", 1f);
    }

    private void FixedUpdate()
    {
        Move();
        Attack();
    }

    public void Move()
    {
        Vector3 currentPos = transform.position;
        currentPos.x += speed * Time.deltaTime;
        transform.position = currentPos;
    }

    void Attack()
    {
        // Get overlapping collider of enemies to the gameObject we provide.
        Collider2D[] enemies = Physics2D.OverlapCircleAll(attackPoint.transform.position, this.radius, this.layers);

        foreach (Collider2D enemy in enemies)
        {
            enemy.gameObject.GetComponent<Opponent>().TakeDamage(100);
        }
    }

    void Continue()
    {
        animator.SetBool("isContinue", true);
        Invoke("End", 6f);
    }


    void End()
    {
        animator.SetBool("isEnd", true);
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPoint.transform.position, radius);
    }
}
