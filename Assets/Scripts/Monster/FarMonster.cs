using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FarMonster : MonsterBase
{
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CheckState());
    }

    IEnumerator CheckState()
    {//최대, 현재, 공격력, 방어력, 순찰속도, 쫒아 범위,쫒는 속도, 공격 속도, 사정거리, 죽었는지
        if (!isdead)
        {
            yield return new WaitForSeconds(5f);
            nav = GetComponent<NavMeshAgent>();
            monsterpos = transform.position;
            _max_hp = 50;
            _current_hp = 50;
            _damage = 5;
            _defense = 1;
            _patrol_speed = 10f;
            _patrol_dist = 30f;
            _chase_dist = 35f;
            _chase_speed = 15f;
            _attack_speed = 5f;
            _attack_dist = 20f;
            current_state = CurrentState.EPATROL;
        }
    }
    // Update is called once per frame
    void Update()
    {
        //전투
        StartCoroutine(Combat_State());
        //비전투
        StartCoroutine(Non_Combet_State());

    }

    IEnumerator Non_Combet_State()
    {
        //비전투
        if(!isdead)
        {
            if (current_state != CurrentState.ECHASE)
            {
                
                yield return new WaitForSeconds(6f);
                switch(non_combet_state)
                {
                    case NonCombetState.EIDLE :
                        Idle();
                        break;
                    case NonCombetState.ETHINK:
                        Think();
                        break;      
                }
            }
        }
    }

    IEnumerator Combat_State()
    {
        if(!isdead)
        {
            if (current_state == CurrentState.ECHASE)
            {
                yield return new WaitForSeconds(0.1f);
                switch(current_state)
                {
                    case CurrentState.EPATROL:
                        Patrol();
                        break;
                    case CurrentState.ECHASE:
                        Chase();
                        break;
                    case CurrentState.EATTACK:
                        break;
                    case CurrentState.ESKILL:
                        break;
                }
            }
        }
    }


    protected override void Patrol()
    {
        base.Patrol();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _patrol_dist);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, _attack_dist);
    }



}
