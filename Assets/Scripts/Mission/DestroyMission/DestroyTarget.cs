using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyTarget : MonoBehaviour
{
    public DestroyMission dm;
    public float hp;
    public float _hp
    {
        get { return hp; }
        set
        {
            hp = value;
            if(hp <= 0) { Destroy_Object(); }

        }
    }

    void Destroy_Object()
    {
        int i = dm.target.IndexOf(this);
        dm.target.RemoveAt(i);

        if(dm.target.Count == 0)
        {
            dm.StopAllCoroutines();
            dm.Clear();
        }

        Destroy(gameObject);
    }
}
