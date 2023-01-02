using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelicopterEvent : MonoBehaviour
{
    public Animator anime;
    public int current_player_count = 0;
    public GameObject helicopter;

    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.layer == 6)
        {
            current_player_count++;
            Ride_Helicopter();
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.layer == 6)
        {
            current_player_count--;
        }
    }

    void Ride_Helicopter()
    {
        if(current_player_count == GameManager.Instance.player_count)
        {
            helicopter.SetActive(true);
        }
    }

    void Ride_Player()
    {
        Player.instance.gameObject.SetActive(false);
    }
}
