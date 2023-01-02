using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelicopterEvent : MonoBehaviour
{
    public Animator anime;
    public int current_player_count = 0;
    public GameObject helicopter;
    public Transform heli_player;

    [Space]
    [Header("Çï±â")]
    public Transform rotor;
    public Transform tail_rotor;
    public float rotor_speed;
    public float tail_rotor_speed;

    void Update()
    {
        rotor.eulerAngles = new Vector3(rotor.eulerAngles.x, rotor.eulerAngles.y + Time.deltaTime * rotor_speed, rotor.eulerAngles.z);
        tail_rotor.eulerAngles = new Vector3(tail_rotor.eulerAngles.x, tail_rotor.eulerAngles.y, tail_rotor.eulerAngles.z + Time.deltaTime * tail_rotor_speed);
    }

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

    public void Ride_Player()
    {
        Player.instance.transform.SetParent(heli_player);
    }

    public void Arrived_Player()
    {
        Player.instance.transform.SetParent(null);
    }
}
