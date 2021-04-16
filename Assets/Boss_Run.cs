using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Boss_Run : StateMachineBehaviour
{
    public BossScript boss;
    Transform player;
    public NavMeshAgent agent;
    public float attackRange = 20f;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        boss = animator.GetComponent<BossScript>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = animator.GetComponent<NavMeshAgent>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent.SetDestination(player.position);
        float distance = Vector3.Distance(animator.transform.position, player.position);
        if (distance <= attackRange)
        {
            agent.isStopped = true;
            animator.SetTrigger("Attack");
        }
        agent.isStopped = false;
        if (boss.currentHealth <= 0)
            agent.isStopped = true;
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Attack");
    }
}
