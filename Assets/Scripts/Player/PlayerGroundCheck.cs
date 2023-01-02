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
            player.rigid.AddForce(Vector3.down * 98.1f);
            player.isGrounded = false;
        }
        else
        {
            player.isGrounded = true;
        }
    }
}
