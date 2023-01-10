using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class GameManager : MonoBehaviourPunCallbacks,IPunObservable

{
    public static GameManager Instance;
    public Player[] players = new Player[4];
    public List<Player> player_list = new List<Player>();
    public int playersnum = 0;
    public SpawnPoint sp;
    public Transform spawnPoint;
    public int player_count;
    public PhotonView pv;
    public Color[] player_color;

    [Header("접속중인 유저")]
    public int user_0;
    public int user_1;
    public int user_2;
    public int user_3;

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(playersnum);
            stream.SendNext(player_count);
        }
        else
        {
            playersnum = (int)stream.ReceiveNext();
            player_count = (int)stream.ReceiveNext();

        }
    }
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
        if (sp == null || SceneManager.sceneCount == 2 && sp == null)
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
                if (i == 0) { user_0 = PhotonNetwork.PlayerList[0].ActorNumber; }
                else if (i == 1) { user_1 = PhotonNetwork.PlayerList[1].ActorNumber; }
                else if (i == 2) { user_2 = PhotonNetwork.PlayerList[2].ActorNumber; }
                else if (i == 3) { user_3 = PhotonNetwork.PlayerList[3].ActorNumber; }
            }
        }
    }
}


