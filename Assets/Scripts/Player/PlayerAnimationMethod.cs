using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationMethod : MonoBehaviour
{
    void RideHelicopter()
    {
        Player.instance.helicopterplayerbody.transform.localPosition = new Vector3(0, 0, 0);
        Player.instance.helicopterplayerbody.SetActive(false);
        Player.instance.helicopter.GetComponent<Animator>().SetBool("Respawn", true);
    }
    void UnRideHelicopter()
    {
        Player.instance.helicopterplayerbody.SetActive(true);
        Player.instance.helicopterplayerbody.transform.parent = Player.instance.originPlayer.transform;
        Player.instance.helicopterplayerbody.transform.localPosition = new Vector3(0, 0, 0);
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
    }

}
