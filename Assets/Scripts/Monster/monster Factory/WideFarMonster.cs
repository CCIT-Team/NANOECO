using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.AI;

class WideFarMonster : Monster
{
    Action mon_action;
    public WideFarMonster()
    {
        type = UnitType.EWideFarMonster;
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
    public void Is_Dead()
    {

    }
    bool Random_Point(Vector3 center, float range, out Vector3 result)
    {

        for (int i = 0; i < 30; i++)
        {
            Vector3 randomPoint = center + UnityEngine.Random.insideUnitSphere * range;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
            {
                result = hit.position;
                return true;
            }
        }

        result = Vector3.zero;

        return false;
    }

    private Vector3 Get_Random_Point(Transform point = null, float radius = 0)
    {
        Vector3 _point;

        if (Random_Point(point == null ? transform.position : point.position, radius == 0 ? move_range : radius, out _point))
        {
            Debug.DrawRay(_point, Vector3.up, Color.black, 1);

            return _point;
        }

        return point == null ? Vector3.zero : point.position;
    }
}
