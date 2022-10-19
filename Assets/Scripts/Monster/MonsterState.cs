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

public enum NonCombetState
{
    ENONE = 0,
    ETHINK//정지상태 혹은 주변 두리번
}
