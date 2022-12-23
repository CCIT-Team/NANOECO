using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginManager : MonoBehaviour
{
    [SerializeField]
    private GameObject loading_canvas;
    void Start()
    {
        SceneFunction.loading_canvas = this.loading_canvas;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
