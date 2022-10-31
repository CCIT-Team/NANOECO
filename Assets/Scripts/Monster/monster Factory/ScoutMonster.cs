using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Photon.Pun;
using Photon.Realtime;

class ScoutMonster : Monster, IMonsterBase
{
    public ScoutMonster()
    {
        type = UnitType.EScoutMonster;
        if (!is_dead)
        {
            //스테이터스
            max_hp = 50f;
            damage = 1f;
            defense = 0.5f;
            patrol_speed = 0f;
            chase_speed = 10f;
            //범위
            move_range = 0;
            patrol_dist = 0;
            chase_dist = 100f;
            attack_dist = 10f;
            skill_dist = 0;
            //쿨타임
            wait_time = 0f;
            idle_cool_time = 0;
            chase_cool_time = 2f;
            attack_cool_time = 3f;
            skill_cool_time = 0;
            //상태
            is_dead = false;
            current_state = CurrentState.EIDLE;
        }
    }
    private void Awake()
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            return;
        }
        else
        {
            mon_action += Currnet_State;
            mon_action += Is_Dead;
        }
    }
    private void FixedUpdate()
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            return;
        }
        else
        {
            mon_action();
        }
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
                case CurrentState.EPATROL:
                    Patrol();
                    break;
                case CurrentState.ECHASE:
                    Chase();
                    break;
                case CurrentState.EATTACK:
                    Attack();
                    break;
                case CurrentState.ESKILL:
                    Skill();
                    break;
            }
        }
    }
    public void Idle()
    {
        
    }

    public void Patrol()
    {
        
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
                nav.stoppingDistance = (attack_dist - 1f);
                nav.SetDestination(lock_target_pos);
            }

            if (dist > chase_dist)
            {
                if (currnet_state_cool >= chase_cool_time)
                {

                    lock_target = null;
                    current_state = CurrentState.EIDLE;
                    currnet_state_cool = 0f;
                }
            }
        }
    }

    public void Attack()
    {
        
    }

    public void Skill()
    {
        
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
}
