using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartsItem : WeaponeBase
{
    public FinalMission fm;
    public bool handed;

    void Start()
    {
        type = Type.ENONE;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(!handed && other.gameObject.layer == 10)
        {
            fm._current_parts++;
            Destroy(gameObject);
        }
    }
}
