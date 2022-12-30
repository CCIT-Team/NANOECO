using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

    public GameObject[] requests;

    public void Join_Panel(GameObject joinPanel)//방 찾기 패널 찾기
    {
        joinPanel.SetActive(true);
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

    public void Select_Request(GameObject request) 
    {

    }

    public void Test_Start()
    {
        //SceneFunction.game_map_name = "FastFoodPlayerTest";
        //SceneFunction.fade.GetComponent<Fade>().Load_Scene();
    }
}
