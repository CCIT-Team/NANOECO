using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NsocutMonster : NewMonster
{
    System.Action mon_action;
    public List<GameObject> monster_group = new List<GameObject>();
    public List<Transform> spawn_point = new List<Transform>();
    public int wave_count;
    public float wave_time;

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
        data.skill_dist = 5f;
        data.event_chase_dist = 150f;

        data.idle_cool_time = 0.5f;
        data.chase_cool_time = 2f;
        data.attack_cool_time = 2f;
        data.skill_cool_time = 0f;

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
        data.skill_dist = 5f;
        data.event_chase_dist = 150f;

        data.idle_cool_time = 0.5f;
        data.chase_cool_time = 2f;
        data.attack_cool_time = 2f;
        data.skill_cool_time = 0f;

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

    public override void Skill()
    {
        audioplayer.PlayOneShot(skill_clip);
        animator.SetTrigger(hash_skill);
        float dist = (lock_target.transform.position - transform.position).magnitude;
        transform.LookAt(lock_target.transform);
        if (dist <= data.skill_dist)
        {
            agent.SetDestination(transform.position);
            Instantiate(Particles[3], transform.position, Quaternion.identity);
            //몬스터 생성 메서드 필요
            if(wave_count > 0)
            {
                StartCoroutine(Monster_Wave());
            }
            data.skill_cool_time = 100f;
        }
        else
        {
            current_state = CURRNET_STATE.EChase;
        }

    }

    IEnumerator Monster_Wave()
    {
        wave_count--;
        int i = Random.Range(0, monster_group.Count);
        Instantiate(monster_group[i], spawn_point[i].position, Quaternion.identity);
        yield return new WaitForSeconds(wave_time);
    }
}
