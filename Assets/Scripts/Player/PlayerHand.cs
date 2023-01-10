using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHand : MonoBehaviour
{
    public Player player;

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.layer == 12 && Input.GetKey(KeyCode.E) && !player.is_usehand)
        {
            player.pi = col.transform.GetComponent<PartsItem>();
            player.is_usehand = true;
            col.transform.parent = player.hand.transform;
            player.pi.handed = true;
        }
    }
}
