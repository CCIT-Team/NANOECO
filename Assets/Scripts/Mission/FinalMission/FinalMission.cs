using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalMission : MissionBase
{
    [Header("�̼� ����")]
    public int goal_parts;
    public int current_parts
    {
        get { return current_parts; }
        set
        {
            current_parts = value;
            if (current_parts == goal_parts)
                Clear();
        }
    }

    [Space][Header("���� ����")]
    public List<Transform> spawn_points;
    public List<GameObject> monster_groups;
    public float delay = 5f;
    public bool clear = false;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public override void Mission_Event()
    {
        SpawnMonster();
    }

    public override void Clear()
    {
        clear = true;
    }

    public IEnumerator SpawnMonster()
    {
        for(int i = 0; i < spawn_points.Count; i++)
        {
            int j = Random.Range(0, monster_groups.Count);
            Instantiate(monster_groups[j], spawn_points[i].position, Quaternion.identity);
        }

        yield return new WaitForSeconds(delay);

        if (!clear) { SpawnMonster(); }
    }
}
