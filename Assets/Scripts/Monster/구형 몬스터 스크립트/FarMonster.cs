//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using System;
//using UnityEngine.AI;

//public class FarMonster : MonsterBase
//{
//    Action test;
//    // Start is called before the first frame update
//    void Start()
//    {
//        _wait_time = 5f;
//        test += Check_State;
//        test += Current_State;
//        test += Check_Isdead;
//    }

//    void Check_State() //1���� ����
//    {//�ִ�, ����, ���ݷ�, ����, �����ӵ�, ��������, �i�� ����,�i�� �ӵ�, ���� �ӵ�, �����Ÿ�, �׾�����
//        if (!is_dead)
//        {
//            //yield return new WaitForSeconds(_wait_time);
//            max_hp = 100;
//            current_hp = 100;
//            damage = 10;
//            defense = 11;
//            patrol_speed = 15f;
//            patrol_dist = 25f;
//            chase_dist = 70f;
//            chase_speed = 20f;
//            attack_speed = 3f;
//            attack_dist = 15f;
//            move_range = 100f;
//            idle_cool_time = 1f;
//            chase_cool_time = 3f;
//            skill_cool_time = 15f;
//            is_dead = false;
//            current_state = CurrentState.EPATROL;
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

//    void Current_State() //��� Ȯ��
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
//    void Check_Isdead() //��� Ȯ��
//    {

//        Is_Dead();
//    }
//    protected override void Patrol()
//    {
//        base.Patrol();
//    }
//    protected override void Attack()
//    {
//        //���Ÿ� ����
//    }

//    void Skill()
//    {

//    }

//    private void OnDrawGizmos()
//    {
//        Gizmos.DrawWireSphere(transform.position, _move_range);
//        Gizmos.color = Color.blue;
//        Gizmos.DrawWireSphere(transform.position, _patrol_dist);

//    }

//}
