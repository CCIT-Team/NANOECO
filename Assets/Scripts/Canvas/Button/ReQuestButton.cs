using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ReQuestButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler , IPointerDownHandler, IPointerUpHandler
{
    public List<Button> buttons;
    public Image image;

    [Header("Play Btn")]
    public GameObject scb;

    public void OnPointerUp(PointerEventData eventData)
    {   
        for(int i = 0; i < buttons.Count; i++)
        {
            buttons[i].image.color = new Color(1, 1, 1, 1);
            buttons[i].transform.GetChild(0).gameObject.SetActive(false);
        }

        ///
        if (this.transform.GetChild(0).gameObject.active == false)
        {
            image.color = new Color(1, 1, 1, 0);
            this.transform.GetChild(0).gameObject.SetActive(true);
            SceneFunction.game_map_name = "GAMESCENE";
        }
        else
        {
            image.color = new Color(1, 1, 1, 1);
            transform.GetChild(0).gameObject.SetActive(false);
            SceneFunction.game_map_name = "";
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //throw new System.NotImplementedException();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //throw new System.NotImplementedException();
    }
}