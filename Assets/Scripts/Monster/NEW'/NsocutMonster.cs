using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NsocutMonster : NewMonster
{
    System.Action mon_action;

    #region 초기값
    public NsocutMonster()
    {
        data.max_hp = 50f;
        data.current_hp = data.max_hp;
        data.damage = 0f;
        data.defense = 1f;
        data.patrol_speed = 10f;
        data.chase_speed = 15f;

        data.patrol_dist = 20f;
        data.chase_dist = 30f;
        data.attack_dist = 3f;
        data.skill_dist = 0f;
        data.event_chase_dist = 1000f;

        data.idle_cool_time = 2f;
        data.chase_cool_time = 2f;
        data.attack_cool_time = 2f;
        data.skill_cool_time = 20f;

        data.current_time = 0f;
        data.state_time = 0f;
    }

    public override void Init()
    {
        data.max_hp = 50f;
        data.current_hp = data.max_hp;
        data.damage = 0f;
        data.defense = 1f;
        data.patrol_speed = 10f;
        data.chase_speed = 15f;

        data.patrol_dist = 20f;
        data.chase_dist = 30f;
        data.attack_dist = 3f;
        data.skill_dist = 0f;
        data.event_chase_dist = 1000f;

        data.idle_cool_time = 2f;
        data.chase_cool_time = 2f;
        data.attack_cool_time = 2f;
        data.skill_cool_time = 20f;

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

    public override void Skill()
    {
        audioplayer.PlayOneShot(skill_clip);
        animator.SetTrigger(hash_skill);
        float dist = (lock_target.transform.position - transform.position).magnitude;
        transform.LookAt(lock_target.transform);
        if (dist <= data.skill_dist)
        {
            agent.SetDestination(transform.position);
            //몬스터 생성 메서드 필요
        }
        else
        {
            current_state = CURRNET_STATE.EChase;
        }

    }
}
