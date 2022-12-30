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
        Player.instance.helicopterplayerbody.SetActive(false);
    }

    void PlayerDead()
    {
        GameManager.Instance.player_count -= 1;
    }

    void PlayerRespawn()
    {
        GameManager.Instance.player_count += 1;
    }

}
