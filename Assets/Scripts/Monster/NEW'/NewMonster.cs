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

    public float idle_cool_time;
    public float chase_cool_time;
    public float attack_cool_time;
    public float skill_cool_time;

    public float current_time;
    public float state_time;
}

public abstract class NewMonster : MonoBehaviourPunCallbacks
{
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

    protected bool _is_dead = false;
    public bool is_dead
    {
        get => _is_dead;
        set
        {
            if (data.current_hp <= 0)
            {
                _is_dead = true;
                Is_Dead();
                Init();
            }
            else
            {
                _is_dead = false;
            }
        }
    }
    [SerializeField]
    protected CURRNET_STATE current_state = new CURRNET_STATE();

    //애니메이션 관련 컴포넌트

    public abstract void Init();

    public virtual void Idle()
    {
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
            

            if(dist > data.chase_dist)
            {
                if(data.state_time >= data.chase_cool_time)
                {
                    lock_target = null;
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
                    agent.stoppingDistance = (data.attack_dist - 1f);
                    player.current_hp -= data.damage;
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
        }
    }

    public virtual void Skill() { }
    
    public virtual void Is_Dead() 
    {
        if (is_dead)
        {
            Instantiate(Particles[0], transform.position, Quaternion.identity);
            Destroy(gameObject);
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
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, data.chase_dist);
    }

}


