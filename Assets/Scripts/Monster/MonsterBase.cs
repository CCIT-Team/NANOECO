using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.AI;

public class MonsterBase : Character
{
    [Space(10f)]
    public NavMeshAgent nav;
    [SerializeField]
    protected GameObject target;
    [SerializeField]
    protected GameObject[] test_target;
    [SerializeField]
    protected LayerMask target_mask;
    [SerializeField]
    protected GameObject lock_target;
    [SerializeField]
    protected Vector3 lock_target_pos;
    [SerializeField]
    protected Transform player_transform;
    [SerializeField]
    protected Vector3 init_pos = new Vector3(0,0,0);

    [Space(10f)]
    [Header("STATS")]
    [SerializeField]
    protected float _patrol_speed; //순찰속도
    [SerializeField]
    protected float _patrol_dist; //순찰 범위
    [SerializeField]
    protected float _chase_dist; //쫒는 범위
    [SerializeField]
    protected float _chase_speed; //쫒는 속도
    [SerializeField]
    protected float _attack_speed; //공격속도
    [SerializeField]
    protected float _attack_dist; //사정거리
    [SerializeField]
    protected float _move_range; //범위 내 움직임
    [SerializeField]
    protected float _skill_dist;
    [SerializeField]
    protected float _idle_cool_time; //통합 쿨타임은 안될듯
    [SerializeField]
    protected float _chase_cool_time;
    [SerializeField]
    protected float _skill_cool_time;

    [Space(10f)]
    [Header("STATE")]
    [SerializeField]
    protected CurrentState current_state = new CurrentState();

    [Space(10f)]
    [Header("LodingTime")]
    [SerializeField]
    protected float _wait_time;
    protected float currnetcool = 0f;
    public float current_time = 0;
    
    public float patrol_speed { get => _patrol_speed;  set => _patrol_speed = value; }
    public float patrol_dist { get => _patrol_dist;  set => _patrol_dist = value;  }
    public float chase_dist { get => _chase_dist;  set => _chase_dist = value;  }
    public float chase_speed { get =>  _chase_speed;  set => _chase_speed = value;  }
    public float attack_speed { get => _attack_speed;  set => _attack_speed = value;  }
    public float attack_dist { get => _attack_dist;  set => _attack_dist = value;  }
    public float skill_dist { get => _skill_dist; set => _skill_dist = value; }
    public float move_range { get => _move_range;  set => _move_range = value;  }
    public float wait_time { get => _wait_time;  set => _wait_time = value;  }
    public float idle_cool_time { get =>  _idle_cool_time;  set => _idle_cool_time = value;  }
    public float chase_cool_time { get => _chase_cool_time;  set => _chase_cool_time = value;  }
    public float skill_cool_time { get => _skill_cool_time;  set => _skill_cool_time = value;  }


    protected virtual void Patrol()
    {
        nav.speed = patrol_speed;
        if (!nav.hasPath)
        {
            nav.SetDestination(Get_Random_Point(transform, move_range));
        }
        if (current_hp < max_hp)
        {
            current_state = CurrentState.ECHASE;
        }
        Update_Patrol();
    }

    protected virtual void Update_Patrol()
    {
        target = GameObject.FindGameObjectWithTag("Player"); //레이어로 찾는 걸로 변경 필요
        if (target == null)
            return;
        float distance = (target.transform.position - transform.position).magnitude;
        if (distance <= patrol_dist)
        {
            lock_target = target;
            current_state = CurrentState.ECHASE;
            return;
        }
    }

    protected virtual void Get_Targets()//포톤 적용후 사용 예정
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, patrol_dist, target_mask);
        for(int i = 0; i < colliders.Length; i++)
        {
            test_target = colliders[i].GetComponents<GameObject>();
            if (test_target != null && !is_dead)
            {
                current_state = CurrentState.ECHASE;
                break;
            }
        }
    }

    protected virtual void Idle()
    {
        currnetcool += Time.deltaTime;
        
        if (lock_target != null)
        {
            current_state = CurrentState.ECHASE;
        }
        if(lock_target == null)
        {
            if (currnetcool >= idle_cool_time)
            {
                current_state = CurrentState.EPATROL;
                currnetcool = 0f;
            }
        }
    }

    protected virtual void Chase()
    {
        if (lock_target != null)
        {
            currnetcool += Time.deltaTime;
            lock_target_pos = lock_target.transform.position;
            float dist = (lock_target_pos - transform.position).magnitude;
            //공격 볌위
            if (dist <= attack_dist)
            {
                current_state = CurrentState.EATTACK;
            }
            
            if (dist <= chase_dist)
            {
                nav.speed = chase_speed;
                nav.stoppingDistance = 3f;
                nav.SetDestination(lock_target_pos);
            }

            if(dist > chase_dist)
            {
                if(currnetcool >= chase_cool_time)
                {
                    
                    lock_target = null;
                    current_state = CurrentState.EIDLE;
                    currnetcool = 0f;
                }
            }
        }
    }

    protected virtual void Attack()
    {
        if (lock_target != null)
        {
            lock_target_pos = lock_target.transform.position;
            Character character = lock_target.GetComponent<Character>();
            //데미지 수식이 들어가야 됨
            currnetcool += Time.deltaTime;

            if (character.current_hp > 0)
            {
                float distance = (lock_target.transform.position - transform.position).magnitude;
                transform.LookAt(lock_target_pos);
                if (distance <= attack_dist)
                {
                    if (currnetcool >= attack_speed)
                    {
                        nav.stoppingDistance = 3f;
                        character.current_hp -= damage;
                        currnetcool = 0f;
                    }
                    else
                    {
                        current_state = CurrentState.ECHASE;
                    }
                }
                else
                {
                    current_state = CurrentState.ECHASE;
                }
            }
            else
            {
                current_state = CurrentState.EIDLE;
            }
        }
        else
        {
            current_state = CurrentState.EIDLE;
        }
    }
 
    protected virtual void Is_Dead()
    {
        if(current_hp <= 0)
        {
            is_dead = true;
            if (is_dead)
            {
                Destroy(gameObject);
                current_state = CurrentState.EIDLE;
                Init_Mon();
            }
        }
    }

    protected virtual void Init_Mon()
    {
        transform.position = init_pos;
        current_hp = max_hp;
        is_dead = false;
        current_state = CurrentState.EIDLE;
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

        if (Random_Point(point == null ? transform.position : point.position, radius == 0 ? move_range : radius, out _point))
        {
            Debug.DrawRay(_point, Vector3.up, Color.black, 1);

            return _point;
        }

        return point == null ? Vector3.zero : point.position;
    }

}
