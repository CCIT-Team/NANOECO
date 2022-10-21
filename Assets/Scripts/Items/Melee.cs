using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : WeaponeBase
{
    public Transform player;
    public GameObject center;

    void Start()
    {
        type = Type.EMELEE;
    }

    void Update()
    {
        if(Input.GetMouseButton(0)&&!isdelay)
        {
            isdelay = true;
            Attack();
            StartCoroutine("AttackDelay");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Monster")
        {
            other.gameObject.GetComponent<Rigidbody>().AddForce(knockback*Vector3.Normalize(other.transform.position - player.position),ForceMode.Impulse);
            other.gameObject.GetComponent<Character>().current_hp -= damage;
            Debug.Log(other.gameObject.name);
        }
    }

    new void Attack()
    {

    }

}
