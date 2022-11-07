using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PlayerCamera : MonoBehaviourPunCallbacks
{
    public Transform player;
    public float smooth_speed = 0.125f;
    public Vector3 offset;

    private void Start()
    {
        player = PhotonTestPlayer.instance.nickname.gameObject.transform;
    }
    void FixedUpdate()
    {
        if (player != null && PhotonTestPlayer.instance.pv.IsMine)
        {
            Vector3 desired_position = player.position + offset;
            Vector3 smoothed_position = Vector3.Lerp(transform.position, desired_position, smooth_speed);
            transform.position = smoothed_position;
        }
    }
}
