using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [Header("Status")]
    [SerializeField] private string _name;//이름 설정하기 없어도 무관
    [SerializeField] private float _max_hp; //최대체력
    [SerializeField] private float _current_hp; //현재체력
    [SerializeField] private float _damage; //공격력
    [SerializeField] private float _defense; //방어력 일단 int형
    [SerializeField] private float _light_armor_percent; //방어구의 수치 ex) light:20 heavy:60
    [SerializeField] private float _heavy_armor_percent;// 방어구 배율 계산식 아머 퍼센트 * 0.01 = x, x * attack = 방어구대비 데미지 하락으로
                                                          // 몬스터 공격에 구현하면 좋을듯? 
    [SerializeField] private bool _is_dead;// 사망여부

    public string thisname { get { return _name; } set { _name = value; } }
    public float max_hp { get { return _max_hp; } set { _max_hp = value; } }
    public float current_hp { get { return _current_hp; } set { _current_hp = _max_hp; } }
    public float damage { get { return _damage; } set { _damage = value; } }
    public float defense { get { return _defense; } set { _defense = value; } }
    public float light_armor_percent { get { return _light_armor_percent; } set { _light_armor_percent = value; } }
    public float heavy_armor_percent { get { return _heavy_armor_percent; } set { _heavy_armor_percent = value; } }
    public bool is_dead { get { return _is_dead; } set { if (_current_hp <= 0) _is_dead = false; else { _is_dead = true; } } }

}
