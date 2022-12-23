using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class PlayerSpawn : MonoBehaviourPunCallbacks
{
    public Transform[] spawnPoint;

    void Start()
    {
        CheckandSpawn();
    }

    void CheckandSpawn()
    {
        for(int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
        {
            PhotonNetwork.Instantiate("Player", spawnPoint[i].position, Quaternion.identity, 0);
        }
    }
}
