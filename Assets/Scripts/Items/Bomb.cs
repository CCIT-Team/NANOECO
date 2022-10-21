using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public Vector3 start;
    public Vector3 target;
    public float flyingtime = 5;
    private float starttime;
    public float reduceheight = 1f; //높이 감소

    SphereCollider col;
    public float damage = 20;
    public bool isboom = false;
    void Start()
    {
        starttime = Time.time;
        start = this.transform.position;
        col = GetComponent<SphereCollider>();
    }
    void Update()
    {
        Vector3 center = (start + target) * 0.5F;
        center -= new Vector3(0, reduceheight, 0);
        Vector3 startRelCenter = start - center;
        Vector3 targetRelCenter = target - center;
        float fracComplete = (Time.time - starttime) / flyingtime;
        transform.position = Vector3.Slerp(startRelCenter, targetRelCenter, fracComplete);
        transform.position += center;
        if (Time.time - starttime >= 1)
        {
            isboom = true;
            col.radius = 10;
            Destroy(this.gameObject,0.1f);
        }     
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((other.tag == "Monster" || other.tag == "Player") && isboom)
        {
            other.gameObject.GetComponent<Rigidbody>().AddForce(10 * Vector3.Normalize(other.transform.position - this.transform.position), ForceMode.Impulse);
            other.gameObject.GetComponent<Character>().current_hp -= damage;
        }
    }
}
