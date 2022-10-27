using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Pool;

public class SimpleCloseMon : MonsterBase
{
    private Action test;
    private void Start()
    {
        _wait_time = 0.5f;
        test += Check_State;
        test += Simple_State;
        test += Check_Isdead;
    }

    void Check_State() //1번만 실행
    {
        if (!_is_dead)
        {
            _max_hp = 50;
            _current_hp = 50;
            _damage = 5;
            _defense = 11;
            _patrol_speed = 0f;
            _patrol_dist = 0f;
            _chase_dist = 0f;
            _chase_speed = 25f;
            _attack_speed = 3f;
            _attack_dist = 5f;
            _move_range = 0f;
            _idle_cool_time = 0f;
            _chase_cool_time = 0f;
            _skill_cool_time = 0f;
            _is_dead = false;
            current_state = CurrentState.ECHASE;
        }
    }

    private void Update()
    {
        current_time += Time.deltaTime;
        if (current_time >= wait_time)
        {
            test();
            test -= Check_State;
        }
    }

    void Simple_State()
    {
        if (!_is_dead)
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

    void Check_Isdead() => Is_Dead();
    

    protected override void Attack()
    {
        Character character = lock_target.GetComponent<Character>();
        //데미지 수식이 들어가야 됨
        currnetcool += Time.deltaTime;

        if (character.current_hp > 0)
        {
            float distance = (lock_target.transform.position - transform.position).magnitude;
            if (distance <= attack_dist)
            {
                if (currnetcool >= attack_speed)
                {
                    character.current_hp -= damage;
                    currnetcool = 0f;
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

    protected override void Chase()
    {
        lock_target = GameObject.FindGameObjectWithTag("Player");
        if (lock_target == null)
            return;
        currnetcool += Time.deltaTime;
        lock_target_pos = lock_target.transform.position;
        nav.speed = chase_speed;
        nav.SetDestination(lock_target_pos);
        float dist = (lock_target_pos - transform.position).magnitude;
        //공격 볌위
        if (dist <= attack_dist)
        {
            current_state = CurrentState.EATTACK;
        }  
    }

    protected override void Idle() =>
        current_state = CurrentState.ECHASE;
}
