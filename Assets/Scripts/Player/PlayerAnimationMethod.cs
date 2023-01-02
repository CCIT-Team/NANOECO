using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationMethod : MonoBehaviour
{
    void RideHelicopter()
    {
        Player.instance.helicopterplayerbody.SetActive(false);
    }
    void UnRideHelicopter()
    {
        Player.instance.helicopterplayerbody.SetActive(true);
    }

    void PlayerDead()
    {
        GameManager.Instance.player_count -= 1;
    }

    void PlayerRespawn()
    {
        GameManager.Instance.player_count += 1;
        Player.instance.helicopter.GetComponent<Animator>().SetBool("Respawn", true);
    }

    void HelicopterEnd()
    {
        Player.instance.helicopter.GetComponent<Animator>().SetBool("HliEnd", true);  
    }

}
