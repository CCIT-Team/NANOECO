using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : WeaponeBase
{
    public Transform player;
    public GameObject center;
    bool isattack = false;

    Animator ani;


    void Start()
    {
        type = Type.EMELEE;
    }

    void Update()
    {
        isattack = ani.GetBool("Close Attack");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 8 && isattack)
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
