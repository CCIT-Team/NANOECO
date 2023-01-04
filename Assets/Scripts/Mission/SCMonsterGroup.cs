using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SCMonsterGroup : MonoBehaviourPunCallbacks, IPunObservable
{
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
}
