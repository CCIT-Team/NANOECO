using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NsocutMomster : NewMonster
{
    System.Action mon_action;

    #region 초기값
    public NsocutMomster()
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
                case CURRNET_STATE.ESkill:
                    Skill();
                    break;
            }
        }
    }

    public override void Skill()
    {
        //플레이어를 바라보며 자리에 멈추어 스킬 사용
        agent.SetDestination(transform.position);
        //몬스터 생성

    }
}
