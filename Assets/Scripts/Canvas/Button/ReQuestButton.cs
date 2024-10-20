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
            Utils.is_select_room = false;
        }
        if (!transform.GetChild(0).gameObject.activeSelf)
        {
            image.color = new Color(1, 1, 1, 0);
            transform.GetChild(0).gameObject.SetActive(true);
            SceneFunction.game_map_name = "FastFoodPlayerTest";

            Utils.is_select_room = true;
        }
        else
        {
            image.color = new Color(1, 1, 1, 1);
            transform.GetChild(0).gameObject.SetActive(false);
            SceneFunction.game_map_name = "";

            Utils.is_select_room = false;
        }
    }
}
