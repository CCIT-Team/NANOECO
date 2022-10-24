using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Setter : ItemControler
{
    void Start()
    {
        count = maxcount;
    }

    public override void Useitem()
    {
        useditem = Instantiate(itemprefab);
        useditem.transform.position = this.transform.position;
        count--;
    }
}
