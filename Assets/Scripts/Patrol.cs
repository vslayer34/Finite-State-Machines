using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Patrol : State
{
    int currentIndex;
    public Patrol(GameObject npc, NavMeshAgent agent, Animator animator, Transform player) : base(npc, agent, animator, player)
    {
        name = STATE.PATROL;
        agent.speed = 2.0f;
        agent.isStopped = false;
    }

    public override void Enter()
    {
        currentIndex = 0;
        animator.SetTrigger("isWalking");
        base.Enter();
    }

    public override void Update()
    {
        if (agent.remainingDistance < 1)
        {
            if (currentIndex >= GameEnvironment.Singleton.CheckPoints.Count - 1)
            {
                currentIndex = 0;
            }
            else
            {
                currentIndex++;
            }
        }
        agent.SetDestination(GameEnvironment.Singleton.CheckPoints[currentIndex].transform.position);
    }

    public override void Exit()
    {
        animator.ResetTrigger("isWalking");
        base.Exit();
    }
}
