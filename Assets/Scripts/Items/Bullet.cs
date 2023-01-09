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
    public float speed = 5;
    public bool explosive = false;
    public ParticleSystem ps;
    SphereCollider sp;

    protected void Start()
    {
        Destroy(gameObject, speed != 0 ? 5*flytime/speed : 0.5f );
        sp = GetComponent<SphereCollider>();
    }
    protected void Update()
    {
        transform.Translate(speed * Time.deltaTime * Vector3.forward);
    }
    protected void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 8)
        {
            if (explosive)
            {
                speed = 0;
                sp.radius *= 5;
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

    protected Vector3 curPos;
    protected Quaternion curRot;
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
