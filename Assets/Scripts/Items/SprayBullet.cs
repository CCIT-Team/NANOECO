using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class SprayBullet : Bullet
{
    Player player;
    [HideInInspector]
    public bool canattack = true;
    new void Start()
    {
        
    }

    // Update is called once per frame
    new void Update()
    {
        
    }
    new void OnTriggerEnter(Collider other)
    {

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == 8 && canattack)
        {
            player.camera_shaking_num = 2;
            var monster = other.gameObject.GetComponent<NewMonster>();
            monster.data.current_hp -= damage;
            monster.hit_true = true;
            canattack = false;
            StartCoroutine("Delayattack");
        }
        if (other.gameObject.layer == 11)
        {
            other.gameObject.GetComponent<DestroyTarget>()._hp -= damage;
        }
    }

    IEnumerator Delayattack()
    {
        yield return new WaitForSeconds(0.5f);
        canattack = true;
    }
}
