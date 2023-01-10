using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHand : MonoBehaviour
{
    public Player player;

    private void Update()
    {
        if(Input.GetKey(KeyCode.E) && player.pi != null)
        {
            player.is_usehand = true;
            player.pi.transform.parent = player.hand.transform;
            player.pi.handed = true;
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.layer == 12 && !player.is_usehand)
        {
            player.pi = col.transform.GetComponent<PartsItem>();
      
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 12 && player.is_usehand)
        {
            player.pi = null;
            player.is_usehand = false;

        }
    }
}
