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
        if (Input.GetMouseButtonDown(0))
            //StartCoroutine("AttackDelay");
            
        if (Input.GetMouseButton(0))
        {
            switch (currentammo)
            {
                case 0:
                    StartCoroutine("Reloading");
                    break;
                default:
                    Attack();
                    break;
            }
        }

        if(Input.GetMouseButtonUp(0))
        {
            new WaitForSecondsRealtime(0.5f);
            bullet.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.R) && !isdelay)
            StartCoroutine("Reloading");
    }

    new void Attack()
    {
        bullet.SetActive(true);
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

    private void OnTriggerStay(Collider other)
    {
        if(!isdelay&&other.tag == "Monster")
        {
            other.GetComponent<Character>().current_hp -= damage;
        }
    }
}
