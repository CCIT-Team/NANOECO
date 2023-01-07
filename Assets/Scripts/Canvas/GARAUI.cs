using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GARAUI : MonoBehaviour
{
    public Sprite ready;
    public Sprite unready;
    public GameObject[] ttt;
    public Button[] buttons;
    public Sprite[] weapons;
    public void check1()
    {
        if (buttons[0].enabled == true)
        {
            ttt[0].GetComponent<Image>().sprite = ready;
            ttt[1].GetComponent<Image>().sprite = unready;
            ttt[2].GetComponent<Image>().sprite = unready;
            ttt[3].GetComponent<Image>().sprite = unready;
            ttt[4].GetComponent<Image>().sprite = weapons[0];
        }
    }

    public void check2()
    {
        
        if (buttons[1].enabled == true)
        {
            ttt[0].GetComponent<Image>().sprite = unready;
            ttt[1].GetComponent<Image>().sprite = ready;
            ttt[2].GetComponent<Image>().sprite = unready;
            ttt[3].GetComponent<Image>().sprite = unready;
            ttt[4].GetComponent<Image>().sprite = weapons[1];

        }
    }

    public void check3()
    {
        if (buttons[2].enabled == true)
        {
            ttt[0].GetComponent<Image>().sprite = unready;
            ttt[1].GetComponent<Image>().sprite = unready;
            ttt[2].GetComponent<Image>().sprite = ready;
            ttt[3].GetComponent<Image>().sprite = unready;
            ttt[4].GetComponent<Image>().sprite = weapons[2];
        }
    }
    public void check4()
    {
        if (buttons[3].enabled == true)
        {
            ttt[0].GetComponent<Image>().sprite = unready;
            ttt[1].GetComponent<Image>().sprite = unready;
            ttt[2].GetComponent<Image>().sprite = unready;
            ttt[3].GetComponent<Image>().sprite = ready;
            ttt[4].GetComponent<Image>().sprite = weapons[3];
        }
    }
}
