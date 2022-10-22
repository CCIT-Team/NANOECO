using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.AI;

public class CloseMonster : MonsterBase
{
    Action test;
    // Start is called before the first frame update
    void Start()
    {
        _wait_time = 5f;
        test += Check_State;
        test += Current_State;
        test += Check_Isdead;
    }

    void Check_State() //1번만 실행
    {//최대, 현재, 공격력, 방어력, 순찰속도, 순찰범위, 쫒아 범위,쫒는 속도, 공격 속도, 사정거리, 죽었는지
        if (!_is_dead)
        {
            //yield return new WaitForSeconds(_wait_time);
            _max_hp = 150;
            _current_hp = 150;
            _damage = 15;
            _defense = 11;
            _patrol_speed = 15f;
            _patrol_dist = 25f;
            _chase_dist = 60f;
            _chase_speed = 20f;
            _attack_speed = 3f;
            _attack_dist = 5f;
            _move_range = 100f;
            _idle_cool_time = 1f;
            _chase_cool_time = 3f;
            _skill_cool_time = 15f;
            _is_dead = false;
            current_state = CurrentState.EPATROL;
        }
    }

    private void Update()
    {
        current_time += Time.deltaTime;
        if(current_time >= wait_time)
        {
            test();
            test -= Check_State;
        }
    }

    void Non_State()
    {
        if(!_is_dead)
        {
            switch(non_combet_state)
            {
                case NonCombetState.ETHINK:
                    Think();
                    break;
            }
        }
    }
    
    void Current_State() //계속 확인
    {
        if(!_is_dead)
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
    void Check_Isdead() //계속 확인
    {
        
        Is_Dead();
    }
    protected override void Patrol()
    {
        base.Patrol();
    }
    protected override void Attack()
    {
        base.Attack();
    }

    void Skill()
    {

    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, _move_range);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, _patrol_dist);
        
    }

}
