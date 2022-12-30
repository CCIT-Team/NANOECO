using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationMethod : MonoBehaviour
{
    void RideHelicopter()
    {
        Player.instance.helicopterplayerbody.SetActive(false);
    }
}
