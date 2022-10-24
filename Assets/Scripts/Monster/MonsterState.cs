using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CurrentState
{
    EIDLE = 0,
    EPATROL,
    ECHASE,
    EATTACK, //쫒는 속도와 공격 속도 공격력
    ESKILL
}
