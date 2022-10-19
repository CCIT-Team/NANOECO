using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponeBase : MonoBehaviour
{
    public enum Type { ENONE = -1, EMELEE, ERANGE, ESUPPORT }
    public Type type = Type.ENONE;
    public float damage = 0;    //������
    public float attackspeed = 2;   //���ݼӵ�, Ŭ���� ����07
    public float realdelay = 5; //���� ������,  (������/�ӵ�)
    public bool isdelay = false;    //������ Ȯ�ο�
    public float knockback = 0; //���ݽ� ���� ���ĳ��� ����

    public int ammo = 0;    //�ִ� ź��
    public int currentammo = 0; //���� ź��
    public GameObject firePosition;


    IEnumerator AttackDelay() //���� �����̿� �ڷ�ƾ
    {
        yield return new WaitForSeconds(realdelay/attackspeed);
        isdelay = false;
    }

    public void Attack()  //�����,�������ؼ� ���
    {
        Debug.Log("��");
    }
}

