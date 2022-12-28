using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Photon.Pun;
using Photon.Realtime;

#region ENUM ����
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

    public float idle_cool_time;
    public float chase_cool_time;
    public float attack_cool_time;
    public float skill_cool_time;

    public float current_time;
    public float state_time;
}

public abstract class NewMonster : MonoBehaviourPunCallbacks
{
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
    protected GameObject[] Particles;
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
    protected AudioClip always_clip; //Ű��Űļļ�� �ϴ� �͵�..
    [SerializeField]
    protected AudioSource audioplayer;


    public bool hit_true = false;
    [SerializeField]
    protected bool is_dead = false;

    [SerializeField]
    protected CURRNET_STATE current_state = new CURRNET_STATE();

    //�ִϸ��̼� ���� ������Ʈ

    public abstract void Init();

    public virtual void Idle()
    {
        //�ִϸ��̼� �ʱ�ȭ + ���� ���� �ʱ�ȭ == init�� �־ �ɵ�
        //idle �ִϸ��̼�
        
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
        //��Ʈ�� �ִϸ���
        animator.SetBool(hash_walk, true);
        agent.speed = data.patrol_speed;
        if(!agent.hasPath)
        {
            agent.SetDestination(Get_Random_Point(transform, data.patrol_dist));
        }
        Collider[] targets = Physics.OverlapSphere(transform.position, data.chase_dist, target_mask);
        for(int i = 0; i < targets.Length; i++)
        {
            player = targets[i].GetComponent<Player>();
            if(player != null)
            {
                lock_target = player.gameObject;
                current_state = CURRNET_STATE.EChase;
                break;
            }
            if (targets == null)
                return;
        }
    }

    public virtual void Chase()
    {
        if(lock_target != null)
        {
            animator.SetBool(hash_walk, false);
            audioplayer.PlayOneShot(chase_clip);
            animator.SetBool(hash_chase, true);
            agent.speed = data.chase_speed;
            data.state_time += Time.deltaTime;
            data.current_time += Time.deltaTime;
            float dist = (lock_target.transform.position - transform.position).magnitude;
            //���� ���� ���� ���� ��ų ���� üũ
            if(dist <= data.skill_dist)
            {
                 if(data.current_time >= data.skill_cool_time)
                {
                    current_state = CURRNET_STATE.ESkill; //��ų �κ����� ���� �ȴٸ� ���缭 ��ų ���(��������)
                    data.current_time = 0f;
                }
            }
            else
            {
                agent.SetDestination(lock_target.transform.position);
            }

            if(dist <= data.attack_dist)
                current_state = CURRNET_STATE.EAttack;
            

            if(dist > data.chase_dist)
            {
                agent.SetDestination(transform.position);
                if(data.state_time >= data.chase_cool_time)
                {
                    lock_target = null;
                    animator.SetTrigger(hash_idle);
                    animator.SetBool(hash_chase, false);
                    current_state = CURRNET_STATE.EIdle;
                    data.state_time = 0f;
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
            transform.rotation = Quaternion.Lerp(transform.rotation, lock_target.transform.rotation, Time.deltaTime);
            if (dist <= data.attack_dist)
            {
                if (data.current_time >= data.attack_cool_time)
                {
                    audioplayer.PlayOneShot(attack_clip);
                    animator.SetTrigger(hash_attack);
                    agent.stoppingDistance = (data.attack_dist - 1f); 
                    player.current_hp -= data.damage;   //������ �ִ� �κ� ���� �ʿ�
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
            audioplayer.PlayOneShot(dead_clip);
            animator.SetTrigger(hash_dead);
            animator.SetBool(hash_walk, false);
            animator.SetBool(hash_chase, false);
            Instantiate(Particles[0], transform.position, Quaternion.identity);
            agent.SetDestination(transform.position);
            Destroy(gameObject, 2f);
            Init();
            current_state = CURRNET_STATE.EIdle;
        }
    }

    public virtual void Monster_State()
    {
        if (!is_dead)
        {
            switch (current_state)
            {
                case CURRNET_STATE.EIdle:
                    Idle();
                    break;
                case CURRNET_STATE.EPatrol:
                    Patrol();
                    break;
                case CURRNET_STATE.EChase:
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
    //���� �ǰ�
    private void Hit_Mon()
    {
        if(hit_true == true)
        {
            animator.SetTrigger(hash_hit);
            audioplayer.PlayOneShot(hit_clip);
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, data.chase_dist);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, data.attack_dist);
    }

}


