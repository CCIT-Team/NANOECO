using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �� ��ũ��Ʈ�� �ְ� bool�� �߰��ϸ� �ı� �ȵſ�~
/// </summary>

public class DontDestroyOnLoad : MonoBehaviour
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
    //�߰� �� ����
}
