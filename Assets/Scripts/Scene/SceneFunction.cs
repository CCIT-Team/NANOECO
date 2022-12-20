using JetBrains.Annotations;
//using MoreMountains.Tools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneFunction
{
    public static GameObject fade;
    public static string game_map_name = "";

    public static Animation anim;

    

    public static void Fade_Out()
    {
        //Object_Check();
        anim.clip = fade.GetComponent<Fade>().fade_out;
        anim.Play();
    }

    public static void Fade_In()
    {
        //Object_Check();
        anim.clip = fade.GetComponent<Fade>().fade_in;
        anim.Play();
    }

    #region
    public static void Object_Check()
    {
        if (fade == null)
            fade = GameObject.Find("Fade");
        if (anim == null)
            anim = fade.GetComponent<Animation>();
    }
    #endregion
}
