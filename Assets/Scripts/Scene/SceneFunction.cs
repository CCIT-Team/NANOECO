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

    /// <summary>
    /// 다음 INDEX 씬으로 이동
    /// </summary>
    /// 
    public static void Load_Scene()
    {
        var current_scene_number = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(current_scene_number + 1);
    }

    public static void Load_Scene(string scene_name)
    {
        SceneManager.LoadScene(scene_name);
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
