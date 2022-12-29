using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class NmonsterBullet : MonoBehaviour
{

    private IObjectPool<NmonsterBullet> targetpool;
    [SerializeField]
    private float damege = 0f;
    [SerializeField]
    private float speed = 0f;
    // Start is called before the first frame update
    private void Awake()
    {
        gameObject.transform.position = new Vector3(0, 0, 0);
    }
    void Start()
    {
        Invoke("Destroy_Bullet", 3f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider bullet)
    {
        if(bullet.gameObject.layer == 6)
        {
            var player = bullet.GetComponent<Player>();
            player.current_hp -= damege;
            Invoke("Destroy_Bullet", 0.01f);
        }
    }

    public void Set_Target_Pool(IObjectPool<NmonsterBullet> pool)
    {
        targetpool = pool;
    }

    public void Destroy_Bullet()
    {
        targetpool.Release(this);
    }
}
