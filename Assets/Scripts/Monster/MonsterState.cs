using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CurrentState
{
    EIDLE = 0,
    EPATROL,
    ECHASE,
    EATTACK, //�i�� �ӵ��� ���� �ӵ� ���ݷ�
    ESKILL
}

public enum NonCombetState
{
    ENONE = 0,
    ETHINK//�������� Ȥ�� �ֺ� �θ���
}
