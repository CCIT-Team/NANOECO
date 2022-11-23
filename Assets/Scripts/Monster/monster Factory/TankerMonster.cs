//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using System;
//using Photon.Pun;
//using Photon.Realtime;
//using UnityEngine.AI;

//class TankerMonster : Monster, IMonsterBase, IMonsterAttack, IMonsterIdle, IMonsterChase, IMonsterPatrol
//{
//    Action mon_action;
//    public TankerMonster()
//    {
//        type = UnitType.ETankerMonster;
//        if (!is_dead)
//        {
//            //스테이터스
//            max_hp = 250f;
//            damage = 4f;
//            defense = 10f;
//            patrol_speed = 10f;
//            chase_speed = 15f;
//            //범위
//            move_range = 50f;
//            patrol_dist = 20f;
//            chase_dist = 25f;
//            attack_dist = 4f;
//            //쿨타임
//            wait_time = 0f;
//            idle_cool_time = 2f;
//            chase_cool_time = 2f;
//            attack_cool_time = 5f;
//            //상태
//            is_dead = false;
//            current_state = CurrentState.EIDLE;
//        }
//    }
//    private void Awake()
//    {
//        //if (!PhotonNetwork.IsMasterClient) 포톤 테스트 전
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
//        //if (!PhotonNetwork.IsMasterClient) 포톤 테스트 전
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
//        switch (current_state)
//        {
//            case CurrentState.EIDLE:
//                Idle();
//                break;
//            case CurrentState.EPATROL:
//                Patrol();
//                break;
//            case CurrentState.ECHASE:
//                Chase();
//                break;
//            case CurrentState.EATTACK:
//                Attack();
//                break;
//        }
//    }
//    public void Idle()
//    {
//        lock_target = null;
//        currnet_state_cool += Time.deltaTime;
//        if (lock_target == null)
//        {
//            if (currnet_state_cool >= idle_cool_time)
//            {
//                current_state = CurrentState.EPATROL;
//                currnet_state_cool = 0;
//            }
//        }
//        else
//        {
//            current_state = CurrentState.ECHASE;
//        }
    
//    }

//    public void Patrol()
//    {
//        nav.speed = patrol_speed;
//        if (!nav.hasPath)
//        {
//            nav.SetDestination(Get_Random_Point(transform, move_range));
//        }
//        Collider[] targets = Physics.OverlapSphere(transform.position, patrol_dist, target_mask);
//        for (int i = 0; i < targets.Length; i++)
//        {
//            player = targets[i].GetComponent<PhotonTestPlayer>();
//            if (lock_target == null)
//            {
//                lock_target = player.gameObject;
//                current_state = CurrentState.ECHASE;
//                break;
//            }
//            if (lock_target != null)
//            {
//                current_state = CurrentState.ECHASE;
//            }
//        }
//    }

//    public void Chase()
//    {
//        if (lock_target != null)
//        {
//            nav.speed = chase_speed;
//            currnet_state_cool += Time.deltaTime;
//            current_time += Time.deltaTime;
//            lock_target_pos = lock_target.transform.position;
//            float dist = (transform.position - lock_target_pos).magnitude;
//            if (dist <= attack_dist)
//            {
//                if(current_time >= attack_cool_time)
//                {
//                    current_state = CurrentState.EATTACK;
//                    current_time = 0;
//                }
//            }
//            else
//            {
//                nav.stoppingDistance = attack_dist - 1f;
//                nav.SetDestination(lock_target_pos);
//            }
//            if(dist >= chase_dist)
//            {
//                current_state = CurrentState.EIDLE;
//            }
//        }
        
//    }

//    public void Attack()
//    {
//        if(lock_target != null)
//        {
//            lock_target_pos = lock_target.transform.position;
//            current_time += Time.deltaTime;
//            float distance = (lock_target.transform.position - transform.position).magnitude;
//            //transform.LookAt(lock_target_pos);
//            transform.rotation.SetLookRotation(lock_target_pos);
//            if (distance <= attack_dist)
//            {
//                if (current_time >= attack_cool_time)
//                {
//                    nav.stoppingDistance = (attack_dist - 1f);
//                    player.current_hp -= damage;
//                    current_time = 0f;
//                }
//                else
//                {
//                    current_state = CurrentState.ECHASE;
//                }
//            }
//            else
//            {
//                current_state = CurrentState.ECHASE;
//            }
//        }
//        else
//        {
//            current_state = CurrentState.EIDLE;
//        }
//    }

//    public void Is_Dead()
//    {
//        if(is_dead)
//        {
//            Instantiate(Particles[0], transform.position, Quaternion.identity);
//            Destroy(gameObject, 0.2f);
//            current_state = CurrentState.EIDLE;
//        }
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
