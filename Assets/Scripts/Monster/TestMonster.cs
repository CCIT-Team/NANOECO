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
    {//최대, 현재, 공격력, 방어력, 순찰속도, 쫒아 범위,쫒는 속도, 공격 속도, 사정거리, 죽었는지
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
    //공격

    //비전투

    //사망

}
