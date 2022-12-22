using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Range : WeaponeBase
{
    public int maxAmmo = 0;    //ÃÖ´ë Åº¼ö
    public int ammo = 0; //ÇöÀç Åº¼ö
    public GameObject firePosition;
    public Bullet bullet;
    string bulletname;
    public bool explosion = false;
    float reloadtime = 0;

    void Update()
    {
        if (Input.GetMouseButton(0)&& !isdelay && pv.IsMine)
        {
            switch (ammo)
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
        chargedbullet.transform.rotation = firePosition.transform.rotation;
        */
        PhotonNetwork.Instantiate(bulletname, firePosition.transform.position, firePosition.transform.rotation);
        ammo--;
    }

    public override void PreSetting()
    {
        type = Type.ERANGE;
        skil = pv.GetComponent<PhotonTestPlayer>().skil_num;
        switch (skil)
        {
            case (0):
                maxAmmo = Mathf.CeilToInt((float)maxAmmo * 1.3f);
                break;
            case (1):
                attackspeed *= 1.25f;
                break;
            case (2):
                break;
            case (3):
                damage *= 1.5f;
                break;
            case (4):
                break;
            case (5):
                break;
        }
        ammo = maxAmmo;
        bullet.damage = damage;
        bullet.knockback = knockback;
        bullet.explosive = explosion;
        bulletname = bullet.gameObject.name;
    }

    IEnumerator Reloading()
    {
        isdelay = true;
        yield return new WaitForSecondsRealtime(2);
        ammo = maxAmmo;
        isdelay = false;
    }
}
