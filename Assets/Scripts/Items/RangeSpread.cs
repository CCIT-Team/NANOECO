using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeSpread : Range
{

    ParticleSystem p;
    public int spreadamunt = 15;
    

    void Start()
    {
        pv = PhotonTestPlayer.instance.pv;
        currentammo = ammo;
        bullet.GetComponent<Bullet>().damage = damage;
        bullet.GetComponent<Bullet>().knockback = knockback;
        p = firePosition.GetComponent<ParticleSystem>();
    }

    void Update()
    {
        if (Input.GetMouseButton(0) && pv.IsMine)
        {
            switch (currentammo)
            {
                case 0:
                    if(!isdelay)
                        StartCoroutine("Reloading");
                    break;
                default:
                    Attack();
                    isdelay = true;
                    break;
            }
        }
        if (Input.GetKeyDown(KeyCode.R) && !isdelay && pv.IsMine)
            StartCoroutine("Reloading");
    }

    public override void Attack()
    {
        if (!isdelay)
        {
            GameObject chargedbullet = Instantiate(bullet);
            chargedbullet.transform.position = firePosition.transform.position;
            chargedbullet.transform.rotation = firePosition.transform.rotation;
            StartCoroutine("AttackDelay");
        }
        p.Emit(spreadamunt);
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
