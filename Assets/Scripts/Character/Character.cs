using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [Header("Status")]
    [SerializeField] protected float _max_hp; //�ִ�ü��
    [SerializeField] protected float _current_hp; //����ü��
    [SerializeField] protected float _damage; //���ݷ�
    [SerializeField] protected float _defense; //���� �ϴ� int��
    [SerializeField] protected float _move_speed; //���� �ϴ� int��
    [SerializeField] protected float _light_armor_percent; //���� ��ġ ex) light:20 heavy:60
    [SerializeField] protected float _heavy_armor_percent;// �� ���� ���� �Ƹ� �ۼ�Ʈ * 0.01

    public float max_hp { get { return _max_hp; } set { _max_hp = value; } }
    public float current_hp { get { return _current_hp; } set { _current_hp = value; } }
    public float damage { get { return _damage; } set { _damage = value; } }
    public float defense { get { return _defense; } set { _defense = value; } }
    public float move_speed { get { return _move_speed; } set { _move_speed = value; } }
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
