using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fade_Out : MonoBehaviour
{
    public Color color;
    public Material mat;
    public float a = 1f;

    private void Start()
    {
        StartCoroutine(test());
    }

    IEnumerator test()
    {
        while (a > 0f)
        {
            a -= 0.01f;
            yield return new WaitForSeconds(0.01f);
            color = new(0.29f, 0.74f, 0.24f, a);
            mat.color = color;
        }
        Destroy(gameObject, 1f);
    }

}
