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
    

    public GameObject[] join_panel_text;

    public void Join_Panel(GameObject joinPanel)//방 찾기 패널
    {
        joinPanel.SetActive(true);
    }

    void Turn_Text_Obj()
    {
        for(int i = 0;i < join_panel_text.Length;i++)
        {
            join_panel_text[i].SetActive(true);
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

    public void Select_Request(int index) 
    {
        for(int i =0; i < requests.Length;i++)
        {
            requests[i].GetComponent<Image>().sprite = requestBox;
        }
        if (!Utils.is_select_room)
        {
            requests[index].GetComponent<Image>().sprite = requestsBox_check;
            Utils.is_select_room = true;
        }
        else
        {
            requests[index].GetComponent<Image>().sprite = requestBox;
            Utils.is_select_room = false;
        }
    }

    public void Test_Start()
    {
        SceneFunction.game_map_name = "FastFoodPlayerTest";
        SceneFunction.fade.GetComponent<Fade>().Load_Scene();
    }
}
