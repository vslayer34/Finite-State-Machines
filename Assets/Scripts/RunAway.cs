using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RunAway : State
{
    public RunAway(GameObject npc, NavMeshAgent agent, Animator animator, Transform player) : base(npc, agent, animator, player)
    {
        name = STATE.RUNAWAY;
    }

    public override void Enter()
    {
        animator.SetTrigger("isRunning");
        agent.isStopped = false;
        agent.speed = 6.0f;
        agent.SetDestination(GameEnvironment.Singleton.SafeZone.position);
        base.Enter();
    }

    public override void Update()
    {
        if (agent.remainingDistance < 1)
        {
            nextState = new Idle(npc, agent,animator, player);
            stage = EVENT.EXIT;
        }
    }

    public override void Exit()
    {
        animator.ResetTrigger("isRunning");
        base.Exit();
    }
}
