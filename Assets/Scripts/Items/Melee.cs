using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : WeaponeBase
{
    public Transform player;


    void Start()
    {
        type = Type.EMELEE;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)&&!isdelay)
        {
            isdelay = true;
            StartCoroutine("AttackDelay");
        }
            
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 8 && isdelay)
        {
            other.gameObject.GetComponent<Rigidbody>().AddForce(knockback * Vector3.Normalize(other.transform.position - player.position), ForceMode.Impulse);
            other.gameObject.GetComponent<Character>().current_hp -= damage;
        }
    }
}
