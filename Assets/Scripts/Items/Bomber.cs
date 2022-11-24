using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Bomber : ItemControler
{
    int layerMask;
    void Start()
    {
        pv = PhotonTestPlayer.instance.pv;
        count = maxcount;
        layerMask  = 1 << LayerMask.NameToLayer("Ground");
    }
    public override void Useitem()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, layerMask))
        {
            target = hit.point;
        }
        else
            return;
        /*useditem = Instantiate(itemprefab);
        useditem.transform.position = this.transform.position;*/
        useditem = PhotonNetwork.Instantiate("Bomb", this.transform.position, this.transform.rotation);
        useditem.GetComponent<Bomb>().target = target;
        useditem.SetActive(true);

        count--;
    }
}
