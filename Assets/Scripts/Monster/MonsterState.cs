using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CombatState
{
    ENONE = 0,
    ECHASE,
    EATTACK, //쫒는 속도와 공격 속도 공격력
    ESKILL
}

public enum NonCombetState
{
    ENONE = 0,
    EIDLE, //정지상태 혹은 주변 두리번
    EPATROL,
    ETHINK
}

public enum AttackType //중갑인지 경갑인지 아마 %이걸로 바뀔 가능성 높음
{
    ENONE = 0,
    ELIGHT,
    EHAEVY
}
