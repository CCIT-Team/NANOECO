using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMonster : MonsterBase
{
    // Start is called before the first frame update
    void Start()
    {

        StartCoroutine(CheckState());
    }

    // Update is called once per frame
    void Update()
    { 
        if(!isdead)
        {

            
        }
    }

    IEnumerator CheckState()
    {//�ִ�, ����, ���ݷ�, ����, �����ӵ�, �i�� ����,�i�� �ӵ�, ���� �ӵ�, �����Ÿ�, �׾�����
        if (!isdead)
        {
            yield return new WaitForSeconds(5f);
            _monster_max_hp = 100;
            _monster_hp = 100;
            _damage = 5;
            _defense = 5;
            _parol_speed = 15f;
            _chase_dist = 20f;
            _chase_speed = 20f;
            _attack_speed = 5f;
            _attack_dist = 10f;
        }
    }
    //����

    //������

    //���

}
