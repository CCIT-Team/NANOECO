using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NnomalFMonster : NewMonster
{
    System.Action mon_action;
    [SerializeField]
    private GameObject mon_bullet;

    #region �ʱⰪ
    public NnomalFMonster()
    {
        data.max_hp = 50f;
        data.current_hp = data.max_hp;
        data.damage = 0f;
        data.defense = 1f;
        data.patrol_speed = 5f;
        data.chase_speed = 8f;

        data.patrol_dist = 20f;
        data.chase_dist = 30f;
        data.attack_dist = 5f;
        data.skill_dist = 0f;

        data.idle_cool_time = 2f;
        data.chase_cool_time = 2f;
        data.attack_cool_time = 2f;
        data.skill_cool_time = 1000f;

        data.current_time = 0f;
        data.state_time = 0f;
    }

    public override void Init()
    {
        data.max_hp = 50f;
        data.current_hp = data.max_hp;
        data.damage = 0f;
        data.defense = 1f;
        data.patrol_speed = 5f;
        data.chase_speed = 8f;

        data.patrol_dist = 20f;
        data.chase_dist = 30f;
        data.attack_dist = 5f;
        data.skill_dist = 0f;

        data.idle_cool_time = 2f;
        data.chase_cool_time = 2f;
        data.attack_cool_time = 2f;
        data.skill_cool_time = 1000f;

        data.current_time = 0f;
        data.state_time = 0f;
    }
    #endregion
    private void Awake()
    {
        mon_action += Monster_State;
    }

    private void FixedUpdate()
    {
        mon_action();
    }

    private void Monster_State()
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
                case CURRNET_STATE.EChase:
                    Chase();
                    break;
                case CURRNET_STATE.EAttack:
                    Attack();
                    break;
            }
        }
    }

    public override void Attack()
    {
        if (lock_target != null)
        {
            data.current_time += Time.deltaTime;
            float dist = (lock_target.transform.position - transform.position).magnitude;
            transform.rotation = Quaternion.Lerp(transform.rotation, lock_target.transform.rotation, Time.deltaTime);
            if (dist <= data.attack_dist)
            {
                if (data.current_time >= data.attack_cool_time)
                {
                    agent.stoppingDistance = (data.attack_dist - 1f);
                    Instantiate(mon_bullet, transform);
                    data.current_time = 0;
                }
                else
                {
                    current_state = CURRNET_STATE.EChase;
                }
            }
            else
            {
                current_state = CURRNET_STATE.EChase;
            }
        }
        else
        {
            current_state = CURRNET_STATE.EIdle;
        }
    }
}
