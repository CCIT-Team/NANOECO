using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Photon.Pun;
using Photon.Realtime;

#region ENUM 값들
public enum MONSTER_TYPE
{
    ENomalMonster,
    ENomalFarMonster,
    EScoutMonster,

    ETankerMonster,
    EWideCloseMonster,
    EWideFarMonster,
    ESelfDestructMonster
}

public enum CURRNET_STATE
{
    EIdle = 0,
    EPatrol,
    EChase,
    EAttack,
    ESkill
}
#endregion

public struct Data
{
    public float max_hp;
    public float current_hp;
    public float damage;
    public float defense;
    public float patrol_speed;
    public float chase_speed;

    public float patrol_dist;
    public float chase_dist;
    public float attack_dist;
    public float skill_dist;
    public float event_chase_dist;

    public float idle_cool_time;
    public float chase_cool_time;
    public float attack_cool_time;
    public float skill_cool_time;

    public float current_time;
    public float state_time;
}

public abstract class NewMonster : MonoBehaviourPunCallbacks, IPunObservable
{
    #region 파라미터
    protected readonly int hash_walk = Animator.StringToHash("Walk");
    protected readonly int hash_attack = Animator.StringToHash("Attack");
    protected readonly int hash_chase = Animator.StringToHash("Chase");
    private readonly int hash_hit = Animator.StringToHash("Hit");
    private readonly int hash_dead = Animator.StringToHash("Dead");
    protected readonly int hash_skill = Animator.StringToHash("Skill");
    private readonly int hash_idle = Animator.StringToHash("Idle");

    public Data data;
    [SerializeField]
    protected MONSTER_TYPE monster_type;
    [SerializeField]
    protected Collider[] targets;
    protected Player player;
    [SerializeField]
    protected NavMeshAgent agent;
    [SerializeField]
    protected GameObject[] Particles; //1: 죽음 이펙트, 2: 죽음 이펙트, 3: 히트 이펙트, 4: 스킬관련
    protected GameObject lock_target;
    [SerializeField]
    protected LayerMask target_mask;


    [SerializeField]
    protected Animator animator;

    [SerializeField]
    protected AudioClip attack_clip;
    [SerializeField]
    protected AudioClip hit_clip;
    [SerializeField]
    protected AudioClip chase_clip;
    [SerializeField]
    protected AudioClip dead_clip;
    [SerializeField]
    protected AudioClip skill_clip;
    [SerializeField]
    protected AudioClip always_clip; //키ㅣ키캬캬ㅑ 하는 것들..
    [SerializeField]
    protected AudioSource audioplayer;


    public bool hit_true = false;
    [SerializeField]
    protected bool is_dead = false;

    public bool on_event = false;  //이벤트로 생성된 몹

    [SerializeField]
    protected CURRNET_STATE current_state = new CURRNET_STATE();

