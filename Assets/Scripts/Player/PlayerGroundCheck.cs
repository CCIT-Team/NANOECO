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
            player.isGrounded = false;
            player.rigid.AddForce(Vector3.down * 98.1f);
        }
        else
        {
            player.isGrounded = true;
        }
    }
}
