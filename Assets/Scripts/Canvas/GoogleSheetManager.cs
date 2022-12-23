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
    const string URL = "https://script.google.com/macros/s/AKfycbx9EZ6CqrTPLPgpOZh0IFewhLZkAldcT9baNwSdQeiOOBWmhIK28ay4ddqW3jw6vUrCCQ/exec";
    public GoogleData GD;

    [Header("Login")]
    public InputField IDInput, PassInput;

    [Space(5)]

    [Header("Register")]
    public InputField IDInput_Register, PassInput_Register;
    [Space(5)]
    [Header("NickName_Input")]
    public InputField ValueInput;
    [Space(5)]
    string id, pass, id_re, pass_re;
    public TMP_Text text_message;

    [Space]
    [Header("login or register or NickName")]
    [SerializeField] GameObject sign_obj;
    [SerializeField] GameObject sign_up_panel;
    [SerializeField] GameObject NickName_Input;

    [Space]
    [Header("login or register")]
    [SerializeField] GameObject loading_circle;

    [SerializeField] GameObject for_the_text_obj;//�Ϸ� �� �˸� �ؽ�Ʈ ������
    

    bool[] asd;
    bool nickname_exist;

    void Awake()
    {
        Screen.SetResolution(1920, 1080, false);

        SceneFunction.Object_Check();// Fade_Canvas ã��� �ѹ�

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

        if (id_re == "" || pass_re == "") return false;
        else return true;
    }


    public void Register()
    {
        if (!SetIDPass_Register())
        {
            print("���̵� �Ǵ� ��й�ȣ�� ����ֽ��ϴ�");
            return;
        }

        WWWForm form = new WWWForm();
        form.AddField("order", "register");
        form.AddField("id", id_re);
        form.AddField("pass", pass_re);

        StartCoroutine(Post(form));
    }


    public void Login()
    {
        if (!SetIDPass_Login())
        {
            print("���̵� �Ǵ� ��й�ȣ�� ����ֽ��ϴ�");
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
        form.AddField("value", ValueInput.text);

        Debug.Log(ValueInput.text);
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
        using (UnityWebRequest www = UnityWebRequest.Post(URL, form)) // �ݵ�� using�� ����Ѵ�
        {

            if(end_check)
            www.uploadHandler.Dispose();

            yield return www.SendWebRequest();

            if (www.isDone) { Response(www.downloadHandler.text); loading_circle.SetActive(false); }
            else print("���� ������ �����ϴ�.");
        }
    }


    void Response(string json)
    {
        if (string.IsNullOrEmpty(json)) return;

        GD = JsonUtility.FromJson<GoogleData>(json);

        if (GD.result == "ERROR")
        {
            if (GD.order == "setValue")
            {
                for_the_text_obj.SetActive(true);
                text_message.text = "�̹� �����ϴ� �г����Դϴ�.";
            }

            else if (GD.order == "register")
            {
                NickName_Input.SetActive(true);
                id_re = "";
                pass_re = "";
            }

            else
                print(GD.order + "�� ������ �� �����ϴ�. ���� �޽��� : " + GD.msg);

            return;
        }

        print(GD.order + "�� �����߽��ϴ�. �޽��� : " + GD.msg);

        if(GD.order == "register")
        {
            sign_up_panel.SetActive(false);
            sign_obj.SetActive(true);
            id_re = "";
            pass_re = "";
        }


        if(GD.order == "login")
        {
            if(GD.msg == "�г��� ����")
            {
                sign_obj.SetActive(false);
                NickName_Input.SetActive(true);
            }
            else// �г��� ����
            {
                sign_obj.SetActive(false);

                Utils.nickname = GD.msg;
                SceneFunction.Fade_Out();
               
            }
        }

        if(GD.order == "setValue")
        {
            nickname_exist = true;
            for_the_text_obj.SetActive(true);
            text_message.text = "�г��� ���� �Ϸ�!";
        }

        if (GD.order == "getValue")
        {
            for_the_text_obj.SetActive(true);
            text_message.text = "������ ������ �غ� �Ϸ�Ǿ����ϴ�!";
        }
    }

    //

    public void SetActive_False(GameObject a)
    {
        a.SetActive(false);
        ValueInput.text = "";

        if (nickname_exist)
        {
            Utils.nickname = GD.msg;
            SceneFunction.Fade_Out();
        }
    }
}