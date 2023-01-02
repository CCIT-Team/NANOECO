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
    int skill;
    bool isreloading = false;
    public override void Start()
    {
        base.Start();
        skill = pv.GetComponent<Player>().skil_num;
        switch (skill)
        {
            default:
                break;
            case 0:
                maxAmmo = Mathf.CeilToInt((float)maxAmmo * 1.5f);
                break;
            case 1:
                attackspeed *= 1.3f;
                break;
            case 3:
                damage *= 1.5f;
                break;
        }
        ammo = maxAmmo;
        bullet.damage = damage;
        //bullet.knockback = knockback;
        bullet.explosive = explosion;
        bulletname = bullet.gameObject.name;
    }

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
        
        GameObject chargedbullet = Instantiate(bullet.gameObject);
        chargedbullet.transform.position = firePosition.transform.position;
        chargedbullet.transform.rotation = firePosition.transform.rotation;
        
        //PhotonNetwork.Instantiate(bulletname, firePosition.transform.position, firePosition.transform.rotation);
        ammo--;
    }

    IEnumerator Reloading()
    {
        isdelay = true;
        isreloading = true;
        yield return new WaitForSecondsRealtime(2);
        ammo = maxAmmo;
        isdelay = false;
        isreloading = false;
    }

    public override void OnEnable()
    {
        PhotonNetwork.AddCallbackTarget(this);
        if (isreloading)
            StartCoroutine("Reloading");
        else if (isdelay)
            StartCoroutine("AttackDelay");
    }
}
