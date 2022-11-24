using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

/// <summary>
/// This friend is only for connecting to the Photon server and checking the connection.
/// </summary>


public class NetworkManager : MonoBehaviourPunCallbacks
{
    //public InputField nick_name_input;
    //public GameObject disconnctpanel;
    //public GameObject connectpanel;

    public PhotonView pv;
    public TMP_Text test_nickname; 


    [Header("1")]
    public int connected_player_number;


    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        Screen.SetResolution(1920 , 1080, false);
        PhotonNetwork.SendRate = 60;
        PhotonNetwork.SerializationRate = 30;
        Connect();
    }

    public void Connect() => PhotonNetwork.ConnectUsingSettings();

    /// <summary>
    /// 서버 접속 완료
    /// </summary>
    public override void OnConnectedToMaster()
    {
        //PhotonNetwork.LocalPlayer.NickName = nick_name_input.text;
        PhotonNetwork.JoinOrCreateRoom("Room", new RoomOptions { MaxPlayers = 4 }, null);

        PhotonNetwork.LocalPlayer.NickName = Utils.nickname;

        PhotonNetwork.JoinLobby();


        //PhotonNetwork.JoinOrCreateRoom("Room", new RoomOptions { MaxPlayers = 4 }, null);
        // 만들자 마자 룸 생성 확인하면 확인이 안되니까 생성이 안료된지 확인 후 룸에 접근할것


        
    }


    /// <summary>
    /// 방 생성 완료
    /// </summary>
    public override void OnCreatedRoom()
    {
        Debug.Log(24);
        Debug.Log(PhotonNetwork.MasterClient.NickName);
        if(PhotonNetwork.InLobby)
        Debug.Log(PhotonNetwork.CurrentLobby.Name);
        if (PhotonNetwork.InRoom)
            Debug.Log(PhotonNetwork.CurrentRoom.Name);
    }


    /// <summary>
    /// 방 참가 완료
    /// </summary>
    public override void OnJoinedRoom()
    {
        //PhotonNetwork.LocalPlayer.NickName = nick_name_input.text;
        //disconnectpanel.SetActive(false);
        Spawn();
        Player_Number_Check();
       // Debug.Log(2);
        
    }


    public void Spawn()
    {
        //PhotonNetwork.Instantiate("Player", new Vector3(Random.Range(-4, 4), Random.Range(3,6), Random.Range(-4, 4)), Quaternion.identity);
        //connectpanel.SetActive(false);

        //PhotonNetwork.Instantiate("PhotonTestPlayer", new Vector3(Random.Range(-4, 4), Random.Range(3,6), Random.Range(-4, 4)), Quaternion.identity);
        //오류나서 지움

        //connectpanel.SetActive(false);

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && PhotonNetwork.IsConnected)
        {
            PhotonNetwork.Disconnect();
        }
    }
    


    public override void OnJoinedLobby()
    {
        Debug.Log(PhotonNetwork.CurrentLobby.Name);
        Debug.Log(242424);
        //GameManager.Instance.testtest.Add(this.gameObject);
        test_nickname.text = PhotonNetwork.CountOfPlayers.ToString();
        //Debug.Log(PhotonNetwork.CountOfPlayers);
    }

    public override void OnDisconnected(DisconnectCause cause)
    { 
    }

    [PunRPC]
    private void Player_Number_Check()
    {
        connected_player_number = PhotonNetwork.PlayerList.Length;
        test_nickname.text = connected_player_number.ToString();
    }

    [PunRPC]
    private void NickName_Check()
    {
        if(PhotonNetwork.LocalPlayer.NickName == "host")
        {
            //
        }
    }
}
