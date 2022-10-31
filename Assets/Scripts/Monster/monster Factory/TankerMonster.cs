using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Photon.Pun;
using Photon.Realtime;

class TankerMonster : Monster
{
    public TankerMonster()
    {
        type = UnitType.ETankerMonster;
        if (!is_dead)
        {
            //스테이터스
            max_hp = 50f;
            damage = 1f;
            defense = 0.5f;
            patrol_speed = 15f;
            chase_speed = 0;
            //범위
            move_range = 0;
            patrol_dist = 0;
            chase_dist = 0;
            attack_dist = 0;
            skill_dist = 0;
            //쿨타임
            wait_time = 0f;
            idle_cool_time = 0;
            chase_cool_time = 0;
            attack_cool_time = 0;
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

    }

    public void Attack()
    {

    }

    public void Skill()
    {

    }
}
