using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public SpawnPoint sp;
    public int num;

    void OnTriggerEnter(Collider col)
    {
        sp.current_spawn_point = num;
    }
}
