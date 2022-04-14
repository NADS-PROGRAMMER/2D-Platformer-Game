using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WATER_START : StateMachineBehaviour
{
    public GameObject attackPoint;
    public float radius;
    public LayerMask layers;
    WaterBall waterBall;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("WATER START");
        waterBall = animator.GetComponent<WaterBall>();
        attackPoint = animator.GetComponent<WaterBall>().attackPoint;
        radius = animator.GetComponent<WaterBall>().radius;
        layers = animator.GetComponent<WaterBall>().layers;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        Collider2D[] enemies = Physics2D.OverlapCircleAll(attackPoint.transform.position, radius, layers);

        if (enemies.Length > 0)
        {
            animator.SetBool("END", true);
        }
        else
        {
            waterBall.Move();
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
