using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fade_Out : MonoBehaviour
{
    public GameObject g;
    public Color color;
    public Material mat;
    // Start is called before the first frame update
    void Start()
    {
        //var test = g.GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        var test = g.GetComponent<Renderer>();
        if (g.activeSelf == true)
        {
            for (float i = 1.0f; i >= 0.0f; i -= 0.01f)
            {
                color = new(1f, 1f, 1f, i);
                test.material.SetColor("_Color", color);
            }
        }
    }
}
