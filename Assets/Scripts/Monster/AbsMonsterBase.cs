using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

abstract class AbsMonsterBase : MonoBehaviour
{
    [Space(10f)]
    [SerializeField]
    private NavMeshAgent nav;
    private GameObject target;
    private GameObject[] test_target;
    private LayerMask target_mask;
    private GameObject lock_target;
    private Vector3 lock_target_pos;
    private Transform player_transform;
    private Vector3 init_pos = new Vector3(0, 0, 0);

    [Space(10f)]
    [Header("STATS")]
    private float _patrol_speed; //�����ӵ�
    private float _patrol_dist; //���� ����
    private float _chase_dist; //�i�� ����
    private float _chase_speed; //�i�� �ӵ�
    private float _attack_speed; //���ݼӵ�
    private float _attack_dist; //�����Ÿ�
    private float _move_range; //���� �� ������
    private float _skill_dist;
    private float _idle_cool_time; //���� ��Ÿ���� �ȵɵ�
    private float _chase_cool_time;
    private float _skill_cool_time;

    [Space(10f)]
    [Header("STATE")]
    [SerializeField]
    protected CurrentState current_state = new CurrentState();

    [Space(10f)]
    [Header("LodingTime")]
    protected float _wait_time;
    protected float currnetcool = 0f;
    public float current_time = 0;
    public GameObject partest;

    public float patrol_speed { get => _patrol_speed; set => _patrol_speed = value; }
    public float patrol_dist { get => _patrol_dist; set => _patrol_dist = value; }
    public float chase_dist { get => _chase_dist; set => _chase_dist = value; }
    public float chase_speed { get => _chase_speed; set => _chase_speed = value; }
    public float attack_speed { get => _attack_speed; set => _attack_speed = value; }
    public float attack_dist { get => _attack_dist; set => _attack_dist = value; }
    public float skill_dist { get => _skill_dist; set => _skill_dist = value; }
    public float move_range { get => _move_range; set => _move_range = value; }
    public float wait_time { get => _wait_time; set => _wait_time = value; }
    public float idle_cool_time { get => _idle_cool_time; set => _idle_cool_time = value; }
    public float chase_cool_time { get => _chase_cool_time; set => _chase_cool_time = value; }
    public float skill_cool_time { get => _skill_cool_time; set => _skill_cool_time = value; }
}
