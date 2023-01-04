using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public abstract class MissionBase : MonoBehaviourPunCallbacks
{
    [Header("미션 데이터")]
    public string mission_name;
    public string mission_info;

    //Vector3 curPos;
    //Quaternion curRot;
    //public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    //{
    //    if (stream.IsWriting)
    //    {
    //        stream.SendNext(transform.position);
    //        stream.SendNext(transform.rotation);
    //    }
    //    else
    //    {
    //        curPos = (Vector3)stream.ReceiveNext();
    //        curRot = (Quaternion)stream.ReceiveNext();
    //    }
    //}

    public MissionSystem ms;

    public abstract void Mission_Event();

    public abstract void Clear();
}
