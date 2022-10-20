using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public Transform player;

    public float smooth_speed = 0.125f;
    public Vector3 offset;

    void FixedUpdate()
    {
        if(player != null)
        {
            Vector3 desired_position = player.position + offset;
            Vector3 smoothed_position = Vector3.Lerp(transform.position, desired_position, smooth_speed);
            transform.position = smoothed_position;
        }
    }
}
