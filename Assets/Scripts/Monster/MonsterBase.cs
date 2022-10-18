using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterBase : MonoBehaviour
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
    protected int _monster_max_hp; //�ִ�ü��
    [SerializeField]
    protected int _monster_hp; //����ü��
    [SerializeField]
    protected int _damage; //���ݷ�
    [SerializeField]
    protected object _defense; //���� �ϴ� int��
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
    protected float _range; //��Ʈ�� ����

    [Header("STATE")]
    [SerializeField]
    protected bool isdead = false;

    int attacktype; //�氩���� �߰�����
    int armortype; //�尩�� �氩���� �߰�����

    public int monster_hp { get { return _monster_hp; } set { _monster_hp = value; } }
    public int monster_max_hp { get { return _monster_max_hp; } set { _monster_max_hp = value; } }
    public int damage { get { return _damage; } set { _damage = value; } }
    public int defense { get { return (int)_defense; } set { _defense = value; } }
    public float patrol_speed { get { return _patrol_speed; } set { _patrol_speed = value; } }
    public float patrol_dist { get { return _patrol_dist; } set { _patrol_dist = value; } }
    public float chase_dist { get { return _chase_dist; } set { _chase_dist = value; } }
    public float chase_speed { get { return _chase_speed; } set { _chase_speed = value; } }
    public float attack_speed { get { return _attack_speed; } set { _attack_speed = value; } }
    public float attack_dist { get { return _attack_dist; } set { _attack_dist = value; } }
    public float move_range { get { return _move_range; } set { _move_range = value; } }
    public float range { get { return _range; } set { _range = value; } }

    [SerializeField]
    protected NonCombetState non_combet_state = new NonCombetState();
    [SerializeField]
    protected CombatState combat_state = new CombatState();
    [SerializeField]
    protected AttackType attack_type = new AttackType();

    //��Ʈ��
    protected virtual void Patrol()
    {
        Update_Patrol();
        if(non_combet_state == NonCombetState.EPATROL)
        {
            nav.speed = patrol_speed;
            if (!nav.hasPath)
            {
                nav.SetDestination(GetRandomPoint(transform, _range));
            }

            Collider[] colliders = Physics.OverlapSphere(this.transform.position, _patrol_dist);
            foreach (var items in colliders)
            {
                if (items.tag == "Player")
                {
                    combat_state = CombatState.ECHASE;
                }
            }

            if (_monster_hp < _monster_max_hp)
            {
                combat_state = CombatState.ECHASE;
            }
        }

    }
    //���̵�
    protected virtual void Idle()
    {
        if(non_combet_state == NonCombetState.EIDLE)
        {

        }
    }
    //����
    protected virtual void Think()
    {
        if(non_combet_state == NonCombetState.ETHINK)
        {

        }
    }
    //ü�̽�
    protected virtual void Chase()
    {
        if(locktarget !=null)
        {
            monsterpos = locktarget.transform.position;
            float distance = (monsterpos - transform.position).sqrMagnitude;
            if ((distance*distance) <= attack_dist)
            {
                //��Ÿ�� �ʿ�
                combat_state = CombatState.EATTACK;
                return;
            }
        }



    }
    //�÷��̾� ã��
    protected virtual void Update_Patrol()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        if (target == null)
            return;
        float distance = (target.transform.position - transform.position).sqrMagnitude;
        if((distance * distance) <= patrol_dist)
        {
            locktarget = target;
            combat_state = CombatState.ECHASE;
            return;
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

        if (RandomPoint(point == null ? transform.position : point.position, radius == 0 ? _range : radius, out _point))
        {
            Debug.DrawRay(_point, Vector3.up, Color.black, 1);

            return _point;
        }

        return point == null ? Vector3.zero : point.position;
    }
}
