using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BOSS_ATTACK : StateMachineBehaviour
{
    GameObject attackPoint;
    Boss bossScript;
    public LayerMask layers;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("ATTACKING");
        attackPoint = animator.gameObject.transform.GetChild(0).gameObject;
        bossScript = animator.GetComponent<Boss>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(attackPoint.transform.position, bossScript.radius, layers);

        if (enemies.Length < 1)
        {
            animator.SetBool("RUN", false);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
