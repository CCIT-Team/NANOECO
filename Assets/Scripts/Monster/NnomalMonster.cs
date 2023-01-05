using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class NnomalMonster : NewMonster
{
    System.Action mon_action;
    #region 초기값
    [PunRPC]
    public NnomalMonster()
    {
        data.max_hp = 100f;
        data.current_hp = data.max_hp;
        data.damage = 5f;
        data.defense = 1f;
        data.patrol_speed = 8f;
        data.chase_speed = 13f;

        data.patrol_dist = 21f;
        data.chase_dist = 25f;
        data.attack_dist = 3f; //몬스터가 크다면 공격 범위도 커야 할 듯
        data.skill_dist = 0f;
        data.event_chase_dist = 150f;

        data.idle_cool_time = 0.5f;
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
        data.patrol_speed = 8f;
        data.chase_speed = 13f;

        data.patrol_dist = 21f;
        data.chase_dist = 25f;
        data.attack_dist = 3f;
        data.skill_dist = 0f;
        data.event_chase_dist = 150f;

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
        if (!PhotonNetwork.IsMasterClient)
        {
            mon_action += Monster_State;
            mon_action += Hp_Check;
            mon_action += Hit_Mon;
            mon_action += Another_Find_Player;
        }
        else
            return;
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
