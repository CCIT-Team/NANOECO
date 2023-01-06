using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Bullet : MonoBehaviourPunCallbacks, IPunObservable
{
    protected float flytime = 1;
    public float damage = 5;
    //public float knockback = 0;
    public float speed = 50;
    public bool explosive = false;
    public ParticleSystem ps;
    SphereCollider sp;

    private void Start()
    {
        Destroy(gameObject, 50*flytime/speed);
        sp = GetComponent<SphereCollider>();
    }
    private void Update()
    {
        transform.Translate(speed * Time.deltaTime * Vector3.forward);
    }
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("닿음");
        if (other.gameObject.layer == 8)
        {
            Debug.Log("레이어 확인");
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
        if(other.gameObject.layer == 11)
        {
            other.gameObject.GetComponent<DestroyTarget>()._hp -= damage;
        }
    }

    Vector3 curPos;
    Quaternion curRot;
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
       if(stream.IsWriting)
        {
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
        }
       else
        {
            curPos = (Vector3)stream.ReceiveNext();
            curRot = (Quaternion)stream.ReceiveNext();
        }
    }
}
