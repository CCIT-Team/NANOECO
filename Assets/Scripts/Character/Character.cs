using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [Header("Status")]
    [SerializeField] protected string _name;//�̸� �����ϱ� ��� ����
    [SerializeField] protected float _max_hp; //�ִ�ü��
    [SerializeField] protected float _current_hp; //����ü��
    [SerializeField] protected float _damage; //���ݷ�
    [SerializeField] protected float _defense; //���� �ϴ� int��
    [SerializeField] protected float _move_speed; //���� �ϴ� int��
    [SerializeField] protected float _light_armor_percent; //���� ��ġ ex) light:20 heavy:60
    [SerializeField] protected float _heavy_armor_percent;// �� ���� ���� �Ƹ� �ۼ�Ʈ * 0.01 = x, x * attack = ����� ������ �϶�����
                                                          // ���� ���ݿ� �����ϸ� ������? 
    [SerializeField] protected bool _is_dead;// �������

    public string thisname { get { return _name; } set { _name = value; } }
    public float max_hp { get { return _max_hp; } set { _max_hp = value; } }
    public float current_hp { get { return _current_hp; } set { _current_hp = value; } }
    public float damage { get { return damage; } set { damage = value; } }
    public float defense { get { return _defense; } set { _defense = value; } }
    public float move_speed { get { return _move_speed; } set { _move_speed = value; } }
    public float light_armor_percent { get { return _light_armor_percent; } set { _light_armor_percent = value; } }
    public float heavy_armor_percent { get { return _heavy_armor_percent; } set { _heavy_armor_percent = value; } }
    public bool is_dead { get { return _is_dead; } set { _is_dead = value; } }

}
