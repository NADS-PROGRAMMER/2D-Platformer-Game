using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public float speed;
    public Animator animator;
    public Transform center;

    [Header("Attack Point Properties")]
    public GameObject attackPoint;
    public float radius;

    [Header("Look")]
    public Transform player;

    [Header("Health")]
    public int maxHealth;
    private int currentHealth;

    [Header("Line of Sight")]
    public GameObject lineOfSight;
    public float lengthOfRay;
    public float currentDirection;
    private float localScale;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        currentDirection = lengthOfRay;
        localScale = gameObject.transform.localScale.x;
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        animator.SetTrigger("HIT");

        if (currentHealth <= 0)
        {
            gameObject.GetComponent<Collider2D>().enabled = false;
            gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
            animator.SetBool("DIE", true);
            Invoke("DestroyThisObject", .8f);
        }
    }

    public void Attack()
    {
        Collider2D[] hitObjects = Physics2D.OverlapCircleAll(attackPoint.transform.position, radius, LayerMask.GetMask("Player"));

        foreach (Collider2D collider in hitObjects)
        {
            if (collider.gameObject.CompareTag("Player"))
            {
                collider.gameObject.GetComponent<PlayerController>().TakeDamage(0);
            }
        }
    }

    public void LookAtPlayer()
    {
        Vector3 currentScale = transform.localScale;

        if (player.transform.position.x < center.position.x)
        {
            // 4.378948f
            currentScale.x = -this.localScale;
            float currentFace = currentDirection;
            lengthOfRay = -currentFace;
        }
        else
        {
            currentScale.x = this.localScale;
            float currentFace = currentDirection;
            lengthOfRay = +currentFace;
        }
        transform.localScale = currentScale;
    }


    private void DestroyThisObject()
    {
        Destroy(gameObject);
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPoint.transform.position, radius);
        Gizmos.DrawRay(lineOfSight.transform.position, new Vector2(lengthOfRay, 0));
    }
}
