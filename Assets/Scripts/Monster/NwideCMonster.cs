using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NwideCMonster : NewMonster
{
    System.Action mon_action;
    #region �ʱⰪ

    public NwideCMonster()
    {
        data.max_hp = 160f;
        data.current_hp = data.max_hp;
        data.damage = 5f;
        data.defense = 1f;
        data.patrol_speed = 6f;
        data.chase_speed = 10f;

        data.patrol_dist = 23f;
        data.chase_dist = 25f;
        data.attack_dist = 3f;
        data.skill_dist = 0f;
        data.event_chase_dist = 1000f;

        data.idle_cool_time = 2f;
        data.chase_cool_time = 2f;
        data.attack_cool_time = 4f;
        data.skill_cool_time = 100f;

        data.current_time = 0f;
        data.state_time = 0f;
    }

    public override void Init()
    {
        data.max_hp = 100f;
        data.current_hp = data.max_hp;
        data.damage = 5f;
        data.defense = 1f;
        data.patrol_speed = 6f;
        data.chase_speed = 10f;

        data.patrol_dist = 23f;
        data.chase_dist = 25f;
        data.attack_dist = 3f;
        data.skill_dist = 0f;
        data.event_chase_dist = 1000f;

        data.idle_cool_time = 2f;
        data.chase_cool_time = 2f;
        data.attack_cool_time = 4f;
        data.skill_cool_time = 100f;

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
}
