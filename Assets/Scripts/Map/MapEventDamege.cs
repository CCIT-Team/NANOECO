using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapEventDamege : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
            var player = other.GetComponent<Player>();
            player.current_hp -= 1f;
    }
}
