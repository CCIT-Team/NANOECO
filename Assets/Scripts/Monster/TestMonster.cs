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
        if(!is_dead)
        {
            
            
        }
    }

    IEnumerator CheckState()
    {//최대, 현재, 공격력, 방어력, 순찰속도, 쫒아 범위,쫒는 속도, 공격 속도, 사정거리, 죽었는지
        if (!is_dead)
        {
            yield return new WaitForSeconds(5f);
            _max_hp = 100;
            _current_hp = 100;
            _damage = 5;
            _defense = 5;
            _patrol_speed = 15f;
            _patrol_dist = 100f;
            _chase_dist = 20f;
            _chase_speed = 20f;
            _attack_speed = 5f;
            _attack_dist = 10f;
        }
    }
    //공격

    //비전투

    //사망

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _patrol_dist);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, _attack_dist);
    }

#endif
}
