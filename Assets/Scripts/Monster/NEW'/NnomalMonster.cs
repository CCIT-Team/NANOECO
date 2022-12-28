using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NnomalMonster : NewMonster
{
    System.Action mon_action;
    #region 초기값

    public NnomalMonster()
    {
        data.max_hp = 100f;
        data.current_hp = data.max_hp;
        data.damage = 5f;
        data.defense = 1f;
        data.patrol_speed = 6f;
        data.chase_speed = 10f;

        data.patrol_dist = 20f;
        data.chase_dist = 25f;
        data.attack_dist = 2f; //몬스터가 크다면 공격 범위도 커야 할 듯
        data.skill_dist = 0f;

        data.idle_cool_time = 3f;
        data.chase_cool_time = 2f;
        data.attack_cool_time = 1f;
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
        mon_action += Monster_State;
        mon_action += Hp_Check;
        mon_action += Hit_Mon;
    }

    private void FixedUpdate()
    {
        mon_action();
    }

    private void Update()
    {
        Debug.Log(data.current_hp);

        if(Input.GetKeyDown(KeyCode.L))
        {
            hit_true = true;
            data.current_hp -= 5f;
        }
    }
}
