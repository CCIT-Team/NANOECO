using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalMissionTrigger : MonoBehaviour
{
    public FinalMission fm;

    void OnTriggerEnter(Collider col)
    {
        fm.SpawnMonster();
    }
}
