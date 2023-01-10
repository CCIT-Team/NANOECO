using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NmonsterBullet : MonoBehaviour
{
    [SerializeField]
    private float damege = 0f;
    [SerializeField]
    private float speed = 0f;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        Destroy(gameObject, 3f);
    }

    private void OnTriggerEnter(Collider bullet)
    {
        if(bullet.gameObject.layer == 6)
        {
            var player = bullet.GetComponent<Player>();
            player.current_hp -= damege;
            player.camera_shaking_num = 3;
            Destroy(gameObject, 0.01f);
        }
    }
}
