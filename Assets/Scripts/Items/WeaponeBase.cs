using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public abstract class WeaponeBase : MonoBehaviourPunCallbacks
{
    public enum Type { ENONE = -1, EMELEE, ERANGE, ESUPPORT }
    public Type type = Type.ENONE;
    public float damage = 0;    //������
    public float attackspeed = 2;   //���ݼӵ�, Ŭ���� ����07
    public float realdelay = 2; //���� ������,  (������/�ӵ�)
    public bool isdelay = false;    //������ Ȯ�ο�
    //public float knockback = 0; //���ݽ� ���� ���ĳ��� ����

    public PhotonView pv;

    IEnumerator AttackDelay() //���� �����̿� �ڷ�ƾ
    {
        yield return new WaitForSeconds(realdelay / attackspeed);
        isdelay = false;
    }

    public virtual void Attack()  //�������ؼ� ���
    {

    }
}

