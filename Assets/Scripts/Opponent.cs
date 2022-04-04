using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Opponent : MonoBehaviour
{
    // REQUIRED FIELD OF AN OPPONENT
    [Header("Opponent Required Fields")]
    [SerializeField] protected SpriteRenderer renderer;
    [SerializeField] protected Animator animator;
    [SerializeField] protected GameObject lineOfSight;
    [SerializeField] protected LayerMask layers;
    [SerializeField] protected Transform toFollow;
    [SerializeField] protected int maxHealth;
    [SerializeField] protected float movementSpeed = 3f;
    [SerializeField] protected float lengthOfRay;
    
    protected int currentHealth;
    protected float localScaleX;

    /* LINE OF SIGHT */
    [Header("Line of Sight Properties")]
    protected RaycastHit2D sight;
    protected float xPositionOfRay;
    protected float distanceOfRay;

    // ANIMATION STATES
    protected const string IS_WALKING = "WALK";
    protected const string IS_ATTACKING = "ATTACK";
    protected const string IS_RUNNING = "RUN";
    protected const string HIT = "HIT";
    protected const string DIE = "DIE";

    [Header("Item")]
    [SerializeField] protected Items item;

    public Transform ToFollow
    {
        get
        {
            return this.toFollow;
        }
        set
        {
            this.toFollow = value;
        }
    }

    public virtual void Move()
    {
        Vector3 currentPosition = transform.position;

        if (toFollow.position.x < currentPosition.x)
        {
            FlipSprite(true);
            FlipLineOfSight(true);
        }
        else
        {
            FlipSprite(false);
            FlipLineOfSight(false);
        }
        transform.position = Vector2.MoveTowards(transform.position, new Vector2(toFollow.position.x, transform.position.y), movementSpeed * Time.deltaTime);
    }


    public virtual void TakeDamage(int damage)
    {
        this.currentHealth -= damage;

        animator.SetTrigger(HIT);

        if (this.currentHealth <= 0)
        {
            gameObject.GetComponent<Collider2D>().enabled = false;
            gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
            animator.SetBool(DIE, true);
            UpdateScore();
            Invoke("DestroyThisObject", 1f);
        }
    }


    void DestroyThisObject()
    {
        Items powerup = Instantiate(item);
        powerup.value = Random.Range(1, 11);
        powerup.transform.position = gameObject.transform.position;
        
        Destroy(gameObject);
    }


    void UpdateScore()
    {
        GameManager.instance.noOfKills += 1;
    }


    public void FlipSprite(bool isFlip)
    {
        Vector3 currentScale = transform.localScale;

        if (isFlip)
            currentScale.x = -localScaleX;
        else
            currentScale.x = localScaleX;

        transform.localScale = currentScale;
    }


    public void FlipLineOfSight(bool isFlip)
    {
        Vector3 currentPos = lineOfSight.transform.localPosition;

        if (isFlip)
        {
            currentPos.x = -xPositionOfRay;
            lengthOfRay = -12f;
        }
        else
        {
            currentPos.x = xPositionOfRay;
            lengthOfRay = 12f;
        }
        lineOfSight.transform.localPosition = currentPos;
    }


    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(lineOfSight.transform.position, new Vector3(lengthOfRay, 0, 0));
    }


    // EACH ENEMY HAS DIFFERENT ATTACKS
    public abstract void Attack();
}
