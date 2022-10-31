using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Photon.Pun;
using Photon.Realtime;

class NomalFarMonster : Monster, IMonsterBase
{
    Action mon_action;
    public NomalFarMonster()
    {
        type = UnitType.ENomalFarMonster;
        if (!is_dead)
        {

            max_hp = 30f;
            damage = 1f;
            defense = 0.5f;
            patrol_speed = 15f;
            chase_speed = 10f;

            chase_dist = 100f;
            attack_dist = 8f;

            wait_time = 0f;
            chase_cool_time = 2f;
            attack_cool_time = 3f;

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
        if (!is_dead)
        {
            Collider[] targets = Physics.OverlapSphere(transform.position, chase_dist, target_mask);

            for (int i = 0; i < targets.Length; i++)
            {
                PhotonTestPlayer character = targets[i].GetComponent<PhotonTestPlayer>();
                if (character != null)
                {
                    lock_target = character.gameObject;
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
                nav.stoppingDistance = (attack_dist - 1f);
                nav.SetDestination(lock_target_pos);
            }
        }
    }

    public void Attack()
    {
        if (lock_target != null)
        {
            lock_target_pos = lock_target.transform.position;
            currnet_state_cool += Time.deltaTime;
            float distance = (lock_target.transform.position - transform.position).magnitude;
            //transform.LookAt(lock_target_pos);
            transform.rotation.SetLookRotation(lock_target_pos);

            if (distance <= attack_dist)
            {
                if (current_time >= attack_cool_time)
                {
                    nav.stoppingDistance = (attack_dist - 1f);
                    PhotonTestPlayer player;
                    //player.currnet_hp -= damage; 현재 포톤 테스트 플레이어 체력이 없어서 이렇게 둠
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
}
