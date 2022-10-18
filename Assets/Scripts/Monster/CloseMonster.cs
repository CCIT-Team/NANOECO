using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CloseMonster : MonsterBase
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CheckState());
    }

    IEnumerator CheckState()
    {//최대, 현재, 공격력, 방어력, 순찰속도, 순찰범위, 쫒아 범위,쫒는 속도, 공격 속도, 사정거리, 죽었는지
        if (!isdead)
        {
            _wait_time = 5f;
            yield return new WaitForSeconds(_wait_time);
            nav = GetComponent<NavMeshAgent>();
            _monster_max_hp = 150;
            _monster_hp = 150;
            _damage = 15;
            _defense = 11;
            _patrol_speed = 15f;
            _patrol_dist = 25f;
            _chase_dist = 30f;
            _chase_speed = 20f;
            _attack_speed = 3f;
            _attack_dist = 5f;
            _move_range = 50f;
            _cool_time = 1f;
            non_combet_state = NonCombetState.EIDLE;
        }
    }

    private void Update()
    {
        StartCoroutine(Non_State());
        StartCoroutine(Combat_State());
    }

    IEnumerator Non_State()
    {
        if(!isdead)
        {
            yield return new WaitForSeconds(_wait_time);
            switch(non_combet_state)
            {
                case NonCombetState.EIDLE:
                    Idle();
                    break;
                case NonCombetState.EPATROL:
                    Patrol();
                    break;
                case NonCombetState.ETHINK:
                    Think();
                    break;
            }
        }
    }
    
    IEnumerator Combat_State()
    {
        if(!isdead)
        {
            yield return new WaitForSeconds(_wait_time);
            switch(combat_state)
            {
                case CombatState.ECHASE:
                    Chase();
                    break;
                case CombatState.EATTACK:
                    Attack();
                    break;
                case CombatState.ESKILL:
                    Skill();
                    break;
            }
        }
    }

    void Attack()
    {
        StartCoroutine(Atteck_Cool());
        
    }

    IEnumerator Atteck_Cool()
    {
        yield return new WaitForSeconds(0.1f);
        Debug.Log("Attack Player");
    }

    void Skill()
    {

    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, _move_range);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, patrol_dist);
        
    }

}
