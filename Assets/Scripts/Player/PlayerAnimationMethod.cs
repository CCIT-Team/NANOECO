using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationMethod : MonoBehaviour
{
    public Animator heliAni;
    void RideHelicopter()
    {
        heliAni.SetBool("Respawn", true);
        NaNoPlayer.instance.helicopterplayerbody.transform.localPosition = new Vector3(0, 0, 0);
        NaNoPlayer.instance.helicopterplayerbody.SetActive(false);
    }
    void UnRideHelicopter()
    {
        NaNoPlayer.instance.isunrideheli = true;
        NaNoPlayer.instance.helicopterplayerbody.SetActive(true);
        NaNoPlayer.instance.helicopterrope.transform.DetachChildren();
        NaNoPlayer.instance.helicopterplayerbody.transform.parent = NaNoPlayer.instance.originPlayer.transform;
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
        heliAni.SetBool("HliEnd", true);
        NaNoPlayer.instance.helicopter.SetActive(false);
    }
}
