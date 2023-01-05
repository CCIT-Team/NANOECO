using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NdestructMonster : NewMonster
{
    System.Action mon_action;
    #region �ʱⰪ

    public NdestructMonster()
    {
        data.max_hp = 30f;
        data.current_hp = data.max_hp;
        data.damage = 20f;
        data.defense = 1f;
        data.patrol_speed = 10f;
        data.chase_speed = 20f;

        data.patrol_dist = 20f;
        data.chase_dist = 30f;
        data.attack_dist = 0f;
        data.skill_dist = 20f;

        data.idle_cool_time = 2f;
        data.chase_cool_time = 2f;
        data.attack_cool_time = 1f;
        data.skill_cool_time = 5f;

        data.current_time = 0f;
        data.state_time = 0f;
    }

    public override void Init()
    {
        data.max_hp = 30f;
        data.current_hp = data.max_hp;
        data.damage = 20f;
        data.defense = 1f;
        data.patrol_speed = 10f;
        data.chase_speed = 20f;

        data.patrol_dist = 20f;
        data.chase_dist = 30f;
        data.attack_dist = 0f;
        data.skill_dist = 20f;

        data.idle_cool_time = 2f;
        data.chase_cool_time = 2f;
        data.attack_cool_time = 1f;
        data.skill_cool_time = 5f;

        data.current_time = 0f;
        data.state_time = 0f;
        on_event = false;
    }
    #endregion
    private void Awake()
    {
        mon_action += Monster_State;
        mon_action += Hp_Check;
        mon_action += Hit_Mon;
    }

    private void FixedUpdate()
    {
        mon_action();
    }

    public override void Monster_State()
    {
        if (!is_dead)
        {
            switch (current_state)
            {
                case CURRNET_STATE.EIdle:
                    Idle();
                    break;
                case CURRNET_STATE.EPatrol:
                    Patrol();
                    break;
                case CURRNET_STATE.ESkill:
                    Skill();
                    break;
            }
        }
    }

    public override void Patrol()
    {
        agent.speed = data.patrol_speed;
        if (lock_target == null)
        {
            Collider[] targets = Physics.OverlapSphere(transform.position, data.patrol_dist, target_mask);
            for (int i = 0; i < targets.Length; i++)
            {
                player = targets[i].GetComponent<Player>();
                if (player != null)
                {
                    lock_target = player.gameObject;
                    current_state = CURRNET_STATE.ESkill;
                    break;
                }
                if (player == null)
                    return;
            }
        }
    }

    public override void Skill()
    {
        if (lock_target != null)
        {
            agent.speed = data.chase_speed;
            agent.SetDestination(lock_target.transform.position);
            float dist = (transform.position - lock_target.transform.position).magnitude;
            if (dist >= data.skill_dist)
            {
                Collider[] collider = Physics.OverlapSphere(transform.position, 20f, target_mask);
                for (int i = 0; i < collider.Length; i++)
                {
                    player = collider[i].GetComponent<NaNoPlayer>();
                    if (player != null)
                    {
                        player.current_hp -= data.damage;
                    }
                    if (player == null)
                        return;
                }
            }
        }
    }
}
