//using System.Collections;
//using System.Collections.Generic;
//using System;
//using UnityEngine;
////using UnityEngine.Pool;

//public class SimpleFarMon : MonsterBase
//{
//    Action test;
//    public Transform shot_pos;
//    public GameObject bullets;
//    //private IObjectPool<SimpleMonBullet> bullet_pool;
//    [SerializeField]
//    private Transform par;

//    private void Start()
//    {
//        _wait_time = 0.5f;
//        test += Check_State;
//        test += Simple_State;
//        test += Check_Isdead;
//    }

//    void Check_State() //1번만 실행
//    {//최대, 현재, 공격력, 방어력, 순찰속도, 순찰범위, 쫒아 범위,쫒는 속도, 공격 속도, 사정거리, 죽었는지
//        if (!is_dead)
//        {
//            //yield return new WaitForSeconds(_wait_time);
//            max_hp = 50;
//            current_hp = 50;
//            damage = 5;
//            defense = 11;
//            patrol_speed = 0f;
//            patrol_dist = 0f;
//            chase_dist = 0f;
//            chase_speed = 15f;
//            attack_speed = 3f;
//            attack_dist = 14f;
//            move_range = 0f;
//            idle_cool_time = 0f;
//            chase_cool_time = 0f;
//            skill_cool_time = 0f;
//            is_dead = false;
//            current_state = CurrentState.ECHASE;
//            //bullet_pool = new ObjectPool<SimpleMonBullet>(Create_Bullets, Get_Bullets, Release_Bullets, Destroy_Bullets, maxSize: 40);
//        }
//    }

//    private void Update()
//    {
//        current_time += Time.deltaTime;
//        if (current_time >= wait_time)
//        {
//            test();
//            test -= Check_State;
//        }
//    }

//    void Simple_State()
//    {
//        if (!is_dead)
//        {
//            switch (current_state)
//            {
//                case CurrentState.EIDLE:
//                    Idle();
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

//    void Check_Isdead()
//    {
//        Is_Dead();
//    }

//    protected override void Attack()
//    {
//        lock_target_pos = lock_target.transform.position;
//        Character character = lock_target.GetComponent<Character>();
//        //데미지 수식이 들어가야 됨
//        currnetcool += Time.deltaTime;
//        if (character.current_hp > 0)
//        {
//            float distance = (lock_target.transform.position - transform.position).magnitude;
//            if (distance <= attack_dist)
//            {
//                transform.LookAt(lock_target_pos);
//                if (currnetcool >= attack_speed)
//                {
//                    nav.stoppingDistance = 15f;
//                    Instantiate(bullets, shot_pos.position, Quaternion.identity);
//                    //var bullet = bullet_pool.Get();
//                    //bullet.Shoot();
//                    currnetcool = 0f;
//                }
//            }
//            else
//            {
//                current_state = CurrentState.ECHASE;
//            }
//        }
//    }

//    protected override void Chase()
//    {
//        lock_target = GameObject.FindGameObjectWithTag("Player");
//        if (lock_target == null)
//            return;
//        currnetcool += Time.deltaTime;
//        lock_target_pos = lock_target.transform.position;
//        nav.speed = chase_speed;
//        nav.stoppingDistance = 10f;
//        nav.SetDestination(lock_target_pos);
//        float dist = (lock_target_pos - transform.position).magnitude;
//        //공격 볌위
//        if (dist <= attack_dist)
//        {
//            current_state = CurrentState.EATTACK;
//        }
//    }

//    protected override void Idle()
//    {
//        current_state = CurrentState.ECHASE;
//    }

//    //private SimpleMonBullet Create_Bullets()
//    //{
//    //    SimpleMonBullet pool_bullets = Instantiate(bullets, par).GetComponent<SimpleMonBullet>();
//    //    pool_bullets.SetManagedBulletPool(bullet_pool);
//    //    return pool_bullets;
//    //}

//    //private void Get_Bullets(SimpleMonBullet bullet)
//    //{
//    //    bullet.gameObject.SetActive(true);
//    //}

//    //private void Release_Bullets(SimpleMonBullet bullet)
//    //{
//    //    bullet.gameObject.SetActive(false);
//    //}

//    //private void Destroy_Bullets(SimpleMonBullet bullet)
//    //{
//    //    Destroy(bullet.gameObject);
//    //}

//}