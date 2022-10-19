using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponeBase : MonoBehaviour
{
    public enum Type { ENONE = -1, EMELEE, ERANGE, ESUPPORT }
    public Type type = Type.ENONE;
    public float damage = 0;    //데미지
    public float attackspeed = 2;   //공격속도, 클수록 빠름07
    public float realdelay = 5; //공격 딜레이,  (딜레이/속도)
    public bool isdelay = false;    //딜레이 확인용
    public float knockback = 0; //공격시 적을 밀쳐내는 정도

    public int ammo = 0;    //최대 탄수
    public int currentammo = 0; //현재 탄수
    public GameObject firePosition;


    IEnumerator AttackDelay() //공격 딜레이용 코루틴
    {
        yield return new WaitForSeconds(realdelay/attackspeed);
        isdelay = false;
    }

    public void Attack()  //실험용,재정의해서 사용
    {
        Debug.Log("샷");
    }
}

