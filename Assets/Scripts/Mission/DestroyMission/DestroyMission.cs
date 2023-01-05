using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class DestroyMission : MissionBase, IPunObservable
{
    public List<DestroyTarget> target = new List<DestroyTarget>();
    public List<GameObject> monster_group = new List<GameObject>();
    public List<GameObject> spawn_point = new List<GameObject>();
    public float wave_time;
    public bool started = false;

    Vector3 curPos;
    Quaternion curRot;
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
            stream.SendNext(started);
            stream.SendNext(wave_time);
            stream.SendNext(mm);
        }
        else
        {
            curPos = (Vector3)stream.ReceiveNext();
            curRot = (Quaternion)stream.ReceiveNext();
            started = (bool)stream.ReceiveNext();
            wave_time = (float)stream.ReceiveNext();
            mm = (int)stream.ReceiveNext();
        }
    }

    public float hp
    {
        get { return Target_Hp(); }
    }

    void OnTriggerEnter(Collider other)
    {
        if(!started && other.gameObject.layer == 6)
        {
            Mission_Event();
            started = true;
        }
    }

    public override void Clear()
    {
        ms.mission_1_clear = true;
    }

    public override void Mission_Event()
    {
        StartCoroutine(Spawn_Monster());
    }

    int mm;
    IEnumerator Spawn_Monster()
    {
        for(int j = 0; j < spawn_point.Count; j++)
        {
            mm = Random.Range(0, monster_group.Count);
            GameObject mg = Instantiate(monster_group[mm], spawn_point[j].transform.position, Quaternion.identity);
            mg.transform.parent = transform;
        }

        yield return new WaitForSeconds(wave_time);
        StartCoroutine(Spawn_Monster());
    }

    float Target_Hp()
    {
        float hp = 0;
        foreach (var g in target)
        {
            hp += g._hp;
        }
        
        if(hp <= 0)
        {
            StopAllCoroutines();
        }
        return hp;
    }
}