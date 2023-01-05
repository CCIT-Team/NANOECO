using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;
using Photon.Pun.Demo.Cockpit;
using Photon.Realtime;

public class ToolBtn : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private TMP_Text toptext;
    [SerializeField]
    private GameObject topline;
    [SerializeField]
    private GameObject ready_join;

    public GameObject[] all_canvas;

    public Sprite requestBox;
    public Sprite requestsBox_check;
    public GameObject[] requests;

    public GameObject master_check;
    public GameObject master_check_for_guest;

    [Space(10)]
    public GameObject my_profile;
    public Sprite ready;
    public Sprite unready;
    public Sprite ready_guest;
    public Sprite unready_guest;
    public GameObject[] user_profile_info;

    [SerializeField]
    PhotonView pv;


    public GameObject join_panel;
    public GameObject[] join_panel_text;
    public void Join_Panel(GameObject joinPanel)//방 찾기 패널 && Text_SetActiveOn
    {
        joinPanel.SetActive(true);
        Invoke("Turn_Text_Obj", 0.5f);//31
    }
    void Turn_Text_Obj()
    {
        for(int i = 0;i < join_panel_text.Length;i++)
        {
            join_panel_text[i].SetActive(true);
        }
    }



    public void ReadyCheck()
    {
        if (Utils.is_ready)
        {
            my_profile.GetComponent<Image>().sprite = unready;
            pv.RPC("Count_Ready_Player", RpcTarget.AllBuffered , false);
            Utils.is_ready = false;
        }
        else
        {
            my_profile.GetComponent<Image>().sprite = ready;
            pv.RPC("Count_Ready_Player", RpcTarget.AllBuffered, true);
            Utils.is_ready = true;
        }

        pv.RPC("Guest_Ready_Check", RpcTarget.OthersBuffered, true);



        SceneFunction.game_map_name = "FastFoodKitchen";
        SceneFunction.fade.GetComponent<Fade>().Load_Scene();
    }

    [PunRPC]
    public void Count_Ready_Player(bool updown)
    {
        if (updown)
            Utils.ready_complete_player_index_inroom++;
        else
            Utils.ready_complete_player_index_inroom--;
    }

    [PunRPC]
    public void Guest_Ready_Check(bool is_ready) 
    {
        for(int i = 0; i < user_profile_info.Length;i++)
        {
            user_profile_info[i].GetComponent<Image>().sprite = unready_guest;
        }

        for(int i = 0; i < Utils.ready_complete_player_index_inroom; i++)
        {
            user_profile_info[i].GetComponent<Image>().sprite = ready_guest;
        }
    }



















    //Toolbar에서 Canvas Switch 역할 함수
    public void Active_Canvas(float btn_index)
    {
        for(int i = 0; i < all_canvas.Length; i++)
        {
            all_canvas[i].SetActive(false);
        }
        if(btn_index == 0)
        {
            //////////////
            //REQUEST Canvas
            //////////////
            toptext.gameObject.SetActive(true);
            topline.SetActive(true);
            ready_join.SetActive(true);

            all_canvas[0].SetActive(true);

            toptext.text = "REQUEST";
        }
        else if(btn_index == 1)
        {
            //////////////
            //Home Canvas
            //////////////

            toptext.gameObject.SetActive(false);
            topline.SetActive(false);
            ready_join.SetActive(true);

            all_canvas[1].SetActive(true);
        }
        else if(btn_index == 2)
        {
            //////////////
            //TOOLS Canvas
            //////////////
            toptext.gameObject.SetActive(true);
            topline.SetActive(true);
            ready_join.SetActive(true);

            all_canvas[2].SetActive(true);

            toptext.text = "TOOLS";
        }
    }



    int re_index;
    public void Select_Request(int index) 
    {
        for(int i =0; i < requests.Length;i++)
        {
            requests[i].GetComponent<Image>().sprite = requestBox;
        }

        if(Utils.is_select_room == false)
        {
            requests[index].GetComponent<Image>().sprite = requestsBox_check;
            this.re_index = index;
            Utils.is_select_room = true;
        }
        else if(Utils.is_select_room)
        {
            Utils.is_select_room = false;
            if (re_index == index)
            {
                requests[index].GetComponent<Image>().sprite = requestBox;
                re_index = 5; // 높게 설정  
            }
            else
            {
                requests[index].GetComponent<Image>().sprite = requestsBox_check;
            }
        }
    }

    public void Test_Start()
    {
        SceneFunction.game_map_name = "FastFoodPlayerTest";
        SceneFunction.fade.GetComponent<Fade>().Load_Scene();
    }

    private void Update()
    {
        if (join_panel.activeSelf == true && Input.GetKeyDown(KeyCode.Escape))
        {
            for (int i = 0; i < join_panel_text.Length; i++)
            {
                join_panel_text[i].SetActive(false);
            }
            join_panel.SetActive(false);
        }

        
    }

    
}
