using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperReload : StateMachineBehaviour
{
    public Sniper gun;
    public GameObject player;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player");
        gun = animator.GetComponent<Sniper>();
    }

    //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        gun.isReloading = false;
        player.GetComponent<PlayerController>().isReloading = false;
    }
}
