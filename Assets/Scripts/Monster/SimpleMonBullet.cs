using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.Pool;
public class SimpleMonBullet : MonoBehaviour
{
    [SerializeField]
    private float bullet_speed;
    Vector3 dir;
    public Rigidbody rd;
    [SerializeField]
    GameObject Player_pos;

    //private IObjectPool<SimpleMonBullet> managed_bullet_pool;
    private void Start()
    {
        Player_pos = GameObject.FindGameObjectWithTag("Player");
        dir = Player_pos.transform.position - transform.position;
        rd.AddForce(dir * Time.deltaTime * bullet_speed);
        Shoot();
    }

    private void OnTriggerEnter(Collider collider)
    {
        if(collider.tag == "Player")
        {
            collider.GetComponent<Character>().current_hp -= 5;
            Destroy(gameObject);
        }
    }

    //public void SetManagedBulletPool(IObjectPool<SimpleMonBullet> pool)
    //{
    //    managed_bullet_pool = pool;
    //}

    //public void DestroyBullets()
    //{
    //    managed_bullet_pool.Release(this);
    //}
    public void Shoot()
    {
        //Invoke("DestroyBullets", 2f);
        Destroy(gameObject, 2f);
    }
}
