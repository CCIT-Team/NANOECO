using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Runtime.InteropServices;
using System;
using UnityEditor;
using System.Net;
using TMPro;

[System.Serializable]
public class GoogleData
{
    public string order, result, msg, value;
}


public class GoogleSheetManager : MonoBehaviour
{
    const string URL = "https://script.google.com/macros/s/AKfycbxv9uFXeLUGgSBG10kIGoDcMi2efxYOu-ZoP2Z7hfBsjjcaymw4I9ZnU8VAXfc9STxaMw/exec";
    public GoogleData GD;

    [Header("Login")]
    public TMP_InputField IDInput, PassInput;

    [Space(5)]

    [Header("Register")]
    public TMP_InputField IDInput_Register, PassInput_Register;
    public TMP_InputField NICKNAMEInput_Register;
    [Space(5)]
    string id, pass, id_re, pass_re, nickname_re;
    [Space(5)]
    public TMP_Text text_message;

    [Space]
    [Header("login or register or NickName")]
    [SerializeField] GameObject sign_obj;
    [SerializeField] GameObject sign_up_panel;
    [SerializeField] GameObject NickName_Input;

    [Space]
    [Header("login or register")]
    [SerializeField] GameObject loading_circle;

    [SerializeField] GameObject info_canvas;//완료 및 알림 텍스트 오브젝



    bool[] asd;
    bool nickname_exist;

    void Awake()
    {
        Screen.SetResolution(1920, 1080, false);

        SceneFunction.Object_Check();// Fade_Canvas 찾기용 한번

        //asd[0] = nickname_exist;
    }

    public void Turn_Sign_Up_Panel()
    {
        id = string.Empty; pass = string.Empty;
        sign_obj.SetActive(false);
        sign_up_panel.SetActive(true);
    }

    public void Turn_Sign()
    {
        id_re = string.Empty; pass_re = string.Empty;
        sign_obj.SetActive(true);
        sign_up_panel.SetActive(false);
    }


    bool SetIDPass_Login()//login
    {
        id = IDInput.text.Trim();
        pass = PassInput.text.Trim();

        if (id == "" || pass == "") return false;
        else return true;
    }

    bool SetIDPass_Register()//Register
    {
        id_re = IDInput_Register.text.Trim();
        pass_re = PassInput_Register.text.Trim();
        nickname_re = NICKNAMEInput_Register.text.Trim();

        if (id_re == "" || pass_re == "" || nickname_re == "") return false;
        else return true;
    }


    public void Register()
    {
        if (!SetIDPass_Register())
        {
            print("아이디 또는 비밀번호 또는 닉네임이 비어있습니다");
            return;
        }

        WWWForm form = new WWWForm();
        form.AddField("order", "register");
        form.AddField("id", id_re);
        form.AddField("pass", pass_re);
        form.AddField("value", nickname_re);

        StartCoroutine(Post(form));
    }


    public void Login()
    {
        if (!SetIDPass_Login())
        {
            print("아이디 또는 비밀번호가 비어있습니다");
            return;
        }

        WWWForm form = new WWWForm();
        form.AddField("order", "login");
        form.AddField("id", id);
        form.AddField("pass", pass);

        StartCoroutine(Post(form));
    }


    void OnApplicationQuit()
    {
        WWWForm form = new WWWForm();
        form.AddField("order", "logout");

        end_check = true;
        StartCoroutine(Post(form));
    }


    public void SetValue()
    {
        WWWForm form = new WWWForm();
        form.AddField("order", "setValue");
        form.AddField("value", NICKNAMEInput_Register.text);

        //Debug.Log(NICKNAMEInput_Register.text);
        StartCoroutine(Post(form));
    }

    public void GetValue()
    {
        WWWForm form = new WWWForm();
        form.AddField("order", "getValue");

        StartCoroutine(Post(form));
    }


    bool end_check = false;
    [RuntimeInitializeOnLoadMethod]
    IEnumerator Post(WWWForm form)
    {
        loading_circle.SetActive(true);
        using (UnityWebRequest www = UnityWebRequest.Post(URL, form)) // 반드시 using을 써야한다
        {

            if(end_check)
            www.uploadHandler.Dispose();

            yield return www.SendWebRequest();

            if (www.isDone) { Response(www.downloadHandler.text); loading_circle.SetActive(false); }
            else print("웹의 응답이 없습니다.");
        }
    }


    void Response(string json)
    {
        if (string.IsNullOrEmpty(json)) return;

        GD = JsonUtility.FromJson<GoogleData>(json);

        if (GD.result == "ERROR")
        {
            if (GD.order == "register")
            {
                NickName_Input.SetActive(true);
                id_re = "";
                pass_re = "";
            }

            else
                print(GD.order + "을 실행할 수 없습니다. 에러 메시지 : " + GD.msg);

            return;
        }

        print(GD.order + "을 실행했습니다. 메시지 : " + GD.msg);

        if(GD.order == "register")
        {
            sign_up_panel.SetActive(false);
            sign_obj.SetActive(true);
            id_re = "";
            pass_re = "";
            //SetValue();
        }


        if(GD.order == "login")
        {
            if(GD.msg == "닉네임 없음")
            {
                sign_obj.SetActive(false);
                NickName_Input.SetActive(true);
            }
            else// 닉네임 있음
            {
                sign_obj.SetActive(false);

                Utils.nickname = GD.msg;
                SceneFunction.Fade_Out();
               
            }
        }

        if (GD.order == "getValue")
        {
            info_canvas.SetActive(true);
            text_message.text = "게임을 시작할 준비가 완료되었습니다!";
        }
    }

    //

    public void SetActive_False(GameObject a)
    {
        a.SetActive(false);
        NICKNAMEInput_Register.text = "";

        if (nickname_exist)
        {
            Utils.nickname = GD.msg;
            SceneFunction.Fade_Out();
        }
    }
}