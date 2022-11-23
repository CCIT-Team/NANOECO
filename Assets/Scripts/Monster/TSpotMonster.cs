//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.AI;
//using System;

//class TSpotMonster : AbsMonsterBase, IMonsterBase
//{ 
//    Action mon_action;
//    public GameObject particles;

//    [Header("이벤트 세팅")]
//    public EventData event_data;
//    public List<GameObject> event_point;
//    public bool is_run;

//    private void Awake()
//    {
//        mon_action += Currnet_Check;
//        mon_action += Currnet_State;
//        mon_action += Is_daed;
//    }

//    private void Start()
//    {

//    }

//    private void Update()
//    {
//        current_time += Time.deltaTime;
//        if (current_time >= wait_time)
//        {
//            mon_action -= Currnet_Check;
//        }
//    }

//    private void FixedUpdate()
//    {
//        mon_action();
//    }

//    public void Currnet_Check()
//    {
//        if (!is_dead)
//        {
//            max_hp = 100f;
//            damage = 3f;
//            defense = 0.5f;
//            patrol_speed = 15f;
//            chase_speed = 20f;
//            move_range = 100f;
//            patrol_dist = 25f;
//            chase_dist = 30f;
//            attack_dist = 3;
//            skill_dist = 20f;
//            wait_time = 1f;
//            idle_cool_time = 1f;
//            chase_cool_time = 2f;
//            attack_cool_time = 2f;
//            skill_cool_time = 60f;
//            is_dead = false;
//            current_state = CurrentState.ECHASE;
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
//                case CurrentState.ESKILL:
//                    Skill();
//                    break;
//            }
//        }
//    }

//    public void Idle()
//    {
//        currnet_state_cool += Time.deltaTime;
//        if (lock_target != null)
//        {
//            current_state = CurrentState.ECHASE;
//        }
//        if (lock_target == null)
//        {
//            if (currnet_state_cool >= idle_cool_time)
//            {
//                current_state = CurrentState.EPATROL;
//                currnet_state_cool = 0f;
//            }
//        }
//    }

//    public void Patrol()
//    {

//    }

//    public void Chase()
//    {
//        if (lock_target != null)
//        {
//            currnet_state_cool += Time.deltaTime;
//            lock_target_pos = lock_target.transform.position;
//            float dist = (lock_target_pos - transform.position).magnitude;
//            //공격 볌위
//            if (dist <= attack_dist)
//            {
//                current_state = CurrentState.EATTACK;
//            }

//            if (dist <= chase_dist)
//            {
//                nav.speed = chase_speed;
//                nav.stoppingDistance = 3f;
//                nav.SetDestination(lock_target_pos);
//            }

//            if (dist > chase_dist)
//            {
//                if (currnet_state_cool >= chase_cool_time)
//                {

//                    lock_target = null;
//                    current_state = CurrentState.EIDLE;
//                    currnet_state_cool = 0f;
//                }
//            }
//        }
//    }

//    public void Attack()
//    {
//        if (lock_target != null)
//        {
//            lock_target_pos = lock_target.transform.position;
//            currnet_state_cool += Time.deltaTime;


//            float distance = (lock_target.transform.position - transform.position).magnitude;
//            transform.LookAt(lock_target_pos);
//            if (distance <= skill_dist)
//            {
//                if (current_time >= skill_cool_time)
//                {
//                    nav.stoppingDistance = (skill_dist - 1f);
//                    current_state = CurrentState.ESKILL;
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
//    }

//    public void Skill()
//    {
//        event_data.event_point = event_point;
//        event_data.Send_Event();
//        is_run = true;
//    }
//    protected override void Is_daed()
//    {
//        if (is_dead)
//        {
//            Instantiate(particles, transform.position, Quaternion.identity);
//            Destroy(gameObject, 0.2f);
//            current_state = CurrentState.EIDLE;
//        }
//    }
//    protected override void Init_Mon()
//    {
//        transform.position = init_pos;
//        current_hp = max_hp;
//        is_dead = false;
//        current_state = CurrentState.EIDLE;
//    }

//    public void Is_Dead()
//    {

//    }
//}