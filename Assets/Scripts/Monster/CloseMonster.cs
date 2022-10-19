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
    {//�ִ�, ����, ���ݷ�, ����, �����ӵ�, ��������, �i�� ����,�i�� �ӵ�, ���� �ӵ�, �����Ÿ�, �׾�����
        if (!isdead)
        {
            _wait_time = 5f;
            yield return new WaitForSeconds(_wait_time);
            nav = GetComponent<NavMeshAgent>();
            _max_hp = 150;
            _current_hp = 150;
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
            current_state = CurrentState.EPATROL;
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
            switch(current_state)
            {
                case CurrentState.EPATROL:
                    Patrol();
                    break;
                case CurrentState.ECHASE:
                    Chase();
                    break;
                case CurrentState.EATTACK:
                    Attack();
                    break;
                case CurrentState.ESKILL:
                    Skill();
                    break;
            }
        }
    }

    protected override void Attack()
    {
        base.Attack();
    }

    IEnumerator Atteck_Cool()
    {
        //��Ÿ��
        //���� �ϴ°�
        
        yield return new WaitForSeconds(0.1f);
        
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
