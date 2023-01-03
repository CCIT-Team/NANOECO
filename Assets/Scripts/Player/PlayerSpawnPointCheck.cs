using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawnPointCheck : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Spawn"))
        {
            Player.instance.spawn_point = other.transform;
        }
    }
}
