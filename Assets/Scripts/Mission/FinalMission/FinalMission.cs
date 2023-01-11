using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalMission : MissionBase
{
    [Header("미션 세팅")]
    public int goal_parts;
    public int current_parts = 0;
    public GameObject effect;
    public int _current_parts
    {
        get { return current_parts; }
        set
        {
            current_parts = value;
            if (current_parts >= goal_parts) { Clear(); }
        }
    }

    [Space][Header("스폰 세팅")]
    public List<Transform> spawn_points;
    public List<GameObject> monster_groups;
    public float delay = 5f;
    public bool clear = false;
    public bool mission_start = false;

    void Start()
    {
        
    }

    void Update()
    {
        if(goal_parts == current_parts) { effect.SetActive(true); }
    }

    public override void Mission_Event()
    {
        StartCoroutine(SpawnMonster());
    }

    public override void Clear()
    {
        clear = true;
        ms.mission_3_clear = true;
        ms.Mission_Clear(3);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 6 && !mission_start)
        {
            Mission_Event();
            mission_start = true;
        }
    }

    public IEnumerator SpawnMonster()
    {
        for(int i = 0; i < spawn_points.Count; i++)
        {
            int j = Random.Range(0, monster_groups.Count);
            print("@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@");
            Instantiate(monster_groups[j], spawn_points[i].position, Quaternion.identity);
        }

        yield return new WaitForSeconds(delay);

        if (!clear) { StartCoroutine(SpawnMonster()); }
    }
}
