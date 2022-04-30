using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBall : MonoBehaviour
{
    [SerializeField] public float speed = 6f;
    [SerializeField] public GameObject attackPoint;
    [SerializeField] public Animator animator;
    [SerializeField] public float radius;
    [SerializeField] public LayerMask layers;
    [SerializeField] public int damage;
    public bool isFlipped = false;

    // Start is called before the first frame update
    void Start()
    {
        Vector2 player = GameObject.FindGameObjectWithTag("Player").transform.GetChild(0).position;

        if (isFlipped)
            gameObject.transform.position = new Vector2(player.x - 1.5f, player.y);
        else
            gameObject.transform.position = new Vector2(player.x + 1.5f, player.y);
        gameObject.GetComponent<SpriteRenderer>().flipX = isFlipped;

        Invoke("DestroyThisObj", 1f);
    }


    public void Move()
    {
        Vector2 currentPos = transform.position;

        if (isFlipped)
            currentPos.x -= speed * Time.deltaTime;
        else
            currentPos.x += speed * Time.deltaTime;

        transform.position = currentPos;
    }


    public void Attack()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(attackPoint.transform.position, radius, layers);

        foreach (Collider2D enemy in enemies)
        {
            if (enemy.gameObject.CompareTag("Boss"))
            {
                enemy.gameObject.GetComponent<Boss>().TakeDamage(20);
            }
            else
                enemy.gameObject.GetComponent<Opponent>().TakeDamage(20);
        }

        Destroy(gameObject);

        Debug.Log("DAMAGED");
    }


    public void Continue()
    {
        animator.SetBool("CONTINUE", true);
    }


    public void DestroyThisObj()
    {
        Destroy(gameObject);
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPoint.transform.position, radius);
    }
}
