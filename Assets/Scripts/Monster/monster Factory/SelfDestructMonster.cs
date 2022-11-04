using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Photon.Pun;
using Photon.Realtime;

class SelfDestructMonster : Monster, IMonsterBase
{
    Action mon_action;
    public SelfDestructMonster()
    {
        type = UnitType.ESelfDestructMonster;
        if (!is_dead)
        {
            //�������ͽ�
            max_hp = 50f;
            damage = 1f;
            defense = 0.5f;
            patrol_speed = 15f;
            chase_speed = 30f;
            //����
            move_range = 30f;
            patrol_dist = 25f;
            skill_dist = 10f;
            //��Ÿ��
            wait_time = 2f;
            idle_cool_time = 2f;
            //����
            is_dead = false;
            current_state = CurrentState.EIDLE;
        }
    }
    private void Awake()
    {
        //if (!PhotonNetwork.IsMasterClient) ���� �׽�Ʈ ��
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
        //if (!PhotonNetwork.IsMasterClient) ���� �׽�Ʈ ��
        //{
        //    return;
        //}
        //else
        //{
        //    mon_action();
        //}
        if (!is_dead)
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
                case CurrentState.ESKILL:
                    Skill();
                    break;
            }
        }
    }
    public void Idle()
    {
        if(!is_dead)
        {
            currnet_state_cool += Time.deltaTime;
            if(lock_target == null)
            {
                if(currnet_state_cool >= idle_cool_time)
                {
                    current_state = CurrentState.EPATROL;
                }
            }
            if(lock_target != null)
            {
                current_state = CurrentState.ESKILL;
            }
        }
    }

    public void Patrol()
    {
        if(!is_dead)
        {
            nav.speed = patrol_speed;
            if(lock_target == null)
            {
                Collider[] targets = Physics.OverlapSphere(transform.position, patrol_dist, target_mask);
                for(int i=0; i < targets.Length; i++)
                {
                    player = targets[i].GetComponent<PhotonTestPlayer>();
                    if(player != null)
                    {
                        lock_target = player.gameObject;
                        current_state = CurrentState.ESKILL;
                        break;
                    }
                    if (player == null)
                        return;
                }
            }
        }
    }

    public void Chase()
    {
        //none
    }

    public void Attack()
    {
        //none
    }

    public void Skill()
    {
        if (lock_target != null)
        {
            nav.speed = chase_speed;
            lock_target_pos = lock_target.transform.position;
            nav.SetDestination(lock_target_pos);
            float dist = (transform.position - lock_target_pos).magnitude;
            if(dist >= skill_dist)
            {
                Collider[] collider = Physics.OverlapSphere(transform.position, 20f, target_mask);
                for(int i = 0; i < collider.Length; i++)
                {
                    player = collider[i].GetComponent<PhotonTestPlayer>();
                    if(player != null)
                    {
                        player.current_hp -= damage;
                    }
                    if (player == null)
                        return;
                }
            }
        }
    }
    public void Is_Dead()
    {
        if(is_dead)
        {
            
        }
    }
}
