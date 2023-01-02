using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ToolBtn : MonoBehaviour
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




    /// <summary>
    /// /
    /// </summary>
    /// <param name="btn_index"></param>
    public void ReadyCheck()
    {
        my_profile.GetComponent<Image>().sprite = ready;
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
