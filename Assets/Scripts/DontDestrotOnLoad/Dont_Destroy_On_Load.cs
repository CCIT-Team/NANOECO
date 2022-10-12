using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 이 스크립트를 넣고 bool값 추가하면 파괴 안돼용~
/// </summary>

public class Dont_Destroy_On_Load : MonoBehaviour
{
    public bool dont_destroy_check;
    void Start()
    {
        if(dont_destroy_check)
        {
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            //
        }
    }
    //추가 될 예정
}
