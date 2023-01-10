using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class NnomalFMonster : NewMonster
{
    System.Action mon_action;
    [SerializeField]
    private GameObject mon_bullet;
    [SerializeField]
    private GameObject shot_pos;

    #region �ʱⰪ
    public NnomalFMonster()
    {
        data.max_hp = 10f;
        data.current_hp = data.max_hp;
        data.damage = 0f;
        data.defense = 1f;
        data.patrol_speed = 1f;
        data.chase_speed = 2f;

        data.patrol_dist = 5f;
        data.chase_dist = 7f;
        data.attack_dist = 4f;
        data.skill_dist = 0f;
        data.event_chase_dist = 20f;

        data.idle_cool_time = 0.5f;
        data.chase_cool_time = 2f;
        data.attack_cool_time = 2f;
        data.skill_cool_time = 1000f;

        data.current_time = 0f;
        data.state_time = 0f;
    }

    public override void Init()
    {
        data.max_hp = 10f;
        data.current_hp = data.max_hp;
        data.damage = 0f;
        data.defense = 1f;
        data.patrol_speed = 2f;
        data.chase_speed = 4f;

        data.patrol_dist = 4f;
        data.chase_dist = 5f;
        data.attack_dist = 3f;
        data.skill_dist = 0f;
        data.event_chase_dist = 20f;

        data.idle_cool_time = 0.5f;
        data.chase_cool_time = 2f;
        data.attack_cool_time = 2f;
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

    public override void Attack()
    {
        if (lock_target != null)
        {
            data.current_time += Time.deltaTime;
            float dist = (lock_target.transform.position - transform.position).magnitude;
            transform.LookAt(lock_target.transform);
            if (dist <= data.attack_dist)
            {
                if (data.current_time >= data.attack_cool_time)
                {
                    audioplayer.PlayOneShot(attack_clip);
                    animator.SetTrigger(hash_attack);
                    agent.stoppingDistance = (data.attack_dist - 0.5f);
                    Instantiate(mon_bullet, transform.position, transform.rotation);
                    data.current_time = 0;
                }
                else { current_state = CURRNET_STATE.EChase; }
            }
            else { current_state = CURRNET_STATE.EChase; }
        }
        else
        {
            current_state = CURRNET_STATE.EIdle;
            animator.SetBool(hash_chase, false);
        }
    }
}
