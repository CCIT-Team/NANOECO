using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class NtankerMonster : NewMonster
{
    System.Action mon_action;

    #region �ʱⰪ
    public NtankerMonster()
    {
        data.max_hp = 150f;
        data.current_hp = data.max_hp;
        data.damage = 10f;
        data.defense = 5f;
        data.patrol_speed = 2f;
        data.chase_speed = 2.5f;

        data.patrol_dist = 8f;
        data.chase_dist = 10f;
        data.attack_dist = 1.5f;
        data.skill_dist = 0f;
        data.event_chase_dist = 50f;

        data.idle_cool_time = 0.5f;
        data.chase_cool_time = 2f;
        data.attack_cool_time = 5f;
        data.skill_cool_time = 1000f;

        data.current_time = 0f;
        data.state_time = 0f;
    }

    public override void Init()
    {
        data.max_hp = 150f;
        data.current_hp = data.max_hp;
        data.damage = 10f;
        data.defense = 5f;
        data.patrol_speed = 4f;
        data.chase_speed = 5f;

        data.patrol_dist = 8f;
        data.chase_dist = 10f;
        data.attack_dist = 1.5f;
        data.skill_dist = 0f;
        data.event_chase_dist = 50f;

        data.idle_cool_time = 0.5f;
        data.chase_cool_time = 2f;
        data.attack_cool_time = 5f;
        data.skill_cool_time = 1000f;

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
