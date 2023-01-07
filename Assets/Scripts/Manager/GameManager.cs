using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class GameManager : MonoBehaviourPunCallbacks
{
    public static GameManager Instance;
    public Player[] players = new Player[4];
    public int playersnum = 0;
    public SpawnPoint sp;
    public Transform spawnPoint;
    public int player_count;
    public PhotonView pv;

    [Header("접속중인 유저")]
    public string user_0;
    public string user_1;
    public string user_2;
    public string user_3;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        Instance = this;
    }

    private void Start()
    {
        sp = GameObject.FindGameObjectWithTag("Spawn").GetComponent<SpawnPoint>();
        playersnum = 0;
    }

    void Update()
    {
        if (sp == null || SceneManager.sceneCount == 3 && sp == null)
        {
            sp = GameObject.FindGameObjectWithTag("Spawn").GetComponent<SpawnPoint>();
        }
        if (playersnum > 4)
        {
            playersnum = PhotonNetwork.PlayerList.Length;
        }
        SpawnPointUpdate();
    }

    public void SpawnPointUpdate()
    {
        spawnPoint = sp.check_points[sp.current_spawn_point].transform;
    }

    public void Player_List_Set()
    {
        if (pv.IsMine == true)
        {
            for(int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
            {
                //if(i == 0) { user_0 = PhotonNetwork.PlayerList[0].ActorNumber; }
                //else if(i == 1) { user_1 = PhotonNetwork.PlayerList[1].NickName; }
                //else if(i == 2) { user_2 = PhotonNetwork.PlayerList[2].NickName; }
                //else if(i == 3) { user_3 = PhotonNetwork.PlayerList[3].NickName; }
            }
        }
    }
}


