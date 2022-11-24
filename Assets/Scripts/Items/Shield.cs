using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : WeaponeBase
{
    public Material a;
    // Start is called before the first frame update
    void Start()
    {
        //pv = PhotonTestPlayer.instance.pv;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space)) 
            a.color += new Color(0.1f, -0.01f, 0, 0);
    }
}
