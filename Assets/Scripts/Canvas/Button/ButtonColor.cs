using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonColor : MonoBehaviour , IPointerEnterHandler, IPointerExitHandler
{
    Image image;

    void Start()
    {
        image = GetComponent<Image>();  
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        image.color = new Color(1, 1, 1, 1);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        image.color = new Color(1, 1, 1, 0.04f);
    }

   
}
