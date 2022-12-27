using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundCheck : MonoBehaviour
{
    public Player player;
    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject == null)
        {
            Player.instance.isGrounded = false;
        }
        else
        {
            Player.instance.isGrounded = true;
        }
    }
}
