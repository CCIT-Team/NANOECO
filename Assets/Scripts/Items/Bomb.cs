using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public Vector3 start;
    public Vector3 target;
    public float flyingtime = 5;
    private float starttime;
    public float reduceheight = 1f; //���� ����

    public ParticleSystem ps;
    SphereCollider col;
    public float damage = 20;
    public bool isboom = false;

    public float destroytime = 0.1f;
    public float knockback = 1;
    bool is_play = false;
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
        if (Time.time - starttime >= 1 )
        {
            isboom = true;
            col.radius = 10;
            if(!is_play)
            {
                ps.Play();
                is_play = true;
            }
            if (!ps.isPlaying)
            {
                Destroy(this.gameObject, destroytime);
            }
            //Destroy(this.gameObject,destroytime);
        }     
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == 8 && isboom)
        {
            other.gameObject.GetComponent<Rigidbody>().AddForce(knockback*Vector3.Normalize(other.transform.position - this.transform.position), ForceMode.Impulse);
            other.gameObject.GetComponent<Character>().current_hp -= damage;
        }
    }
}
