using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class State
{
    public enum STATE
    {
        IDLE, PATROL, PERSUIT, ATTACK, SLEEP, RUNAWAY
    };

    public enum EVENT
    {
        ENTER, UPDATE, EXIT
    };

    public STATE name;
    protected EVENT stage;

    protected GameObject npc;
    protected NavMeshAgent agent;
    protected Animator animator;
    protected Transform player;
    protected State nextState;

    float visualDistance = 10.0f;
    float visualAngle = 30.0f;
    float shootingDistance = 7.0f;


    public State(GameObject npc, NavMeshAgent agent, Animator animator, Transform player)
    {
        this.npc = npc;
        this.agent = agent;
        this.animator = animator;
        stage = EVENT.ENTER;
        this.player = player;
    }

    // skeleton methods for what to do
    public virtual void Enter() { stage = EVENT.UPDATE; }
    public virtual void Update() { stage = EVENT.UPDATE; }
    public virtual void Exit() { stage = EVENT.EXIT; }


    // adminstrative method that control the above methods
    public State Process()
    {
        if (stage == EVENT.ENTER) { Enter(); }
        if (stage == EVENT.UPDATE) {  Update(); }
        if (stage == EVENT.EXIT)
        {
            Exit();
            return nextState;
        }

        return this;
    }

    public bool CanSeePlayer()
    {
        Vector3 direction = player.position - npc.transform.position;
        float angle = Vector3.Angle(direction, npc.transform.position);

        if (angle <= visualAngle && direction.magnitude <= visualDistance)
        {
            return true;
        }

        return false;
    }

    public bool CanShootPlayer()
    {
        Vector3 direction = player.position - npc.transform.position;
        
        if (direction.magnitude <= shootingDistance)
        {
            return true;
        }

        return false;
    }


    public bool IsPlayerBehind()
    {
        Vector3 direction = npc.transform.position - player.position;
        float angle = Vector3.Angle(direction, npc.transform.position);

        if (direction.magnitude < 2.0f && angle < 30)
        {
            return true;
        }
        return false;
    }
}
