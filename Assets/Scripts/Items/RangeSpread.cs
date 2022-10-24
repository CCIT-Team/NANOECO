using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeSpread : WeaponeBase
{

    public int ammo = 0;    //ÃÖ´ë Åº¼ö
    public int currentammo = 0; //ÇöÀç Åº¼ö
    public GameObject firePosition;
    public GameObject bullet;
    ParticleSystem p;
    

    void Start()
    {
        type = Type.ERANGE;
        currentammo = ammo;
        p = firePosition.GetComponent<ParticleSystem>();
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            switch (currentammo)
            {
                case 0:
                    if(!isdelay)
                        StartCoroutine("Reloading");
                    break;
                default:
                    Attack();
                    isdelay = true;
                    break;
            }
        }
        if (Input.GetKeyDown(KeyCode.R) && !isdelay)
            StartCoroutine("Reloading");
    }

    new void Attack()
    {
        if (!isdelay)
        {
            GameObject chargedbullet = Instantiate(bullet);
            chargedbullet.transform.position = firePosition.transform.position;
            chargedbullet.transform.rotation = firePosition.transform.rotation;
            StartCoroutine("AttackDelay");
        }
        p.Emit(15);
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
