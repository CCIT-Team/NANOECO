using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
public class RangeSpread : Range
{
    GameObject spray;
    void Update()
    {
        if (Input.GetMouseButton(0) && pv.IsMine && !player.is_dead)
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
            isdelay = true;
            StartCoroutine("Reloading");
            pv.RPC("ReloadRPC", RpcTarget.AllBuffered);
        }

        if ((Input.GetMouseButtonUp(0) && pv.IsMine)||player.is_dead||isreloading)
        {
            Destroy(spray);
            spray = null;
        }

    }

    public override void Attack()
    {
        if (!isdelay)
        {
            if(spray == null)
            {
                spray = PhotonNetwork.Instantiate(bulletname, firePosition.transform.position, firePosition.transform.rotation);
                spray.transform.SetParent(firePosition.transform);
                /*GameObject chargedbullet = Instantiate(bullet);
                chargedbullet.transform.position = firePosition.transform.position;
                chargedbullet.transform.rotation = firePosition.transform.rotation;*/
            }
            StartCoroutine("AttackDelay");
        }
        p.Play();
        ammo--;
    }
}
