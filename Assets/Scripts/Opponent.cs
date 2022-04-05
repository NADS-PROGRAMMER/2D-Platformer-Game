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
    [SerializeField] protected Transform toFollow;
    [SerializeField] protected LayerMask layers;
    [SerializeField] protected int maxHealth;
    [SerializeField] protected float movementSpeed = 3f;
    [SerializeField] protected float lengthOfRay;
    
    /* LINE OF SIGHT */
    [Header("Line of Sight Properties")]
    protected RaycastHit2D sight;
    protected float xPositionOfRay;
    protected float distanceOfRay;

    // Class Item (Aggregation)
    [Header("Item")]
    [SerializeField] protected Items item;

    protected int currentHealth;
    protected float localScaleX;

    // ANIMATION STATES
    protected const string IS_WALKING = "WALK";
    protected const string IS_ATTACKING = "ATTACK";
    protected const string IS_RUNNING = "RUN";
    protected const string HIT = "HIT";
    protected const string DIE = "DIE";


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


    /** Destroy the gameObject that is attached to this component. */
    void DestroyThisObject()
    {
        Items powerup = Instantiate(item);

        // Random value of heart powerups.
        powerup.value = Random.Range(1, 11);

        // Set the position of powerup to the position of the gameobject of this script.
        powerup.transform.position = gameObject.transform.position;

        Destroy(gameObject);
    }


    void UpdateScore()
    {
        GameManager.instance.noOfKills += 1;
    }


    public virtual void Move()
    {
        Vector3 currentPosition = transform.position;

        /** Check if the object to follow is on the left side. */
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

        /** MoveTowards to the specified location
            The first argument is the starting position, second is the 
            destination point, and the max speed of movement. */
        transform.position = Vector2.MoveTowards(transform.position, new Vector2(toFollow.position.x, transform.position.y), movementSpeed * Time.deltaTime);
    }


    /* A function that handles damaging this gameObject. 
        @param damage : int
        - the value of attack damage.
     */
    public virtual void TakeDamage(int damage)
    {
        this.currentHealth -= damage;

        animator.SetTrigger(HIT); // Trigger the Hit animation.

        if (this.currentHealth <= 0)
        {
            /* Disable the collider and set the rigidbody to kinematic
             so that the gameobject doesn't react to the possible next
            attack of the enemy when it is actually dead. */
            gameObject.GetComponent<Collider2D>().enabled = false;
            gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
            animator.SetBool(DIE, true);
            UpdateScore();
            // Invoke the "DestroyThisObject" function after 1s so that it has time to finish the "Die" animation.
            Invoke("DestroyThisObject", 1f); 
        }
    }


    /** Flipping the sprite.
    @NOTE: I used the localScale because if 
    the flipX property of SpriteRenderer is used
    the collider is not consistent. 
    
     @isFlip : bool
     - If isFlip is true then we flip to left, otherwise, we flip to right. */
    public void FlipSprite(bool isFlip)
    {
        Vector3 currentScale = transform.localScale;

        if (isFlip)
            currentScale.x = -localScaleX;
        else
            currentScale.x = localScaleX;

        transform.localScale = currentScale;
    }


    /** A function that is responsible for 
     flipping the line of sight. */
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


    // EACH ENEMY HAS DIFFERENT ATTACKS, SO WE SET IT AS ABSTRACT.
    public abstract void Attack();
}
