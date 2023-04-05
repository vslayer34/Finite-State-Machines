using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Attack : State
{
    float rotationSpeed = 2.0f;
    AudioSource shootAudio;
    public Attack(GameObject npc, NavMeshAgent agent, Animator animator, Transform player) : base(npc, agent, animator, player)
    {
        name = STATE.ATTACK;
        shootAudio = npc.GetComponent<AudioSource>();
    }

    public override void Enter()
    {
        animator.SetTrigger("isShooting");
        agent.isStopped = true;
        shootAudio.Play();
        base.Enter();
    }

    public override void Update()
    {
        Vector3 direction = player.position - npc.transform.position;
        float angle = Vector3.Angle(npc.transform.forward, direction);
        //direction.z = 0.0f;

        npc.transform.rotation = Quaternion.Slerp(npc.transform.rotation,
                                                Quaternion.LookRotation(direction),
                                                rotationSpeed * Time.deltaTime);

        if (!CanShootPlayer())
        {
            nextState = new Idle(npc, agent, animator, player);
            stage = EVENT.EXIT;
        }
    }

    public override void Exit()
    {
        animator.ResetTrigger("isShooting");
        shootAudio.Stop();
    }
}
