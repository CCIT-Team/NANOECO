using MoreMountains.TopDownEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Fade : MonoBehaviour
{
    public Animation fade_ani;
    public AnimationClip fade_out;
    public AnimationClip fade_in;

    public void Load_Scene()
    {
        SceneFunction.Load_Scene();
    }

    void Start()
    {
        // ��������Ʈ ü�� �߰�
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SceneFunction.Fade_In();
    }

    void OnDisable()
    {
        // ��������Ʈ ü�� ����
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
