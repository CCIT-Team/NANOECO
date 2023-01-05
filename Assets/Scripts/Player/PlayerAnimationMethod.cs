using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationMethod : MonoBehaviour
{
    public Animator heliAni;
    void RideHelicopter()
    {
        heliAni.SetBool("Respawn", true);
        Player.instance.helicopterplayerbody.transform.localPosition = new Vector3(0, 0, 0);
        Player.instance.helicopterplayerbody.SetActive(false);
    }
    void UnRideHelicopter()
    {
        Debug.Log("��� ������");
        //Player.instance.is_dead = false;
        //Player.instance.helicopterrope.transform.DetachChildren();
        Player.instance.isunrideheli = true;
       // Debug.Log(Player.instance.isunrideheli + "�÷��̾� ���ȳ�?");
        Player.instance.helicopterplayerbody.SetActive(true);
       // Player.instance.helicopterrope.transform.DetachChildren();
        //Player.instance.helicopterplayerbody.transform.parent = Player.instance.originPlayer.transform;
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
        Player.instance.helicopter.SetActive(false);
    }
}
