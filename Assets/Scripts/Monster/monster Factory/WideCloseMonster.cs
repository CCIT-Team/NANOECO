//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using System;
//using Photon.Pun;
//using Photon.Realtime;
//using UnityEngine.AI;

//class WideCloseMonster : Monster, IMonsterBase, IMonsterAttack, IMonsterIdle, IMonsterChase, IMonsterPatrol
//{
//    Action mon_action;
//    public WideCloseMonster()
//    {
//        type = UnitType.EWideCloseMonster;
//        if (!is_dead)
//        {
//            //�������ͽ�
//            max_hp = 50f;
//            damage = 1f;
//            defense = 0.5f;
//            patrol_speed = 15f;
//            chase_speed = 0;
//            //����
//            move_range = 0;
//            patrol_dist = 0;
//            chase_dist = 0;
//            attack_dist = 0;
//            skill_dist = 0;
//            //��Ÿ��
//            wait_time = 0f;
//            idle_cool_time = 0;
//            chase_cool_time = 0;
//            attack_cool_time = 0;
//            skill_cool_time = 0;
//            //����
//            is_dead = false;
//            current_state = CurrentState.EIDLE;
//        }
//    }
//    private void Awake()
//    {
//        //if (!PhotonNetwork.IsMasterClient) ���� �׽�Ʈ ��
//        //{
//        //    return;
//        //}
//        //else
//        //{
//        //    mon_action += Currnet_State;
//        //    mon_action += Is_Dead;
//        //}
//        mon_action += Currnet_State;
//        mon_action += Is_Dead;
//    }
//    private void FixedUpdate()
//    {
//        //if (!PhotonNetwork.IsMasterClient) ���� �׽�Ʈ ��
//        //{
//        //    return;
//        //}
//        //else
//        //{
//        //    mon_action();
//        //}
//        if (!is_dead)
//        {
//            mon_action();
//        }
//    }
//    public void Currnet_State()
//    {
//        if (!is_dead)
//        {
//            switch (current_state)
//            {
//                case CurrentState.EIDLE:
//                    Idle();
//                    break;
//                case CurrentState.EPATROL:
//                    Patrol();
//                    break;
//                case CurrentState.ECHASE:
//                    Chase();
//                    break;
//                case CurrentState.EATTACK:
//                    Attack();
//                    break;
//            }
//        }
//    }
//    public void Idle()
//    {
//        currnet_state_cool += Time.deltaTime;
//        lock_target = null;
//        if(currnet_state_cool >= idle_cool_time)
//        {
//            current_state = CurrentState.EPATROL;
//        }
//        if(lock_target != null)
//        {
//            current_state = CurrentState.ECHASE;
//        }
        
//    }

//    public void Patrol()
//    {
//        if(lock_target == null)
//        {

//        }
//        if(lock_target != null)
//        {

//        }
//    }

//    public void Chase()
//    {

//    }

//    public void Attack()
//    {

//    }

//    public void Is_Dead()
//    {

//    }

//    bool Random_Point(Vector3 center, float range, out Vector3 result)
//    {

//        for (int i = 0; i < 30; i++)
//        {
//            Vector3 randomPoint = center + UnityEngine.Random.insideUnitSphere * range;
//            NavMeshHit hit;
//            if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
//            {
//                result = hit.position;
//                return true;
//            }
//        }

//        result = Vector3.zero;

//        return false;
//    }

//    private Vector3 Get_Random_Point(Transform point = null, float radius = 0)
//    {
//        Vector3 _point;

//        if (Random_Point(point == null ? transform.position : point.position, radius == 0 ? move_range : radius, out _point))
//        {
//            Debug.DrawRay(_point, Vector3.up, Color.black, 1);

//            return _point;
//        }

//        return point == null ? Vector3.zero : point.position;
//    }
//}
