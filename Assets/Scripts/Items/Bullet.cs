using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Bullet : MonoBehaviourPunCallbacks
{
    public float damage = 0;
    public float knockback = 0;
    public float speed = 500;

    private void Start()
    {
        Destroy(gameObject, 1f);
    }
    private void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 8)
        {
            if(knockback != 0)
                other.gameObject.GetComponent<Rigidbody>().AddForce(knockback * Vector3.Normalize(other.transform.position - this.transform.position), ForceMode.Impulse);
            other.gameObject.GetComponent<Character>().current_hp -= damage;
            Destroy(gameObject);
        }
    }
}
