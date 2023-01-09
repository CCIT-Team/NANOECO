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
    public int[] ff = new int[4];
    public int player_hand;
    public Player ply;
    public List<Image> weapon_icon;
    public TextMeshProUGUI max_ammo;
    public TextMeshProUGUI current_ammo;
    public Range gun;
    public Range launcher;
    public RangeSpread spray;
    WeaponeBase wbb;

    bool hp_set = false;
    public int a = -1;
    public int b = -1;
    public int c = -1;
    public int d = -1;

    public int hh = -1;
    public bool sibal = true;
    public bool setting = false;

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
        if (hp_set)
        {
            Update_HP(a, b, c, d);
            Hand_Icon();
            Ammo_Setting();
        }
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
        GameManager.Instance.player_list[c].cccc = player_color[c];
        GameManager.Instance.player_list[d].cccc = player_color[d];

        Set_Color(a, b, c, d);
        Init_ff();
        Set_HP(a, b, c, d);
        hp_set = true;
    }

    void Set_Color(int a, int b, int c, int d)
    {
        color_bar[0].color = player_color[a];
        color_bar[1].color = player_color[b];
        color_bar[2].color = player_color[c];
        color_bar[3].color = player_color[d];

        color_point[a].color = player_color[a];
        color_point[b].color = player_color[b];
        color_point[c].color = player_color[c];
        color_point[d].color = player_color[d];
    }

    void Set_HP(int a, int b, int c, int d)
    {
        //hp[0].text = GameManager.Instance.player_list[a].current_hp.ToString();
        //hp[1].text = GameManager.Instance.player_list[b].current_hp.ToString();
        //hp[2].text = GameManager.Instance.player_list[c].current_hp.ToString();
        //hp[3].text = GameManager.Instance.player_list[d].current_hp.ToString();

        hp[ff[0]].text = GameManager.Instance.player_list[ff[0]].current_hp.ToString();
        hp[ff[1]].text = GameManager.Instance.player_list[ff[1]].current_hp.ToString();
        hp[ff[2]].text = GameManager.Instance.player_list[ff[2]].current_hp.ToString();
        hp[ff[3]].text = GameManager.Instance.player_list[ff[3]].current_hp.ToString();
    }

    void Update_HP(int a, int b, int c, int d)
    {
        //if(int.Parse(hp[0].text) != GameManager.Instance.player_list[a].current_hp)
        //{
        //    hp[0].text = GameManager.Instance.player_list[a].current_hp.ToString();
        //    hit_anime[0].Play();

        //}
        //if (int.Parse(hp[1].text) != GameManager.Instance.player_list[b].current_hp)
        //{
        //    hp[1].text = GameManager.Instance.player_list[b].current_hp.ToString();
        //    hit_anime[1].Play();
        //}
        //if (int.Parse(hp[2].text) != GameManager.Instance.player_list[c].current_hp)
        //{
        //    hp[2].text = GameManager.Instance.player_list[c].current_hp.ToString();
        //    hit_anime[2].Play();
        //}
        //if (int.Parse(hp[3].text) != GameManager.Instance.player_list[d].current_hp)
        //{
        //    hp[3].text = GameManager.Instance.player_list[d].current_hp.ToString();
        //    hit_anime[3].Play();
        //}

        if (int.Parse(hp[ff[0]].text) != GameManager.Instance.player_list[ff[0]].current_hp)
        {
            if(GameManager.Instance.player_list[ff[0]].current_hp > 0)
            {
                hp[ff[0]].text = GameManager.Instance.player_list[ff[0]].current_hp.ToString();
                hit_anime[ff[0]].Play();
            }
            else { hp[ff[0]].text = 0.ToString(); }
        }
        if (int.Parse(hp[ff[1]].text) != GameManager.Instance.player_list[ff[1]].current_hp)
        {
            if(GameManager.Instance.player_list[ff[0]].current_hp > 0)
            {
                hp[ff[1]].text = GameManager.Instance.player_list[ff[1]].current_hp.ToString();
                hit_anime[ff[1]].Play();
            }
            else { hp[ff[1]].text = 0.ToString(); }
        }
        if (int.Parse(hp[ff[2]].text) != GameManager.Instance.player_list[ff[2]].current_hp)
        {
            if (GameManager.Instance.player_list[ff[0]].current_hp > 0)
            {
                hp[ff[2]].text = GameManager.Instance.player_list[ff[2]].current_hp.ToString();
                hit_anime[ff[2]].Play();
            }
            else { hp[ff[2]].text = 0.ToString(); }
        }
        if (int.Parse(hp[ff[3]].text) != GameManager.Instance.player_list[ff[3]].current_hp)
        {
            if (GameManager.Instance.player_list[ff[0]].current_hp > 0)
            {
                hp[ff[3]].text = GameManager.Instance.player_list[ff[3]].current_hp.ToString();
                hit_anime[ff[3]].Play();
            }
            else { hp[ff[3]].text = 0.ToString(); }
        }

        gun = ply.weapons[0].GetComponent<Range>();
        launcher = ply.weapons[1].GetComponent<Range>();
        spray = ply.weapons[2].GetComponent<RangeSpread>();
    }

    void Init_ff()
    {
        for(int i = 0; i < ff.Length; i++)
        {
            for(int j = 0; j < ff.Length; j++)
            {
                if(player_color[j] == color_bar[i].color)
                {
                    ff[i] = j;
                    break;
                }
            }
        }
    }

    void Hand_Icon()
    {
        if(ply.current_Hand != player_hand)
        {
            for(int i = 0; i < weapon_icon.Count; i++)
            {
                weapon_icon[i].enabled = false;
            }

            weapon_icon[ply.current_Hand].enabled = true;
        }
    }

    void Ammo_Setting()
    {
        if(ply.current_Hand != player_hand)
        {
            int a;
            switch(ply.current_Hand)
            {
                case 0:
                    max_ammo.text = gun.maxAmmo.ToString();
                    current_ammo.text = gun.ammo.ToString();
                    wbb = gun;
                    break;
                case 1:
                    max_ammo.text = launcher.maxAmmo.ToString();
                    current_ammo.text = launcher.ammo.ToString();
                    wbb = launcher;
                    break;
                case 2:
                    max_ammo.text = spray.maxAmmo.ToString();
                    current_ammo.text = spray.ammo.ToString();
                    wbb = spray;
                    break;
                case 3:
                case 4:
                case 5:
                case 6:
                    max_ammo.text = "-";
                    current_ammo.text = "-";
                    break;
            }

            player_hand = ply.current_Hand;
        }
        Ammo_Update();
    }

    void Ammo_Update()
    {
        switch (player_hand)
        {
            case 0:
                if (gun.ammo != int.Parse(current_ammo.text))
                    current_ammo.text = gun.ammo.ToString();
                break;
            case 1:
                if (launcher.ammo != int.Parse(current_ammo.text))
                    current_ammo.text = launcher.ammo.ToString();
                break;
            case 2:
                if (spray.ammo != int.Parse(current_ammo.text))
                    current_ammo.text = spray.ammo.ToString();
                break;
            default:
                current_ammo.text = "-";
                break;
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