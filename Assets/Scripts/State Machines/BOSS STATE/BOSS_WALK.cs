using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BOSS_WALK : StateMachineBehaviour
{
    public GameObject player;
    public GameObject attackPoint;

    public Rigidbody2D rb;
    public Boss bossScript;

    [Header("Line of Sight")]
    public GameObject lineOfSight;
    public float lengthOfRay;
    public float distanceOfRay;
    public LayerMask layers;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = animator.GetComponent<Rigidbody2D>();
        attackPoint = animator.gameObject.transform.GetChild(0).gameObject;
        bossScript = animator.GetComponent<Boss>();
        lineOfSight = animator.gameObject.transform.GetChild(2).gameObject;
        lengthOfRay = animator.GetComponent<Boss>().lengthOfRay;
        distanceOfRay = Math.Abs(animator.GetComponent<Boss>().lengthOfRay);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        try
        {
            bossScript.LookAtPlayer();

            lengthOfRay = animator.GetComponent<Boss>().lengthOfRay;

            Vector2 toFollow = Vector2.MoveTowards(rb.position, new Vector2(player.transform.position.x, rb.position.y), bossScript.speed * Time.fixedDeltaTime);

            rb.MovePosition(toFollow);

            RaycastHit2D isSighted = Physics2D.Raycast(lineOfSight.transform.position, new Vector2(lengthOfRay, 0), distanceOfRay, layers);

            if (isSighted)
            {
                animator.SetBool("RUN", true);
            }
        }
        catch(Exception e)
        {
            Debug.Log("TRYING TO ACCESS DESTROYED OBJECT");
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
