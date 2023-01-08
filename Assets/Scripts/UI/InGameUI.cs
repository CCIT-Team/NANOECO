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
    public List<Image> color_point = new List<Image>();
    public List<Image> color_point_ = new List<Image>();
    public Animation[] hit_anime;
    bool first_setting = true;
    public TextMeshProUGUI[] hp;
    public int player_hand;
    public Player ply;

    bool hp_set = false;
    public int a = -1;
    public int b = -1;
    public int c = -1;
    public int d = -1;

    public int hh = -1;
    public bool sibal = true;

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
        zzz();
        if (hp_set) { Update_HP(a, b, c, d); }
    }

    public void zzz()
    {
        if (GameManager.Instance.player_list.Count == PhotonNetwork.PlayerList.Length && sibal == true)
        {
            UI_Setting(ply.player_actornum);
            sibal = false;
        }
    }

    public void GM_Color()
    {
        player_color = GameManager.Instance.player_color;
    }

    public void UI_Setting(int i)
    {
        switch(i)
        {
            case 0:
                a = 0;
                b = 1;
                c = 2;
                d = 3;
                break;
            case 1:
                a = 1;
                b = 0;
                c = 2;
                d = 3;
                break;
            case 2:
                a = 2;
                b = 0;
                c = 1;
                d = 3;
                break;
            case 3:
                a = 3;
                b = 0;
                c = 1;
                d = 2;
                break;
        }
        Setting(a, b, c, d);
    }

    void Setting(int a, int b, int c, int d)
    {
        GameManager.Instance.player_list[a].cccc = player_color[a];
        GameManager.Instance.player_list[b].cccc = player_color[b];
        //GameManager.Instance.player_list[c].cccc = player_color[c];
        //GameManager.Instance.player_list[d].cccc = player_color[d];

        Set_Color(a, b, c, d);
        Set_HP(a, b, c, d);
        hp_set = true;
    }

    void Set_Color(int a, int b, int c, int d)
    {
        color_bar[0].color = player_color[a];
        color_bar[1].color = player_color[b];
        //color_bar[2].color = player_color[c];
        //color_bar[3].color = player_color[d];

        color_point[a].color = player_color[a];
        color_point[b].color = player_color[b];
        //color_point[c].color = player_color[c];
        //color_point[d].color = player_color[d];
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
        if(int.Parse(hp[0].text) != GameManager.Instance.player_list[a].current_hp)
        {
            hp[0].text = GameManager.Instance.player_list[a].current_hp.ToString();
            hit_anime[0].Play();

        }
        if (int.Parse(hp[1].text) != GameManager.Instance.player_list[b].current_hp)
        {
            hp[1].text = GameManager.Instance.player_list[b].current_hp.ToString();
            hit_anime[1].Play();
        }
        if (int.Parse(hp[2].text) != GameManager.Instance.player_list[c].current_hp)
        {
            hp[2].text = GameManager.Instance.player_list[c].current_hp.ToString();
            hit_anime[2].Play();
        }
        if (int.Parse(hp[3].text) != GameManager.Instance.player_list[d].current_hp)
        {
            hp[3].text = GameManager.Instance.player_list[d].current_hp.ToString();
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