using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class TransportTarget : MonoBehaviourPunCallbacks, IPunObservable
{
    public TransportMission path;

    Vector3 curPos;
    Quaternion curRot;
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
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

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 6)
            path._active_count++;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 6)
            path._active_count--;
    }
}