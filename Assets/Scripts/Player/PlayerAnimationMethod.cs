using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationMethod : MonoBehaviour
{
    public Player player;

    void RideHelicopter()
    {
        player.helicopterplayerbody.transform.localPosition = new Vector3(0, 0, 0);
        player.helicopterplayerbody.SetActive(false);
        player.helicopterAni.SetBool("Respawn", true);
    }
    void UnRideHelicopter()
    {
        Debug.Log("Çï±â ³»¸®±â");
        player.is_dead = false;
        player.isunrideheli = true;
        player.helicopterplayerbody.SetActive(true);
        player.helicopterrope.transform.DetachChildren();
        player.helicopterplayerbody.transform.parent = player.originPlayer.transform;
       // player.helicopterplayerbody.transform.localPosition = new Vector3(0, 0, 0);
    }

    void PlayerDead()
    {
        GameManager.Instance.player_count -= 1;
    }

    void PlayerRespawn()
    {
        GameManager.Instance.player_count += 1;
    }

    void HelicopterEnd()
    {
        Player.instance.helicopter.GetComponent<Animator>().SetBool("HliEnd", true);
        player.helicopter.SetActive(false);
    }

}
