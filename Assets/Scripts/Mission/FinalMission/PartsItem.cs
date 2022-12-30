using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartsItem : WeaponeBase
{
    public Transform player;

    void Start()
    {
        type = Type.ENONE;
    }

    private void OnTriggerEnter(Collider other)
    {

    }
}
