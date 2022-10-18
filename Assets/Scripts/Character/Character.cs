using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [Header("Status")]
    [SerializeField] protected float _max_hp; //최대체력
    [SerializeField] protected float _current_hp; //현재체력
    [SerializeField] protected float _damage; //공격력
    [SerializeField] protected float _defense; //방어력 일단 int형
    [SerializeField] protected float _jump_force; //점프력
    [SerializeField] protected bool _is_jump; //점프중?
    [SerializeField] protected float _move_speed; //방어력 일단 int형
    [SerializeField] protected float _attack_speed; //공격 속도
    [SerializeField] protected float _attack_range; //공격 범위
    [SerializeField] protected bool _is_armor;
    [SerializeField] protected float _light_armor_percent; //방어구의 수치 ex) light:20 heavy:60
    [SerializeField] protected float _heavy_armor_percent;

    public float max_hp { get { return _max_hp; } set { _max_hp = value; } }
    public float current_hp { get { return _current_hp; } set { _current_hp = value; } }
    public float damage { get { return damage; } set { damage = value; } }
    public float defense { get { return _defense; } set { _defense = value; } }
    public float jump_force { get { return _jump_force; } set { _jump_force = value; } }
    public bool is_jump { get { return _is_jump; } set { _is_jump = value; } }
    public float move_speed { get { return _move_speed; } set { _move_speed = value; } }
    public float attack_speed { get { return _attack_speed; } set { _attack_speed = value; } }
    public float attack_range { get { return _attack_range; } set { _attack_range = value; } }
    public bool is_armor { get { return _is_armor; } set { _is_armor = value; } }
    public float light_armor_percent { get { return _light_armor_percent; } set { _light_armor_percent = value; } }
    public float heavy_armor_percent { get { return _heavy_armor_percent; } set { _heavy_armor_percent = value; } }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
