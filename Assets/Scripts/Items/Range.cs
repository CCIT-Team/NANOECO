using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Range : WeaponeBase
{
    public GameObject bullet;
    //List<GameObject> bullets;
    void Start()
    {
        //bullets = new List<GameObject>();
        type = Type.ERANGE;
        //GameObject bulletobject;
        /*for(int i=0;i<ammo+20; i++)
        {
            bulletobject = Instantiate(bullet);
            bullets.Add(bulletobject);
            bulletobject.SetActive(false);
        }*/
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Q)&&!isdelay)
        {
            isdelay = true;
            Attack();
            StartCoroutine("AttackDelay");
        }
            
    }

    new void Attack()
    {
        GameObject chargedbullet = Instantiate(bullet);
        //GameObject chargedbullet = bullets[0];
        chargedbullet.transform.position = firePosition.transform.position;
        chargedbullet.transform.rotation = firePosition.transform.rotation;
        //chargedbullet.SetActive(true);
        //bullets.Remove(chargedbullet);

    }
}
