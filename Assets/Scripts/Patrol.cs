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
        // set the first waypoint to the closest one
        float lastDistance = Mathf.Infinity;
        for (int i = 0; i < GameEnvironment.Singleton.CheckPoints.Count; i++)
        {
            GameObject thisWaypoint = GameEnvironment.Singleton.CheckPoints[i]; 
            float distance = Vector3.Distance(npc.transform.position, thisWaypoint.transform.position);
            if (distance < lastDistance)
            {
                // making in consdiration the currentIndex++; in the update
                currentIndex = i - 1;
                lastDistance = distance;
            }
        }

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
            agent.SetDestination(GameEnvironment.Singleton.CheckPoints[currentIndex].transform.position);
        }

        if (CanSeePlayer())
        {
            nextState = new Pursue(npc, agent, animator, player);
            stage = EVENT.EXIT;
        }

        if (IsPlayerBehind())
        {
            nextState = new RunAway(npc, agent, animator, player);
            stage = EVENT.EXIT;
        }
    }

    public override void Exit()
    {
        animator.ResetTrigger("isWalking");
        base.Exit();
    }
}
