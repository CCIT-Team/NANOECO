using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapEventDead : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
            var player = other.GetComponent<Player>();
            player.is_dead = true;
    }
}
