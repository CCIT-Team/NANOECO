using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Bullet : MonoBehaviourPunCallbacks
{
    public float damage = 0;
    //public float knockback = 0;
    public float speed = 500;
    public bool explosive = false;
    public ParticleSystem ps;
    SphereCollider sp;

    private void Start()
    {
        Destroy(gameObject, 1f);
        sp = GetComponent<SphereCollider>();
    }
    private void Update()
    {
        transform.Translate(speed * Time.deltaTime * Vector3.forward);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 8)
        {
            if (explosive)
            {
                speed = 0;
                sp.radius = 3;
                ps.Play();
                transform.GetChild(1).gameObject.SetActive(false);
            }
            //if(knockback != 0)
            //other.gameObject.GetComponent<Rigidbody>().AddForce(knockback * Vector3.Normalize(other.transform.position - this.transform.position), ForceMode.Impulse);
            var monster = other.gameObject.GetComponent<NewMonster>();
            monster.data.current_hp -= damage;
            monster.hit_true = true;
            if (!ps.isPlaying)
                Destroy(this.gameObject);
        }
    }
}
