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
    protected int _monster_max_hp; //최대체력
    [SerializeField]
    protected int _monster_hp; //현재체력
    [SerializeField]
    protected int _damage; //공격력
    [SerializeField]
    protected object _defense; //방어력 일단 int형
    [SerializeField]
    protected float _parol_speed; //순찰속도
    [SerializeField]
    protected float _chase_dist; //쫒는 범위
    [SerializeField]
    protected float _chase_speed; //쫒는 속도
    [SerializeField]
    protected float _attack_speed; //공격속도
    [SerializeField]
    protected float _attack_dist; //사정거리
    [SerializeField]
    protected bool isdead = false;

    int attacktype; //경갑인지 중갑인지
    int armortype; //장갑이 경갑인지 중갑인지

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
        EIDLE, //정지상태 혹은 주변 두리번
        EPATROL, //패트롤 스피트
        EATTACK, //쫒는 속도와 공격 속도 공격력
        EDEAD
    }

    enum AttackType //중갑인지 경갑인지 아마 %이걸로 바뀔 가능성 높음
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

    //패트롤

}
