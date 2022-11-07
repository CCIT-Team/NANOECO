using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Photon.Pun;
using Photon.Realtime;

class NomalFarMonster : Monster, IMonsterBase, IMonsterAttack, IMonsterIdle, IMonsterChase
{
    Action mon_action;
    [SerializeField]
    private GameObject bullets;
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
        //if (!PhotonNetwork.IsMasterClient) 포톤 테스트 전
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
        //if (!PhotonNetwork.IsMasterClient) 포톤 테스트 전
        //{
        //    return;
        //}
        //else
        //{
        //    mon_action();
        //}
        if(!is_dead)
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

    public void Chase()
    {
        if (lock_target != null)
        {
            lock_target_pos = lock_target.transform.position;
            float dist = (lock_target_pos - transform.position).magnitude;
            //공격 볌위
            if (dist <= attack_dist)
            {
                current_state = CurrentState.EATTACK;
            }
            else
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
            current_time += Time.deltaTime;
            lock_target_pos = lock_target.transform.position;
            float distance = (lock_target.transform.position - transform.position).magnitude;
            //transform.LookAt(lock_target_pos);
            transform.rotation.SetLookRotation(lock_target_pos);

            if (distance <= attack_dist)
            {
                if (current_time >= attack_cool_time)
                {
                    nav.stoppingDistance = (attack_dist - 1f);
                    Instantiate(bullets, transform);
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
