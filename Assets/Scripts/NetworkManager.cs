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
    public PhotonView pv;
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
        Canvas.GetComponent<WRCanvas>().Room_Code.text = num_code.Substring(0, 4) + num_code.Substring(4);

        ros.MaxPlayers = 4;
        ros.IsVisible = true;
        PhotonNetwork.JoinOrCreateRoom(Canvas.GetComponent<WRCanvas>().Room_Code.text, ros, null);
    }


    private void Update()
    {
        //for master check
        if(PhotonNetwork.InRoom)
        if (PhotonNetwork.IsMasterClient)
        {
            if (PhotonNetwork.CurrentRoom.PlayerCount >= 2)
            {
                tool_btn.GetComponent<ToolBtn>().master_check.SetActive(true);
            }
            else
            {
                tool_btn.GetComponent<ToolBtn>().master_check.SetActive(false);
            }
        }
        else
        {
            if(PhotonNetwork.CurrentRoom.PlayerCount >= 2)
            {
                tool_btn.GetComponent<ToolBtn>().master_check_for_guest.SetActive(true);
            }
            else
            {
                tool_btn.GetComponent<ToolBtn>().master_check_for_guest.SetActive(false);
            }
        }
    }
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
    public GameObject tool_btn;
    public override void OnJoinedRoom()
    {
        SceneFunction.loading_canvas.SetActive(false);
        pv.RPC("Player_Number_Check", RpcTarget.AllBuffered);
        if (PhotonNetwork.IsMasterClient)
        {
            
            //
            //몇명인지 확인하고 갯수대로 키고
            //Canvas.GetComponent<WRCanvas>().Room_Code.text = PhotonNetwork.CurrentRoom.Name;
        }
        else
        {

        }

        //Debug.Log(PhotonNetwork.CurrentRoom.Name);
    }


    public override void OnLeftRoom()
    {
        Debug.Log(25);
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        pv.RPC("Player_Number_Check", RpcTarget.AllBuffered);
        SceneFunction.loading_canvas.SetActive(false);
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        SceneFunction.loading_canvas.SetActive(false);
        Debug.Log("존재하는 방이 없어요~");
    }



    public void Ready_Check()
    {
        if(PhotonNetwork.IsMasterClient)
        {
            if(Utils.is_select_room)
            {
                Utils.info_message.text = "You Should Wait All Player!";
                Utils.info_canvas.SetActive(true);
            }
            else
            {

            }
        }
    }

    [PunRPC]
    private void Player_Number_Check()
    {
        //Debug.Log(PhotonNetwork.CurrentRoom.PlayerCount);
        for(int i = 0; i < 3;i++)
        {
            tool_btn.GetComponent<ToolBtn>().user_profile_info[i].SetActive(false);
        }
        for(int i = 0; i < PhotonNetwork.CurrentRoom.PlayerCount - 1; i++)
        {
            tool_btn.GetComponent<ToolBtn>().user_profile_info[i].SetActive(true);
        }
    }

    
}
