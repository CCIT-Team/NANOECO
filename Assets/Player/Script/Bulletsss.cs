using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bulletsss : MonoBehaviour
{
    public float damage = 10;
    public float speed = 500;
    private void Update()
    {
        transform.Translate(Vector3.forward * speed);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Monster")
        {
            damage -= other.gameObject.GetComponent<Character>().current_hp;
            Destroy(gameObject);
        }
        else { Destroy(gameObject, 3f); }
    }
}
