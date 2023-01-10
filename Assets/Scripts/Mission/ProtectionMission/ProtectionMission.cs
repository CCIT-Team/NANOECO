using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ProtectionMission : MissionBase, IPunObservable
{
    public ProtectionTarget target;

    public List<GameObject> monster_group = new List<GameObject>();
    public List<Transform> spawn_point = new List<Transform>();
    public int wave_count;
    public float wave_time;
    public bool mission_start = false;

    Vector3 curPos;
    Quaternion curRot;
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
            stream.SendNext(wave_count);
            stream.SendNext(wave_time);
            stream.SendNext(mission_start);
        }
        else
        {
            curPos = (Vector3)stream.ReceiveNext();
            curRot = (Quaternion)stream.ReceiveNext();
            wave_count = (int)stream.ReceiveNext();
            wave_time = (float)stream.ReceiveNext();
            mission_start = (bool)stream.ReceiveNext();
        }
    }

    public override void Clear()
    {
        ms.mission_2_clear = true;
        ms.Mission_Clear(2);
    }

    public override void Mission_Event()
    {
        if(!mission_start)
        {
            mission_start = true;
            StartCoroutine(Monster_Wave());
        }
    }

    IEnumerator Monster_Wave()
    {
        wave_count--;
        for (int i = 0; i < spawn_point.Count; i++)
        {
            int j = Random.Range(0, monster_group.Count);
            Instantiate(monster_group[j], spawn_point[i].position, Quaternion.identity);
        }
        yield return new WaitForSeconds(wave_time);
        if(wave_count != 0 && target.hp > 0) { StartCoroutine(Monster_Wave()); }
        else if(wave_count == 0 && target.hp > 0) { Clear(); }
    }

    public void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.layer == 6)
            Mission_Event();
    }
}