using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Melee : WeaponeBase
{
    public Transform player;
    public override void Start()
    {
        base.Start();
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
            //other.gameObject.GetComponent<Rigidbody>().AddForce(knockback * Vector3.Normalize(other.transform.position - player.position), ForceMode.Impulse);
            var monster = other.gameObject.GetComponent<NewMonster>();
            monster.data.current_hp -= damage;
            monster.hit_true = true;
        }
    }
}
