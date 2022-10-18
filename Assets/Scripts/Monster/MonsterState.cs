using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CombatState
{
    ENONE = 0,
    ECHASE,
    EATTACK, //�i�� �ӵ��� ���� �ӵ� ���ݷ�
    ESKILL
}

public enum NonCombetState
{
    ENONE = 0,
    EIDLE, //�������� Ȥ�� �ֺ� �θ���
    EPATROL,
    ETHINK
}

public enum AttackType //�߰����� �氩���� �Ƹ� %�̰ɷ� �ٲ� ���ɼ� ����
{
    ENONE = 0,
    ELIGHT,
    EHAEVY
}
