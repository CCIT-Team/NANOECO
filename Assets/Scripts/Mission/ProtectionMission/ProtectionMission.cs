using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtectionMission : MissionBase
{
    public ProtectionTarget target;

    public List<GameObject> monster_group = new List<GameObject>();
    public int wave_count;
    public float wave_time;

    public override void Clear()
    {
        ms.mission_2_clear = true;
    }

    public override void Mission_Event()
    {
        StartCoroutine(Monster_Wave());
    }

    IEnumerator Monster_Wave()
    {
        wave_count--;
        int i = Random.Range(0, monster_group.Count);
        yield return new WaitForSeconds(wave_time);
        if(wave_count != 0 && target.hp > 0) { StartCoroutine(Monster_Wave()); }
        else if(wave_count == 0 && target.hp > 0) { Clear(); }
    }
}