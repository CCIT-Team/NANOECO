using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHand : MonoBehaviour
{
    public Player player;
    PartsItem pi;
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.layer == 12 && Input.GetKey(KeyCode.E) && !player.is_usehand)
        {
            player.is_usehand = true;
            col.transform.parent = player.hand.transform;
            pi = col.transform.GetComponent<PartsItem>();
            pi.handed = true;
        }
    }
}
