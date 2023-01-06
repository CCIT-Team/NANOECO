using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MissionSystem : MonoBehaviour
{
    [Header("파괴 미션 클리어")] public bool mission_0_clear = false;
    [Header("호송 미션 클리어")] public bool mission_1_clear = false;
    [Header("방어 미션 클리어")] public bool mission_2_clear = false;
    [Header("수리 미션 클리어")] public bool mission_3_clear = false;

    public InGameUI ig;
    public List<MissionBase> mb;

    public void Mission_Box_Update(List<MissionBox> mission_box_list)
    {
        for(int i = 0; i < mission_box_list.Count; i++)
        {
            mission_box_list[i].mission_text.text = mb[i].mission_name;
        }
    }

    public void Mission_Clear(int i)
    {
        ig.mission_box_list[i].anime.Play();
    }
}
