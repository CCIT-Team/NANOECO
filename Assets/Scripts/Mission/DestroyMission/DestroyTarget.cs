using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class DestroyTarget : MonoBehaviourPunCallbacks
{
    public DestroyMission dm;
    public float hp;
    public GameObject eft;
    public float _hp
    {
        get { return hp; }
        set
        {
            hp = value;
            if (hp <= 0) { Destroy_Object(); }
        }
    }
    Vector3 curPos;
    Quaternion curRot;
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
            stream.SendNext(_hp);
        }
        else
        {
            curPos = (Vector3)stream.ReceiveNext();
            curRot = (Quaternion)stream.ReceiveNext();
            _hp = (float)stream.ReceiveNext();
        }
    }

    void Destroy_Object()
    {
        int i = dm.target.IndexOf(this);
        dm.target.RemoveAt(i);

        if(dm.target.Count == 0)
        {
            dm.StopAllCoroutines();
            dm.Clear();
        }
        Instantiate(eft, transform.position, Quaternion.identity);
        Destroy(gameObject,0.01f);
    }
}
