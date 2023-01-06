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

}


