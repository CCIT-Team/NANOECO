using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NnomalFMonster : NewMonster
{
    System.Action mon_action;
    [SerializeField]
    private GameObject mon_bullet;

    #region 초기값
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
        data.attack_dist = 15f;
        data.skill_dist = 0f;

        data.idle_cool_time = 3f;
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
        data.attack_dist = 15f;
        data.skill_dist = 0f;

        data.idle_cool_time = 3f;
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
                    audioplayer.PlayOneShot(attack_clip);
                    animator.SetTrigger(hash_attack);
                    agent.stoppingDistance = (data.attack_dist - 5f);
                    Instantiate(mon_bullet, transform);  //아마 몬스터 총알도 변경해야 될듯 포톤 연동할때
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
            animator.SetBool(hash_chase, false);
        }
    }
}
