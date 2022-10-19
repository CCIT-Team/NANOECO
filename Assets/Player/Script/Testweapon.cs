using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testweapon : MonoBehaviour
{
    public GameObject bullet;
    public GameObject firePosition;
    public GameObject player;
    void Start()
    {
        
    }

    
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject bullets = Instantiate(bullet);     
            bullet.transform.position = firePosition.transform.position;
            bullet.transform.rotation = player.transform.rotation;
        }
    }
}