    float RandTime = 5;
    public float Rand_Chase_Time;
    #endregion
    Vector3 curPos;
    Quaternion curRot;
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
            stream.SendNext(data.current_hp);
            stream.SendNext(is_dead);
        }
        else
        {
            curPos = (Vector3)stream.ReceiveNext();
            curRot = (Quaternion)stream.ReceiveNext();
            data.current_hp = (float)stream.ReceiveNext();
            is_dead = (bool)stream.ReceiveNext();
        }
    }
    public abstract void Init();

    public virtual void Idle()
    {
        //애니메이션 초기화 + 사운드 상태 초기화 == init에 넣어도 될듯
        //idle 애니메이션
        data.state_time += Time.deltaTime;
        
        if(lock_target == null)
        {
            if(data.state_time >= data.idle_cool_time)
            {
                current_state = CURRNET_STATE.EPatrol;
                data.state_time = 0f;
            }
        }
        else
        {
            current_state = CURRNET_STATE.EChase;
        }

    }

    public virtual void Patrol()
    {
        //패트롤 애니메이
        animator.SetBool(hash_walk, true);
        agent.speed = data.patrol_speed;
        RandTime -= Time.deltaTime;
        if(!agent.hasPath)
        {
            agent.SetDestination(Get_Random_Point(transform, data.patrol_dist));
        }
        //몇 초뒤에 확인 했는데 똑같은 자리라면 다시 랜덤으로 돌려보리기
        if (RandTime <= 0)
        {
            agent.SetDestination(Get_Random_Point(transform, data.patrol_dist));
            RandTime = Random.Range(1f,4f);
        }
        if (on_event ==true)
        {
            Collider[] targets = Physics.OverlapSphere(transform.position, data.event_chase_dist, target_mask);
            for (int i = 0; i < targets.Length; i++)
            {
                player = targets[i].GetComponent<Player>();
                if (player != null)
                {
                    lock_target = player.gameObject;
                    current_state = CURRNET_STATE.EChase;
                    break;
                }
                if (targets == null)
                    return;
            }
        }
        else
        {
            Collider[] targets = Physics.OverlapSphere(transform.position, data.chase_dist, target_mask);
            for (int i = 0; i < targets.Length; i++)
            {
                player = targets[i].GetComponent<Player>();
                if (player != null)
                {
                    lock_target = player.gameObject;
                    current_state = CURRNET_STATE.EChase;
                    break;
                }
                if (targets == null)
                    return;
            }
        }
    }

    public virtual void Chase()
    {
        if(lock_target != null)
        {
            animator.SetBool(hash_walk, false);
            animator.SetBool(hash_chase, true);
            agent.speed = data.chase_speed;
            data.state_time += Time.deltaTime;
            data.current_time += Time.deltaTime;
            float dist = (lock_target.transform.position - transform.position).magnitude;
            //공격 범위 보다 넓은 스킬 먼저 체크
            if(dist <= data.skill_dist)
            {
                 if(data.current_time >= data.skill_cool_time)
                {
                    current_state = CURRNET_STATE.ESkill; //스킬 부분으로 들어가게 된다면 멈춰서 스킬 사용(자폭제외)
                    data.current_time = 0f;
                }
            }
            else
            {
                agent.SetDestination(lock_target.transform.position);
            }

            if(dist <= data.attack_dist)
                current_state = CURRNET_STATE.EAttack;
            
            if(on_event == true)
            {
                if (dist > data.event_chase_dist)
                {
                    if (data.state_time >= data.chase_cool_time)
                    {
                        agent.SetDestination(transform.position);
                        lock_target = null;
                        animator.SetTrigger(hash_idle);
                        animator.SetBool(hash_chase, false);
                        current_state = CURRNET_STATE.EIdle;
                        data.state_time = 0f;
                    }
                }
            }
            else
            {
                if (dist > data.chase_dist)
                {
                    if (data.state_time >= data.chase_cool_time)
                    {
                        agent.SetDestination(transform.position);
                        lock_target = null;
                        animator.SetTrigger(hash_idle);
                        animator.SetBool(hash_chase, false);
                        current_state = CURRNET_STATE.EIdle;
                        data.state_time = 0f;
                    }
                }
            }
        }
    }

    public virtual void Attack()
    {
        if (lock_target != null)
        {
            data.current_time += Time.deltaTime;
            float dist = (lock_target.transform.position - transform.position).magnitude;
            transform.LookAt(lock_target.transform);
            if (dist <= data.attack_dist)
            {
                if (data.current_time >= data.attack_cool_time)
                {
                    audioplayer.PlayOneShot(attack_clip);
                    animator.SetTrigger(hash_attack);
                    agent.stoppingDistance = (data.attack_dist-0.5f); 
                    player.current_hp -= data.damage;   //데미지 주는 부분 변경 필요
                    data.current_time = 0;
                }
                else
                {
                    current_state = CURRNET_STATE.EChase;
                }
            }
            else
            {
                current_state = CURRNET_STATE.EChase;
            }
        }
        else
        {
            current_state = CURRNET_STATE.EIdle;
            animator.SetBool(hash_chase, false);
        }
    }

    public virtual void Skill() { }
    
    public virtual void Hp_Check()
    {
        if(data.current_hp <= 0)
        {
            is_dead = true;
            Is_Dead();
            is_dead = false;
        }
    }

    public virtual void Is_Dead() 
    {
        if (is_dead)
        {
            //콜라이더 disable
            agent.SetDestination(transform.position);
            agent.isStopped = true;
            audioplayer.PlayOneShot(dead_clip);
            animator.SetTrigger(hash_dead);
            animator.SetBool(hash_walk, false);
            animator.SetBool(hash_chase, false);
            Instantiate(Particles[0], transform.position, Quaternion.identity);
            Instantiate(Particles[1], transform.position, Quaternion.identity);
            Destroy(gameObject, 0.3f);
            Init();
            current_state = CURRNET_STATE.EIdle;
        }
    }

    public virtual void Hit_Mon()
    {
        if (hit_true == true)
        {
            if (data.current_hp <= 20f)
            {
                Instantiate(Particles[2], transform.position, Quaternion.identity);
                audioplayer.PlayOneShot(hit_clip);
                hit_true = false;
            }
            else
            {
                Instantiate(Particles[2], transform.position, Quaternion.identity);
                animator.SetTrigger(hash_hit);
                audioplayer.PlayOneShot(hit_clip);
                hit_true = false;
            }
        }

    }

    public virtual void Another_Find_Player()
    {
        if(lock_target != null)
        {
            Rand_Chase_Time -= Time.deltaTime;
            if(Rand_Chase_Time <= 0)
            {
                lock_target = null;
                Rand_Chase_Time = 15f;
                current_state = CURRNET_STATE.EIdle;
            }
        }
    }

    public virtual void Monster_State()
    {
        if (!is_dead)
        {
            switch (current_state)
            {
                case CURRNET_STATE.EIdle:
                    audioplayer.clip = always_clip;
                    Idle();
                    break;
                case CURRNET_STATE.EPatrol:
                    audioplayer.clip = always_clip;
                    Patrol();
                    break;
                case CURRNET_STATE.EChase:
                    audioplayer.clip = chase_clip;
                    Chase();
                    break;
                case CURRNET_STATE.EAttack:
                    Attack();
                    break;
                case CURRNET_STATE.ESkill:
                    Skill();
                    break;
            }
        }
    }

    bool Random_Point(Vector3 center, float range, out Vector3 result)
    {

        for (int i = 0; i < 30; i++)
        {
            Vector3 randomPoint = center + UnityEngine.Random.insideUnitSphere * range;
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

    private Vector3 Get_Random_Point(Transform point = null, float radius = 0)
    {
        Vector3 _point;

        if (Random_Point(point == null ? transform.position : point.position, radius == 0 ? data.patrol_dist : radius, out _point))
        {
            Debug.DrawRay(_point, Vector3.up, Color.black, 1);

            return _point;
        }

        return point == null ? Vector3.zero : point.position;
    }

    private void OnDrawGizmos()
    {
        //Gizmos.color = Color.red;
        //Gizmos.DrawWireSphere(transform.position, data.chase_dist);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, data.attack_dist);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, data.event_chase_dist);
    }

}


