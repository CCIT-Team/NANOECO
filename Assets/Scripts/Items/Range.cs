using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Range : WeaponeBase
{

    public int ammo = 0;    //�ִ� ź��
    public int currentammo = 0; //���� ź��
    public GameObject firePosition;
    public GameObject bullet;
    void Start()
    {
        type = Type.ERANGE;
        currentammo = ammo;
        bullet.GetComponent<Bullet>().damage = damage;
        bullet.GetComponent<Bullet>().knockback = knockback;
    }

    void Update()
    {
        if (Input.GetMouseButton(0)&& !isdelay)
        {
            switch (currentammo)
            {
                case 0:
                    StartCoroutine("Reloading");
                    break;
                default :
                    isdelay = true;
                    Attack();
                    StartCoroutine("AttackDelay");
                    break;
            }
        }
        if(Input.GetKeyDown(KeyCode.R) && !isdelay)
            StartCoroutine("Reloading");
    }

    new void Attack()
    {
        GameObject chargedbullet = Instantiate(bullet);
        chargedbullet.transform.position = firePosition.transform.position;
        chargedbullet.transform.rotation = firePosition.transform.rotation;
        currentammo--;
    }

    IEnumerator Reloading()
    {
        isdelay = true;
        yield return new WaitForSecondsRealtime(2);
        currentammo = ammo;
        isdelay = false;
    }
}