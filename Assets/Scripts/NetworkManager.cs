using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using Newtonsoft.Json.Bson;
using static System.Net.Mime.MediaTypeNames;

/// <summary>
/// This friend is only for connecting to the Photon server and checking the connection.
/// </summary>


public class NetworkManager : MonoBehaviourPunCallbacks
{
    RoomOptions ros = new RoomOptions();
    TypedLobby typedLobby = new TypedLobby("Lobby1", LobbyType.Default);

    [SerializeField]
    PhotonView pv;

    


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

    private void Start()
    {
        Utils.is_findroom = false;
    }

    public void Connect() => PhotonNetwork.ConnectUsingSettings();
    public override void OnConnectedToMaster()
    {
        if(!Utils.is_findroom)
        PhotonNetwork.JoinLobby();
        else if(Utils.is_findroom)
        PhotonNetwork.JoinRandomRoom();
    }
    public override void OnJoinedLobby()
    {
        Debug.Log("로비 연결 완료");
        SceneFunction.loading_canvas.SetActive(false);
        Utils.Ran();
        string num_code = Utils.room_number.ToString();
        Canvas.GetComponent<WRCanvas>().Room_Code.text = num_code.Substring(0, 4) + " " + num_code.Substring(4);

        ros.MaxPlayers = 4;
        ros.IsVisible = true;
        PhotonNetwork.JoinOrCreateRoom(Canvas.GetComponent<WRCanvas>().Room_Code.text, ros, null);
    }


    private void Update()
    {
        if(make_room_panel != null)
        if (Input.GetKeyDown(KeyCode.Escape) && make_room_panel.activeSelf)
        {
            make_room_panel.SetActive(false);
        }

        if (PhotonNetwork.PlayerList.Length >= 2)
        {

            Debug.Log(PhotonNetwork.PlayerListOthers[0].ToStringFull());
        }
        //Debug.Log(PhotonNetwork.PlayerListOthers[0].ToStringFull());
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
         SceneFunction.loading_canvas.SetActive(true);
         PhotonNetwork.Disconnect();
         text.text = "...";
         Utils.room_number = 0;
    }

    public void Join_Random_Room()
    {
        Utils.is_findroom = true;
         PhotonNetwork.LeaveRoom();
         SceneFunction.loading_canvas.SetActive(true);
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
        if (!PhotonNetwork.IsMasterClient)
        {
            SceneFunction.loading_canvas.SetActive(false);
            Canvas.GetComponent<WRCanvas>().Room_Code.text = PhotonNetwork.CurrentRoom.Name;
        }

        //Debug.Log(PhotonNetwork.CurrentRoom.Name);
    }


    public override void OnLeftRoom()
    {
        Debug.Log(25);
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        SceneFunction.loading_canvas.SetActive(false);
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        SceneFunction.loading_canvas.SetActive(false);
        Debug.Log("존재하는 방이 없어요~");
    }

    [PunRPC]
    private void Player_Number_Check()
    {
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
