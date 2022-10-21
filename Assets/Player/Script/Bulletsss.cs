using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bulletsss : MonoBehaviour
{
    public float damage = 10;
    public float speed = 500;

    private void Start()
    {
        Destroy(gameObject, 1f);
    }
    private void Update()
    {
        transform.Translate(Vector3.forward * speed);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Monster")
        {
            other.gameObject.GetComponent<Character>().current_hp -= damage;
            Destroy(gameObject);
        }
    }
}
