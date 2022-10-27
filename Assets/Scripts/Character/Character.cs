using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [Header("Status")]
    [SerializeField] private string _name;//�̸� �����ϱ� ��� ����
    [SerializeField] private float _max_hp; //�ִ�ü��
    [SerializeField] private float _current_hp; //����ü��
    [SerializeField] private float _damage; //���ݷ�
    [SerializeField] private float _defense; //���� �ϴ� int��
    [SerializeField] private float _light_armor_percent; //���� ��ġ ex) light:20 heavy:60
    [SerializeField] private float _heavy_armor_percent;// �� ���� ���� �Ƹ� �ۼ�Ʈ * 0.01 = x, x * attack = ����� ������ �϶�����
                                                          // ���� ���ݿ� �����ϸ� ������? 
    [SerializeField] private bool _is_dead;// �������

    public string thisname { get { return _name; } set { _name = value; } }
    public float max_hp { get { return _max_hp; } set { _max_hp = value; } }
    public float current_hp { get { return _current_hp; } set { _current_hp = _max_hp; } }
    public float damage { get { return _damage; } set { _damage = value; } }
    public float defense { get { return _defense; } set { _defense = value; } }
    public float light_armor_percent { get { return _light_armor_percent; } set { _light_armor_percent = value; } }
    public float heavy_armor_percent { get { return _heavy_armor_percent; } set { _heavy_armor_percent = value; } }
    public bool is_dead { get { return _is_dead; } set { if (_current_hp <= 0) _is_dead = false; else { _is_dead = true; } } }

}
