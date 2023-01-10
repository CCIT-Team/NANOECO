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
    public bool ride = false;

    public List<GameObject> player_list;

    void Update()
    {
        rotor.eulerAngles = new Vector3(rotor.eulerAngles.x, rotor.eulerAngles.y + Time.deltaTime * rotor_speed, rotor.eulerAngles.z);
        tail_rotor.eulerAngles = new Vector3(tail_rotor.eulerAngles.x, tail_rotor.eulerAngles.y, tail_rotor.eulerAngles.z + Time.deltaTime * tail_rotor_speed);
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.layer == 6)
        {
            if(!ride)
            {
                current_player_count++;
                Ride_Helicopter();
                player_list.Add(col.gameObject);
            }
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.layer == 6)
        {
            if(!ride)
            {
                current_player_count--;
                player_list.Remove(col.gameObject);
            }
        }
    }

    void Ride_Helicopter()
    {
        if(current_player_count == 4)
        {
            ride = true;
            helicopter.SetActive(true);
        }
    }

    public void Ride_Player()
    {
        for(int i = 0; i < player_list.Count; i++)
        {
            player_list[i].transform.SetParent(heli_player);
            player_list[i].transform.localPosition = Vector3.zero;
        }
    }

    public void Arrived_Player()
    {
        for (int i = 0; i < player_list.Count; i++)
        {
            player_list[i].transform.localPosition = Vector3.zero;
            //player_list[i].transform.DetachChildren();
        }
        heli_player.transform.DetachChildren();
        player_list.Clear();
    }
}
