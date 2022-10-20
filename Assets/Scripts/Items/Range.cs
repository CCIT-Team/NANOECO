using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Range : WeaponeBase
{

    public int ammo = 0;    //ÃÖ´ë Åº¼ö
    public int currentammo = 0; //ÇöÀç Åº¼ö
    public GameObject firePosition;
    public GameObject bullet;
    //List<GameObject> bullets;
    void Start()
    {
        //bullets = new List<GameObject>();
        type = Type.ERANGE;
        currentammo = ammo;
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
        if (Input.GetMouseButton(0)&& !isdelay)
        {
            switch (currentammo)
            {
                case 0:
                    Reload();
                    break;
                default :
                    isdelay = true;
                    Attack();
                    StartCoroutine("AttackDelay");
                    break;
            }
        }
        if(Input.GetKeyDown(KeyCode.R) && !isdelay)
        {
            Reload();
           
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
        currentammo--;
    }

    void Reload()
    {
        isdelay = true;
        StartCoroutine("Reloading");
    }

    IEnumerator Reloading()
    {
        yield return new WaitForSecondsRealtime(2);
        currentammo = ammo;
        isdelay = false;
    }
}
