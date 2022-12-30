using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartsItem : WeaponeBase
{
    public Transform player;
    public FinalMission fm;

    void Start()
    {
        type = Type.ENONE;
    }

    private void OnTriggerEnter(Collider other)
    {
        fm._current_parts++;
        Destroy(gameObject);
    }
}
