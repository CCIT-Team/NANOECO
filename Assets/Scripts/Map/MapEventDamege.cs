using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapEventDamege : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        //±×¸±
        if (other.gameObject.layer == 6)
        {
            var player = other.GetComponent<Player>();
            player.current_hp -= 1f;
        }
    }
}
