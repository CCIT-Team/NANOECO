using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyMission : MissionBase
{
    public List<DestroyTarget> target = new List<DestroyTarget>();
    public List<GameObject> monster_group = new List<GameObject>();
    public List<GameObject> spawn_point = new List<GameObject>();
    public float wave_time;
    public bool started = false;

    public float hp
    {
        get { return Target_Hp(); }
    }

    void OnTriggerEnter(Collider other)
    {
        if(!started && other.gameObject.layer == 6)
        {
            Mission_Event();
            started = true;
        }
    }

    public override void Clear()
    {
        ms.mission_1_clear = true;
    }

    public override void Mission_Event()
    {
        StartCoroutine(Spawn_Monster());
    }

    IEnumerator Spawn_Monster()
    {
        int i = Random.Range(0, monster_group.Count);
        Instantiate(monster_group[i], spawn_point[i].transform.position, Quaternion.identity);

        yield return new WaitForSeconds(wave_time);
        StartCoroutine(Spawn_Monster());
    }

    float Target_Hp()
    {
        float hp = 0;
        foreach (var g in target)
        {
            hp += g._hp;
        }
        return hp;
    }
}