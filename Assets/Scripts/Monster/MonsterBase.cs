using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterBase : MonoBehaviour
{
    protected NavMeshAgent nav;
    [SerializeField]
    Transform target;

    [SerializeField]
    protected int _monster_max_hp; //�ִ�ü��
    [SerializeField]
    protected int _monster_hp; //����ü��
    [SerializeField]
    protected int _damage; //���ݷ�
    [SerializeField]
    protected object _defense; //���� �ϴ� int��
    [SerializeField]
    protected float _parol_speed; //�����ӵ�
    [SerializeField]
    protected float _chase_dist; //�i�� ����
    [SerializeField]
    protected float _chase_speed; //�i�� �ӵ�
    [SerializeField]
    protected float _attack_speed; //���ݼӵ�
    [SerializeField]
    protected float _attack_dist; //�����Ÿ�
    [SerializeField]
    protected bool isdead = false;

    int attacktype; //�氩���� �߰�����
    int armortype; //�尩�� �氩���� �߰�����

    public int monster_hp { get { return _monster_hp; } set { _monster_hp = value; } }
    public int monster_max_hp { get { return _monster_max_hp; } set { _monster_max_hp = value; } }
    public int damage { get { return _damage; } set { _damage = value; } }
    public int defense { get { return (int)_defense; } set { _defense = value; } }
    public float parol_speed { get { return _parol_speed; } set { _parol_speed = value; } }
    public float chase_dist { get { return _chase_dist; } set { _chase_dist = value; } }
    public float chase_speed { get { return _chase_speed; } set { _chase_speed = value; } }
    public float attack_speed { get { return _attack_speed; } set { _attack_speed = value; } }
    public float attack_dist { get { return _attack_dist; } set { _attack_dist = value; } }


    enum CombatState
    {
        EIDLE, //�������� Ȥ�� �ֺ� �θ���
        EPATROL, //��Ʈ�� ����Ʈ
        EATTACK, //�i�� �ӵ��� ���� �ӵ� ���ݷ�
        EDEAD
    }

    enum AttackType //�߰����� �氩���� �Ƹ� %�̰ɷ� �ٲ� ���ɼ� ����
    {
        ELIGHT,
        EHAEVY
    }

    enum NonCombetState
    {
        ETALK,
        EDANCE,
        ETHINK
    }

    private void Start()
    {
        StartCoroutine(Before_Start());
    }
    private void Update()
    {
        
    }

    IEnumerator Before_Start()
    {
        yield return new WaitForSeconds(5f);
        nav = GetComponent<NavMeshAgent>();
        nav.SetDestination(target.position);
    }

    //��Ʈ��

}
