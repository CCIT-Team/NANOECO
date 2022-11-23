using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MissionBase : MonoBehaviour
{
    [Header("미션 데이터")]
    public string mission_name;
    public string mission_info;

    public MissionSystem ms;

    public abstract void Mission_Event();

    public abstract void Clear();
}
