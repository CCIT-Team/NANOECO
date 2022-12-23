using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class PlayerSpawn : MonoBehaviourPunCallbacks
{
    public Transform[] spawnPoint;

    //public GameObject me;

    void Start()
    {
        CheckandSpawn();
    }

    void CheckandSpawn()
    {      
        PhotonNetwork.Instantiate("Player", spawnPoint[0].position, Quaternion.identity, 0);
    }
    //PhotonNetwork.Instantiate("Player", spawnPoint[].position, Quaternion.identity, 0);
}
