using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomber : ItemControler
{
    int layerMask;
    void Start()
    {
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
        useditem = Instantiate(itemprefab);
        useditem.transform.position = this.transform.position;
        useditem.GetComponent<Bomb>().target = target;
        useditem.SetActive(true);

        count--;
    }
}