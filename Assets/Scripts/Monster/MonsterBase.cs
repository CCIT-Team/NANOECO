using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterBase : Character
{
    
    protected NavMeshAgent nav;
    [SerializeField]
    protected GameObject target;
    [SerializeField]
    protected GameObject locktarget;
    [SerializeField]
    protected Vector3 monsterpos;

    [Header("STATS")]
    [SerializeField]
    protected float _patrol_speed; //�����ӵ�
    [SerializeField]
    protected float _patrol_dist; //���� ����
    [SerializeField]
    protected float _chase_dist; //�i�� ����
    [SerializeField]
    protected float _chase_speed; //�i�� �ӵ�
    [SerializeField]
    protected float _attack_speed; //���ݼӵ�
    [SerializeField]
    protected float _attack_dist; //�����Ÿ�
    [SerializeField]
    protected float _move_range; //���� �� ������
    [SerializeField]
    protected float _cool_time;

    [Header("STATE")]
    [SerializeField]
    protected bool isdead = false;
    [SerializeField]
    protected NonCombetState non_combet_state = new NonCombetState();
    [SerializeField]
    protected CombatState combat_state = new CombatState();
    [SerializeField]
    protected AttackType attack_type = new AttackType();

    [Header("LodingTime")]
    [SerializeField]
    protected float _wait_time;

    

    public float patrol_speed { get { return _patrol_speed; } set { _patrol_speed = value; } }
    public float patrol_dist { get { return _patrol_dist; } set { _patrol_dist = value; } }
    public float chase_dist { get { return _chase_dist; } set { _chase_dist = value; } }
    public float chase_speed { get { return _chase_speed; } set { _chase_speed = value; } }
    public float attack_speed { get { return _attack_speed; } set { _attack_speed = value; } }
    public float attack_dist { get { return _attack_dist; } set { _attack_dist = value; } }
    public float move_range { get { return _move_range; } set { _move_range = value; } }
    public float wait_time { get { return _wait_time; } set { _wait_time = value; } }
    public float cool_time { get { return _cool_time; } set { _cool_time = value; } }



    float currnetcool = 0f;

    //��Ʈ��
    protected virtual void Patrol()
    {
        
        if(non_combet_state == NonCombetState.EPATROL)
        {
            nav.speed = patrol_speed;
            if (!nav.hasPath)
            {
                nav.SetDestination(GetRandomPoint(transform, move_range));
            }
            Update_Patrol();
            if (current_hp < max_hp)
            {
                combat_state = CombatState.ECHASE;
            }
        }

    }
    //���̵�
    protected virtual void Idle()
    {
        currnetcool += Time.deltaTime;
        if(locktarget !=null)
        {
            non_combet_state = NonCombetState.ENONE;
            combat_state = CombatState.ECHASE;
        }
        if(currnetcool >= cool_time)
        {
            currnetcool = 0f;
            non_combet_state = NonCombetState.EPATROL;
        }

    }
    //����
    protected virtual void Think()
    {
        
    }

    //ü�̽�
    protected virtual void Chase()
    {
        if(locktarget !=null)
        {
            currnetcool += Time.deltaTime;
            monsterpos = locktarget.transform.position;
            float distance = (monsterpos - transform.position).magnitude;
            if (distance <= attack_dist)
            {
                if(currnetcool >= attack_speed)
                {
                    combat_state = CombatState.EATTACK;
                    currnetcool = 0;
                    return;
                }
            }

            Vector3 dir = monsterpos - transform.position;
            if(dir.magnitude < 0.1f)
            {
                non_combet_state = NonCombetState.EPATROL;
            }
            else
            {
                nav.speed = chase_speed;
                nav.SetDestination(monsterpos);

                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 10 * Time.deltaTime);
            }
        }
    }

    //�÷��̾� ã��
    protected virtual void Update_Patrol()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        if (target == null)
            return;
        float distance = (target.transform.position - transform.position).magnitude;
        if(distance <= patrol_dist)
        {
            locktarget = target;
            non_combet_state = NonCombetState.ENONE;
            combat_state = CombatState.ECHASE;
            return;
        }
    }

    //����
    protected virtual void Is_Dead()
    {
        if(current_hp == 0)
        {
            isdead = true;
        }
    }

    //���� ��Ʈ��
    bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {

        for (int i = 0; i < 30; i++)
        {
            Vector3 randomPoint = center + Random.insideUnitSphere * range;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
            {
                result = hit.position;
                return true;
            }
        }

        result = Vector3.zero;

        return false;
    }

    public Vector3 GetRandomPoint(Transform point = null, float radius = 0)
    {
        Vector3 _point;

        if (RandomPoint(point == null ? transform.position : point.position, radius == 0 ? move_range : radius, out _point))
        {
            Debug.DrawRay(_point, Vector3.up, Color.black, 1);

            return _point;
        }

        return point == null ? Vector3.zero : point.position;
    }
}
