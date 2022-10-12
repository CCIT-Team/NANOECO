using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneChangeButton : MonoBehaviour
{
    [Header("True : 다음씬으로 이동 , False : String씬으로 이동")]
    public bool next_scene = true;
    public string scene_name;

    Button scene_change_btn;

    

    private void Start()
    {
        scene_change_btn = this.gameObject.GetComponent<Button>();

        if(next_scene)
        {
            scene_change_btn.onClick.RemoveAllListeners();
            scene_change_btn.onClick.AddListener(SceneFunction.Fade_Out);
        }
        else
        {
            scene_change_btn.onClick.RemoveAllListeners();
            scene_change_btn.onClick.AddListener(SceneFunction.Fade_In);
        } 
            

        //if (next_scene)
        //    scene_change_btn.onClick.AddListener(Input_Scene_Change_Event);
        //else
        //    scene_change_btn.onClick.AddListener(() => Input_Scene_Change_Event(scene_name));
    }

    public void Input_Scene_Change_Event()
    {
        SceneFunction.Load_Scene();
    }

    public void Input_Scene_Change_Event(string scene_name)
    {
        SceneFunction.Load_Scene(scene_name);
    }
}
