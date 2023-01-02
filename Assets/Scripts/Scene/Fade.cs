//using MoreMountains.TopDownEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Fade : MonoBehaviour
{
    public Animation fade_ani;
    public AnimationClip fade_out;
    public AnimationClip fade_in;

    public void Fade_Out()
    {
        if (SceneFunction.game_map_name != "")
        {
            SceneFunction.Fade_Out();
            Debug.Log(24);
        }
    }

    public void Load_Scene()//Fade Out
    {
        if (SceneFunction.game_map_name != "")
        {
            SceneManager.LoadScene(SceneFunction.game_map_name);
            SceneFunction.game_map_name = string.Empty;
        }
        else
        {
            var current_scene_index = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(current_scene_index + 1);
        }
    }

    void Start()
    {
        // 델리게이트 체인 추가
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SceneFunction.Fade_In();
    }

    void OnDisable()
    {
        // 델리게이트 체인 제거
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
