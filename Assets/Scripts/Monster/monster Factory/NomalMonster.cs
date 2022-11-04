using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Photon.Pun;
using Photon.Realtime;

class NomalMonster : Monster, IMonsterBase
{
    Action mon_action;
    public NomalMonster()
    {
        type = UnitType.ENomalMonster;
        if (!is_dead)
        {
            //스테이터스
            max_hp = 50f;
            damage = 1f;
            defense = 0.5f;
            chase_speed = 15f;
            //범위
            chase_dist = 100f;
            attack_dist = 10f;
            //쿨타임
            wait_time = 0f;
            chase_cool_time = 2f;
            attack_cool_time = 3f;
            //상태
            current_state = CurrentState.EIDLE;
        }
    }
    private void Awake()
    {
        //if (!PhotonNetwork.IsMasterClient)
        //{
        //    return;
        //}
        //else
        //{
        //    mon_action += Currnet_State;
        //    mon_action += Is_Dead;
        //}
        mon_action += Currnet_State;
        mon_action += Is_Dead;
    }

    private void FixedUpdate()
    {
        //if (!PhotonNetwork.IsMasterClient)
        //{
        //    return;
        //}
        //else
        //{
        //    mon_action();
        //}
        mon_action();
    }

    public void Currnet_State()
    {
        if (!is_dead)
        {
            switch (current_state)
            {
                case CurrentState.EIDLE:
                    Idle();
                    break;
                case CurrentState.ECHASE:
                    Chase();
                    break;
                case CurrentState.EATTACK:
                    Attack();
                    break;
            }
        }
    }

    public void Idle()
    {
        //노말 몬스터에서는 find player로 사용
        if(!is_dead)
        {
            targets = Physics.OverlapSphere(transform.position, chase_dist, target_mask);

            for(int i = 0; i < targets.Length; i++)
            {
                player = targets[i].GetComponent<PhotonTestPlayer>();
                if(player != null)
                {
                    lock_target = player.gameObject;
                    current_state = CurrentState.ECHASE;
                    break;
                }
            }
        }
    }

    public void Patrol()
    {
        //none
    }

    public void Chase()
    {
        if (lock_target != null)
        {
            currnet_state_cool += Time.deltaTime;
            lock_target_pos = lock_target.transform.position;
            float dist = (lock_target_pos - transform.position).magnitude;
            //공격 볌위
            if (dist <= attack_dist)
            {
                current_state = CurrentState.EATTACK;
            }

            if (dist <= chase_dist)
            {
                nav.speed = chase_speed;
                nav.stoppingDistance = (attack_dist-1f);
                nav.SetDestination(lock_target_pos);
            }
        }
    }

    public void Attack()
    {
        if (lock_target != null)
        {
            lock_target_pos = lock_target.transform.position;
            current_time += Time.deltaTime;
            float distance = (lock_target.transform.position - transform.position).magnitude;
            //transform.LookAt(lock_target_pos);
            transform.rotation.SetLookRotation(lock_target_pos);
            
            if (distance <= attack_dist)
            {
                if (current_time >= attack_cool_time)
                {
                    nav.stoppingDistance = (attack_dist - 1f);
                    player.current_hp -= damage;
                    current_time = 0f;
                }
                else
                {
                    current_state = CurrentState.ECHASE;
                }
            }
            else
            {
                current_state = CurrentState.ECHASE;
            }
        }
    }

    public void Skill()
    {
        //none
    }
    public void Is_Dead()
    {
        if (is_dead)
        {
            Instantiate(Particles[0], transform.position, Quaternion.identity);
            Destroy(gameObject, 0.2f);
            current_state = CurrentState.EIDLE;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chase_dist);
    }
}
