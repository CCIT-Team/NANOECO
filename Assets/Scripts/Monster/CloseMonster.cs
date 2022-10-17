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
            yield return new WaitForSeconds(5f);
            nav = GetComponent<NavMeshAgent>();
            monsterpos = transform.position;
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
            _move_range = 5f;
            _range = 10f;
        }
    }

    private void Update()
    {
        StartCoroutine(Non_Combet_State());
    }

    IEnumerator Non_Combet_State()
    {
        //비전투
        if (!isdead)
        {
            if (combat_state != CombatState.ECHASE)
            {

                yield return new WaitForSeconds(6f);
                switch (non_combet_state)
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
    }

    IEnumerator Combat_State()
    {
        if(!isdead)
        {
            if (combat_state == CombatState.ECHASE)
            {
                yield return new WaitForSeconds(0.1f);
                switch (combat_state)
                {
                    case CombatState.ECHASE:
                        break;
                    case CombatState.EATTACK:
                        break;
                    case CombatState.ESKILL:
                        Debug.Log("SKILL");
                        break;
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, _move_range);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _patrol_dist);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, _attack_dist);

        
    }

}
