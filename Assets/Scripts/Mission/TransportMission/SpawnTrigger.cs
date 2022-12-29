using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTrigger : MonoBehaviour
{
    public TransportMission mb;
    public List<Transform> spawn_points;
    bool spawned = false;

    void OnTriggerEnter(Collider col)
    {
        if(!spawned)
        {
            print(123);
            mb.Spawn_Monster(spawn_points);
            spawned = true;
        }
    }
}
