using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public abstract class WeaponeBase : MonoBehaviourPunCallbacks
{
    public enum Type { ENONE = -1, EMELEE, ERANGE, ESUPPORT }
    public Type type = Type.ENONE;
    public float damage = 0;    //데미지
    public float attackspeed = 2;   //공격속도, 클수록 빠름07
    protected float realdelay = 2; //공격 딜레이,  (딜레이/속도)
    public bool isdelay = false;    //딜레이 확인용
    //public float knockback = 0; //공격시 적을 밀쳐내는 정도

    public PhotonView pv;
    public NaNoPlayer player;

    IEnumerator AttackDelay() //공격 딜레이용 코루틴
    {
        yield return new WaitForSeconds(realdelay / attackspeed);
        isdelay = false;
    }

    public virtual void Start()
    {
        GetPlayer();
    }
    public virtual void Attack()  //재정의해서 사용
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

