using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Photon.Pun;
using Photon.Realtime;
public enum UnitType
{
    //���� �� ���
    ENomalMonster,
    ENomalFarMonster,
    EScoutMonster,

    ETankerMonster,
    EWideCloseMonster,
    EWideFarMonster,
    ESelfDestructMonster
}

abstract class Monster : MonoBehaviourPunCallbacks
{
    [SerializeField]
    protected Collider[] targets;
    protected PhotonTestPlayer player;
    protected UnitType type;
    [SerializeField]
    protected NavMeshAgent nav;
    [SerializeField]
    protected GameObject[] Particles;
    protected GameObject lock_target;
    protected Vector3 lock_target_pos;
    [SerializeField]
    protected LayerMask target_mask;
    [SerializeField]
    protected float max_hp = 0f;
    protected float current_hp = 0f;
    protected float damage = 0f;
    protected float defense = 0f;
    protected float patrol_speed = 0f;
    protected float chase_speed = 0f;

    protected float move_range = 0f;
    protected float patrol_dist = 0f;
    protected float chase_dist = 0f;
    protected float attack_dist = 0f;
    protected float skill_dist = 0f;

    protected float idle_cool_time = 0f;
    protected float chase_cool_time = 0f;
    protected float attack_cool_time = 0f;
    protected float skill_cool_time = 0f;

    protected float wait_time = 0f;
    protected float currnet_state_cool = 0f;
    protected float current_time = 0;

    protected bool is_dead = false;

    [SerializeField]
    protected CurrentState current_state = new CurrentState();
    
    
}
