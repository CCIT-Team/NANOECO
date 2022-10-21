using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sington : MonoBehaviour
{
    public Sington instance = null;

    private void Awake()
    {
        if(null == instance)
        {
            instance = this;
        }
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
