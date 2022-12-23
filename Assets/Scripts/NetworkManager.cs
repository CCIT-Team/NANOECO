using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using Newtonsoft.Json.Bson;

/// <summary>
/// This friend is only for connecting to the Photon server and checking the connection.
/// </summary>


public class NetworkManager : MonoBehaviourPunCallbacks
{
    RoomOptions ros = new RoomOptions();
    TypedLobby typedLobby = new TypedLobby("Lobby1", LobbyType.Default);

    [SerializeField]
    PhotonView pv;
    [SerializeField]
    TMP_Text test_nickname;


    [Header("1")]
    public int connected_player_number;


    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject); 
        Screen.SetResolution(1920, 1080, false);



        PhotonNetwork.LocalPlayer.NickName = Utils.nickname;
        PhotonNetwork.LogLevel = PunLogLevel.Full;
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.SendRate = 60;
        PhotonNetwork.SerializationRate = 30;
        Connect();
    }

    public void Connect() => PhotonNetwork.ConnectUsingSettings();
    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
    }
    public override void OnJoinedLobby()
    {
        Debug.Log("로비 연결 완료");
        SceneFunction.loading_canvas.SetActive(false);
    }


    private void Update()
    {
        if(make_room_panel != null)
        if (Input.GetKeyDown(KeyCode.Escape) && make_room_panel.activeSelf)
        {
            make_room_panel.SetActive(false);
        }

        if(PhotonNetwork.InRoom)
        Debug.Log(PhotonNetwork.CurrentRoom.Players.Count);
    }
    /// <summary>
    /// //////////////////////////////////////////////////////////////////////
    /// </summary>
    /// <param name="Make_Room_Panel"></param>
    private GameObject make_room_panel;
    public void Active_Room_Panel(GameObject Make_Room_Panel)
    {
        this.make_room_panel = Make_Room_Panel;
        Make_Room_Panel.SetActive(true);
    }

    //
    public void Make_Room_Panel(TMP_Text text)
    {
        if (!Utils.is_inRoom)
        {
            SceneFunction.loading_canvas.SetActive(true);
            Utils.Ran();
            string num_code = Utils.room_number.ToString();
            
            text.text = num_code.Substring(0,4) + " " + num_code.Substring(4);
            
            Utils.is_inRoom = true;
            ros.MaxPlayers = 4;
            ros.IsVisible = true;
            PhotonNetwork.JoinOrCreateRoom(text.text, ros, null);
        }
        else if(Utils.is_inRoom) 
        {
            SceneFunction.loading_canvas.SetActive(true);
            PhotonNetwork.Disconnect();
            text.text = "...";
            Utils.room_number= 0;
        }
    }

    public void Join_Random_Room()
    {
        if(!Utils.is_inRoom) 
        {
            PhotonNetwork.JoinRandomRoom();
            SceneFunction.loading_canvas.SetActive(true);
        }
    }

/// <summary>
/// /////////////////////////////////////////////////////////////////
/// </summary>

    public override void OnCreatedRoom()
    {
        if (PhotonNetwork.InRoom)
        {
            SceneFunction.loading_canvas.SetActive(false);
        }
    }

    [SerializeField]
    private GameObject Canvas;
    public override void OnJoinedRoom()
    {
        SceneFunction.loading_canvas.SetActive(false);
        Canvas.GetComponent<WRCanvas>().Room_Code.text = PhotonNetwork.CurrentRoom.Name;

    }



    public override void OnDisconnected(DisconnectCause cause)
    {
        SceneFunction.loading_canvas.SetActive(false);
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        SceneFunction.loading_canvas.SetActive(false);
        Debug.Log(2424);
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
