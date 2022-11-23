using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NnomalMonster : NewMonster
{
    System.Action mon_action;
    #region �ʱⰪ
    public override void Init()
    {
        data.max_hp = 100f;
        data.current_hp = data.max_hp;
        data.damage = 5f;
        data.defense = 1f;
        data.patrol_speed = 6f;
        data.chase_speed = 10f;

        data.patrol_dist = 20f;
        data.chase_dist = 25f;
        data.attack_dist = 2f;
        data.skill_dist = 0f;

        data.idle_cool_time = 2f;
        data.chase_cool_time = 2f;
        data.attack_cool_time = 1f;
        data.skill_cool_time = 100f;

        data.current_time = 0f;
        data.state_time = 0f;
    }
    #endregion
    private void Awake()
    {
        mon_action += Init;
        mon_action += Monster_State;
        mon_action -= Init;
    }

    private void FixedUpdate()
    {
        mon_action();
    }

    private void Monster_State()
    {
        if(!is_dead)
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



}
