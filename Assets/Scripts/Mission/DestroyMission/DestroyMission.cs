using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyMission : MissionBase
{
    public MissionSystem ms;

    public DestroyTarget target;

    public override void Clear()
    {
        ms.mission_1_clear = true;
    }

    public override void Mission_Event()
    {

    }

    //void 
}