using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Totem : MonoBehaviourPunCallbacks
{
    public PhotonView pv;
    public int maxeffectcount = 5;
    public int effectcount = 0;
    public float effectamount = 10;
    SphereCollider effectaround;
    public bool iseffect = false;
    

    void Start()
    {
        effectaround = GetComponent<SphereCollider>();
        new WaitForSecondsRealtime(1);
        StartCoroutine("Effectdelay");
    }

    private void Update()
    {
        if(effectcount >= maxeffectcount)
        {
            pv.RPC("DestroyRPC", RpcTarget.AllBuffered);
            Destroy(gameObject);
        }

        if(!iseffect)
        {
            iseffect = true;
            effectaround.enabled = true;
            effectcount++;
            StartCoroutine("Effectdelay");
            StartCoroutine("Effectcooldown");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 6)
        {
            PhotonTestPlayer chr;
            chr = other.gameObject.GetComponent<PhotonTestPlayer>();
            Mathf.Clamp(chr.current_hp, 0, chr.max_hp);
            chr.current_hp += effectamount;
        }
    }

    IEnumerator Effectdelay()
    {
        yield return new WaitForSecondsRealtime(1);
        effectaround.enabled = false; 
    }

    IEnumerator Effectcooldown()
    {
        yield return new WaitForSecondsRealtime(5.5f);
        iseffect = false;
    }

    [PunRPC]
    void DestroyRPC() => Destroy(gameObject);
}
