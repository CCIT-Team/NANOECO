using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class ItemF : Bomber
{
    public override void Useitem()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, 100f, layerMask)&& Vector3.Distance(hit.point,player.transform.position) <= 30)
        {
            target = hit.point;
            PhotonNetwork.Instantiate(itemprefab.name, target, this.transform.rotation);
            count--;
            StartCoroutine("Cooling");
        }
        else
            return;
    }
}