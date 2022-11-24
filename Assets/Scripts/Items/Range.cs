using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Range : WeaponeBase
{

    public int ammo = 0;    //ÃÖ´ë Åº¼ö
    public int currentammo = 0; //ÇöÀç Åº¼ö
    public GameObject firePosition;
    public GameObject bullet;
    string bulletname;
    void Start()
    {
        pv = PhotonTestPlayer.instance.pv;
        currentammo = ammo;
        bullet.GetComponent<Bullet>().damage = damage;
        bullet.GetComponent<Bullet>().knockback = knockback;
        bulletname = bullet.name;
    }

    void Update()
    {
        if (Input.GetMouseButton(0)&& !isdelay && pv.IsMine)
        {
            switch (currentammo)
            {
                case 0:
                    StartCoroutine("Reloading");
                    break;
                default :
                    isdelay = true;
                    Attack();
                    StartCoroutine("AttackDelay");
                    break;
            }
        }
        if(Input.GetKeyDown(KeyCode.R) && !isdelay && pv.IsMine)
            StartCoroutine("Reloading");
    }

    public override void Attack()
    {
        /*
        GameObject chargedbullet = Instantiate(bullet);
        chargedbullet.transform.position = firePosition.transform.position;
        chargedbullet.transform.rotation = firePosition.transform.rotation;*/
        PhotonNetwork.Instantiate(bulletname, firePosition.transform.position, firePosition.transform.rotation);
        currentammo--;
    }

    IEnumerator Reloading()
    {
        isdelay = true;
        yield return new WaitForSecondsRealtime(2);
        currentammo = ammo;
        isdelay = false;
    }
}
