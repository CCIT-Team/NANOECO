using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class NnomalMonster : NewMonster
{
    System.Action mon_action;
    #region 초기값
    public NnomalMonster()
    {
        data.max_hp = 60f;
        data.current_hp = data.max_hp;
        data.damage = 5f;
        data.defense = 1f;
        data.patrol_speed = 2f;
        data.chase_speed = 3f;

        data.patrol_dist = 5f;
        data.chase_dist = 7f;
        data.attack_dist = 0.5f; //몬스터가 크다면 공격 범위도 커야 할 듯
        data.skill_dist = 0.5f;
        data.event_chase_dist = 50f;

        data.idle_cool_time = 0.5f;
        data.chase_cool_time = 2f;
        data.attack_cool_time = 1f;
        data.skill_cool_time = 100f;

        data.current_time = 0f;
        data.state_time = 0f;
    }

    public override void Init()
    {
        data.max_hp = 60f;
        data.current_hp = data.max_hp;
        data.damage = 5f;
        data.defense = 1f;
        data.patrol_speed = 4f;
        data.chase_speed = 6f;

        data.patrol_dist = 5f;
        data.chase_dist = 7f;
        data.attack_dist = 0.5f; //몬스터가 크다면 공격 범위도 커야 할 듯
        data.skill_dist = 0.5f;
        data.event_chase_dist = 50f;

        data.idle_cool_time = 0.5f;
        data.chase_cool_time = 2f;
        data.attack_cool_time = 1f;
        data.skill_cool_time = 100f;

        data.current_time = 0f;
        data.state_time = 0f;
        on_event = false;
        protection_target = false;
    }
    #endregion
    private void Awake()
    {
            mon_action += Monster_State;
            mon_action += Hp_Check;
            mon_action += Hit_Mon;
            mon_action += Another_Find_Player;
    }

    private void FixedUpdate()
    {
            mon_action();
    }
}
