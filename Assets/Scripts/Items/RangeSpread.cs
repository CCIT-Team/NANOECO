using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
public class RangeSpread : Range
{

    ParticleSystem p;
    public int spreadamunt = 15;
    

    public override void Start()
    {
        base.Start();
        //bullet.GetComponent<Bullet>().knockback = knockback;
        p = firePosition.GetComponent<ParticleSystem>();
    }

    void Update()
    {
        if (Input.GetMouseButton(0) && pv.IsMine && player.is_dead)
        {
            switch (ammo)
            {
                case 0:
                    if(!isdelay)
                    StartCoroutine("Reloading");
                    pv.RPC("ReloadRPC", RpcTarget.AllBuffered);
                    break;
                default:
                    Attack();
                    isdelay = true;
                    pv.RPC("ShotRPC", RpcTarget.AllBuffered);
                    break;
            }
        }
        if (Input.GetKeyDown(KeyCode.R) && pv.IsMine && !player.is_dead && !isdelay)
        {
            StartCoroutine("Reloading");
            pv.RPC("ReloadRPC", RpcTarget.AllBuffered);
        }
    }

    public override void Attack()
    {
        if (!isdelay)
        {
            PhotonNetwork.Instantiate("SprayBullet", firePosition.transform.position, firePosition.transform.rotation);
            /*GameObject chargedbullet = Instantiate(bullet);
            chargedbullet.transform.position = firePosition.transform.position;
            chargedbullet.transform.rotation = firePosition.transform.rotation;*/
            StartCoroutine("AttackDelay");
        }
        p.Emit(spreadamunt);
        ammo--;
    }

    IEnumerator Reloading()
    {
        isdelay = true;
        yield return new WaitForSecondsRealtime(2);
        ammo = maxAmmo;
        isdelay = false;
    }
}
