using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Photon.Pun;
using Photon.Realtime;

abstract class AbsMonsterBase : MonoBehaviourPunCallbacks
{
    [Space(10f)]
    [SerializeField]
    protected NavMeshAgent nav;
    protected GameObject target;
    protected GameObject[] test_target;
    protected GameObject lock_target;
    protected Transform player_transform;
    protected Vector3 lock_target_pos;
    protected Vector3 init_pos = new Vector3(0, 0, 0);
    protected LayerMask target_mask;

    [Space(10f)]
    [SerializeField]
    private float _max_hp; 
    private float _current_hp; 
    private float _damage; 
    private float _defense; 
    private float _patrol_speed; 
    private float _chase_speed; 

    private float _move_range; 
    private float _patrol_dist; 
    private float _chase_dist; 
    private float _attack_dist; 
    private float _skill_dist;

    private float _idle_cool_time; 
    private float _chase_cool_time;
    private float _attack_cool_time;
    private float _skill_cool_time;

    private float _wait_time;
    protected float currnet_state_cool = 0f;
    public float current_time = 0;

    [Space(10f)]
    [Header("STATE")]
    [SerializeField] 
    private bool _is_dead;

    [SerializeField]
    protected CurrentState current_state = new CurrentState();

    public float max_hp { get  => _max_hp;  set => _max_hp = value; } 
    public float current_hp { get  => _current_hp;  set => _current_hp = _max_hp;  }
    public float damage { get  => _damage;  set => _damage = value; } 
    public float defense { get  => _defense;  set => _defense = value; } 
    public float patrol_speed { get => _patrol_speed; set => _patrol_speed = value; }
    public float chase_speed { get => _chase_speed; set => _chase_speed = value; }

    public float move_range { get => _move_range; set => _move_range = value; }
    public float patrol_dist { get => _patrol_dist; set => _patrol_dist = value; }
    public float chase_dist { get => _chase_dist; set => _chase_dist = value; }
    public float attack_dist { get => _attack_dist; set => _attack_dist = value; }
    public float skill_dist { get => _skill_dist; set => _skill_dist = value; }

    public float wait_time { get => _wait_time; set => _wait_time = value; }
    public float idle_cool_time { get => _idle_cool_time; set => _idle_cool_time = value; }
    public float chase_cool_time { get => _chase_cool_time; set => _chase_cool_time = value; }
    public float attack_cool_time { get => _attack_cool_time; set => _attack_cool_time = value; }
    public float skill_cool_time { get => _skill_cool_time; set => _skill_cool_time = value; }

    public bool is_dead 
    { 
        get => _is_dead; 
        set 
        { 
            if (_current_hp <= 0)
            {
                _is_dead = true;
                Is_daed();
                Init_Mon();
            }
            else 
            {
                _is_dead = false; 
            } 
        }
    }
    //해당 기능만 프로퍼티 떄문에 추상클래스에 선언
    abstract protected void Is_daed();
    abstract protected void Init_Mon();



}
