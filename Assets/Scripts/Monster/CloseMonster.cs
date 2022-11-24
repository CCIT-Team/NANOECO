//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.AI;

//public class CloseMonster : MonsterBase
//{
//    // Start is called before the first frame update
//    void Start()
//    {
//        StartCoroutine(CheckState());
//    }

//    IEnumerator CheckState()
//    {//최대, 현재, 공격력, 방어력, 순찰속도, 순찰범위, 쫒아 범위,쫒는 속도, 공격 속도, 사정거리, 죽었는지
//        if (!is_dead)
//        {
//            _wait_time = 5f;
//            yield return new WaitForSeconds(_wait_time);
//            nav = GetComponent<NavMeshAgent>();
//            _max_hp = 150;
//            _current_hp = 150;
//            _damage = 15;
//            _defense = 11;
//            _patrol_speed = 15f;
//            _patrol_dist = 25f;
//            _chase_dist = 30f;
//            _chase_speed = 20f;
//            _attack_speed = 3f;
//            _attack_dist = 5f;
//            _move_range = 50f;
//            _idle_cool_time = 1f;
//            _chase_cool_time = 20f;
//            _attack_cool_time = 1f;
//            _is_dead = false;
//            current_state = CurrentState.EPATROL;
//        }
//    }

//    private void Update()
//    {
//        StartCoroutine(Non_State());
//        StartCoroutine(Current_State());
//        StartCoroutine(Check_Isdead());
//    }

//    IEnumerator Non_State()
//    {
//        if(!is_dead)
//        {
//            yield return new WaitForSeconds(_wait_time);
//            switch(non_combet_state)
//            {
//                case NonCombetState.ETHINK:
//                    Think();
//                    break;
//            }
//        }
//    }
    
//    IEnumerator Current_State()
//    {
//        if(!is_dead)
//        {
//            yield return new WaitForSeconds(_wait_time);
//            switch(current_state)
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
//    IEnumerator Check_Isdead()
//    {
//        yield return new WaitForSeconds(10f);
//        Is_Dead();
//    }
//    protected override void Patrol()
//    {
//        base.Patrol();
//    }
//    protected override void Attack()
//    {
//        base.Attack();
//    }

//    IEnumerator Atteck_Cool()
//    {
//        //쿨타임
//        //공격 하는거
        
//        yield return new WaitForSeconds(0.1f);
        
//    }

//    void Skill()
//    {

//    }

//    private void OnDrawGizmos()
//    {
//        Gizmos.DrawWireSphere(transform.position, _move_range);
//        Gizmos.color = Color.blue;
//        Gizmos.DrawWireSphere(transform.position, patrol_dist);
        
//    }

//}
