using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.Pool;
public class SimpleMonBullet : Character
{
    [SerializeField]
    private LayerMask target_mask;
    [SerializeField]
    private float bullet_speed;

    //private IObjectPool<SimpleMonBullet> managed_bullet_pool;
    private void Start()
    {
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * bullet_speed);

    }

    private void OnTriggerEnter(Collider collider)
    {
        if(target_mask == 6)
        {
            collider.GetComponent<Character>().current_hp -= damage;
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
