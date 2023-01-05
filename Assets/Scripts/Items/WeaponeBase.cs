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
    protected float realdelay = 2; //���� ������,  (������/�ӵ�)
    public bool isdelay = false;    //������ Ȯ�ο�
    //public float knockback = 0; //���ݽ� ���� ���ĳ��� ����

    public PhotonView pv;
    public NaNoPlayer player;

    IEnumerator AttackDelay() //���� �����̿� �ڷ�ƾ
    {
        yield return new WaitForSeconds(realdelay / attackspeed);
        isdelay = false;
    }

    public virtual void Start()
    {
        GetPlayer();
    }
    public virtual void Attack()  //�������ؼ� ���
    {

    }

    public override void OnEnable()
    {
        base.OnEnable();
        if(isdelay)
            StartCoroutine("AttackDelay");
    }

    void GetPlayer()
    {
        player = transform.parent.GetComponentInParent<NaNoPlayer>();
        pv = GetComponent<PhotonView>();
    }
}

