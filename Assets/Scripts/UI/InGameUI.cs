using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;

public class InGameUI : MonoBehaviour
{
    public static InGameUI instace;
    public List<MissionBox> mission_box_list;
    public MissionSystem ms;
    public Color[] player_color;
    public Image[] color_bar;
    public Image[] color_point;
    public Animation[] hit_anime;
    bool first_setting = true;
    public TextMeshProUGUI[] hp;
    public int player_hand;

    bool hp_set = false;
    int a = -1;
    int b = -1;
    int c = -1;
    int d = -1;

    void Awake()
    {
        instace = this;
        //GameManager.Instance.Player_List_Set();
        ms.Mission_Box_Update(mission_box_list);
        //UI_Setting(PhotonNetwork.LocalPlayer.ActorNumber);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad0)) { ms.Mission_Clear(0); }
        if (Input.GetKeyDown(KeyCode.Keypad1)) { ms.Mission_Clear(1); }
        if (Input.GetKeyDown(KeyCode.Keypad2)) { ms.Mission_Clear(2); }
        if (Input.GetKeyDown(KeyCode.Keypad3)) { ms.Mission_Clear(3); }
        if (hp_set) { Update_HP(a, b, c, d); }
    }

    public void GM_Color()
    {
        player_color = GameManager.Instance.player_color;
    }

    public void UI_Setting()
    {
        for(int i = 0; i < GameManager.Instance.player_list.Count; i++)
        {
            if (GameManager.Instance.player_list[i] == Player.instance)
            {
                a = i;
            }
            break;
        }

        for (int i = 0; i < GameManager.Instance.player_list.Count; i++)
        {
            if(b == -1) { continue; }
            else { b = i; break; }
            if (c == -1) { continue; }
            else { c = i; break; }
            if (d == -1) { continue; }
            else { d = i; break; }
        }

        Setting(a, b, c, d);
    }

    void Setting(int a, int b, int c, int d)
    {
        Set_Color(a, b, c, d);
        Set_HP(a, b, c, d);
        hp_set = true;
    }

    void Set_Color(int a, int b, int c, int d)
    {
        color_bar[0].color = player_color[a];
        color_bar[1].color = player_color[b];
        color_bar[2].color = player_color[c];
        color_bar[3].color = player_color[d];

        color_point[0].color = player_color[a];
        color_point[1].color = player_color[b];
        color_point[2].color = player_color[c];
        color_point[3].color = player_color[d];
    }

    void Set_HP(int a, int b, int c, int d)
    {
        hp[0].text = GameManager.Instance.player_list[a].current_hp.ToString();
        hp[1].text = GameManager.Instance.player_list[b].current_hp.ToString();
        hp[2].text = GameManager.Instance.player_list[c].current_hp.ToString();
        hp[3].text = GameManager.Instance.player_list[d].current_hp.ToString();
    }

    void Update_HP(int a, int b, int c, int d)
    {
        if(int.Parse(hp[a].text) != GameManager.Instance.player_list[a].current_hp)
        {
            hp[0].text = GameManager.Instance.player_list[a].current_hp.ToString();
            hit_anime[0].Play();

        }
        if (int.Parse(hp[1].text) != GameManager.Instance.player_list[b].current_hp)
        {
            hp[1].text = GameManager.Instance.player_list[a].current_hp.ToString();
            hit_anime[1].Play();
        }
        if (int.Parse(hp[2].text) != GameManager.Instance.player_list[c].current_hp)
        {
            hp[2].text = GameManager.Instance.player_list[a].current_hp.ToString();
            hit_anime[2].Play();
        }
        if (int.Parse(hp[3].text) != GameManager.Instance.player_list[d].current_hp)
        {
            hp[3].text = GameManager.Instance.player_list[a].current_hp.ToString();
            hit_anime[3].Play();
        }
    }
}

[System.Serializable]
public class MissionBox
{
    public InGameUI ig;
    public TextMeshProUGUI mission_text;
    public Animation anime;
}