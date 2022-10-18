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
    {//�ִ�, ����, ���ݷ�, ����, �����ӵ�, �i�� ����,�i�� �ӵ�, ���� �ӵ�, �����Ÿ�, �׾�����
        if (!isdead)
        {
            yield return new WaitForSeconds(5f);
            nav = GetComponent<NavMeshAgent>();
            monsterpos = transform.position;
            _monster_max_hp = 50;
            _monster_hp = 50;
            _damage = 5;
            _defense = 1;
            _patrol_speed = 10f;
            _patrol_dist = 30f;
            _chase_dist = 35f;
            _chase_speed = 15f;
            _attack_speed = 5f;
            _attack_dist = 20f;
            _range = 10f;
        }
    }
    // Update is called once per frame
    void Update()
    {
        //����
        StartCoroutine(Combat_State());
        //������
        StartCoroutine(Non_Combet_State());

    }

    IEnumerator Non_Combet_State()
    {
        //������
        if(!isdead)
        {
            if (combat_state != CombatState.ECHASE)
            {
                
                yield return new WaitForSeconds(6f);
                switch(non_combet_state)
                {
                    case NonCombetState.EIDLE :
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
                switch(combat_state)
                {
                    case CombatState.ECHASE:
                        Chase();
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
