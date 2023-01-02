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
    const string URL = "https://script.google.com/macros/s/AKfycbxSdZgxBLpXoTW-wH_ZgR1_neJnleyJuay2mx5CNs4d1LO3VzYnwZvC6oqjGmx92f-02w/exec";
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

    [Space]
    [Header("login or register")]
    [SerializeField] GameObject loading_circle;
    [SerializeField] GameObject info_canvas;//�Ϸ� �� �˸� �ؽ�Ʈ ������


    bool nickname_exist;

    void Awake()
    {
        Screen.SetResolution(1920, 1080, false);

        SceneFunction.Object_Check();// Fade_Canvas ã��� �ѹ�
    }

    private void Start()
    {
        Utils.info_canvas = info_canvas;
        Utils.info_message = text_message;
    }

    public void Turn_Sign_Up_Panel()
    {
        id = string.Empty; pass = string.Empty;
        sign_obj.SetActive(false);
        sign_up_panel.SetActive(true);
        LoginManager.Instance.login_check = false;
    }

    public void Turn_Sign()
    {
        id_re = string.Empty; pass_re = string.Empty;
        sign_obj.SetActive(true);
        sign_up_panel.SetActive(false);
        LoginManager.Instance.login_check = true;
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
            print("���̵� �Ǵ� ��й�ȣ �Ǵ� �г����� ����ֽ��ϴ�");
            text_message.text = "Please Fill in All the Blanks!";
            info_canvas.SetActive(true);
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
            print("���̵� �Ǵ� ��й�ȣ�� ����ֽ��ϴ�");
            text_message.text = "Please Write ID or Password.";
            info_canvas.SetActive(true);
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
        using (UnityWebRequest www = UnityWebRequest.Post(URL, form)) // �ݵ�� using�� ����Ѵ�
        {

            if(end_check)
            www.uploadHandler.Dispose();

            yield return www.SendWebRequest();

            if (www.isDone) { Response(www.downloadHandler.text); loading_circle.SetActive(false); }
            else
            {
                print("���� ������ �����ϴ�.");
                text_message.text = "���� ������ �����ϴ�.";
                info_canvas.SetActive(true);
            }
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
                text_message.text = GD.msg;
                info_canvas.SetActive(true);

                id_re = "";
                pass_re = "";
                nickname_re = "";
                IDInput_Register.text = "";
                PassInput_Register.text = "";
                NICKNAMEInput_Register.text = "";
                LoginManager.Instance.lock_image_register.gameObject.SetActive(true);
            }

            else
            {
                if(GD.order == "login")
                {
                    id = "";
                    pass = "";
                    IDInput.text = "";
                    PassInput.text = "";
                    LoginManager.Instance.lock_image_login.gameObject.SetActive(true);
                }
                print(GD.order + "�� ������ �� �����ϴ�. ���� �޽��� : " + GD.msg);
                text_message.text = GD.msg;
                info_canvas.SetActive(true);
            }

            return;
        }

        print(GD.order + "�� �����߽��ϴ�. �޽��� : " + GD.msg);

        if(GD.order == "register")
        {
            sign_up_panel.SetActive(false);
            sign_obj.SetActive(true);
            id_re = "";
            pass_re = "";
            nickname_re = "";
            IDInput_Register.text = "";
            PassInput_Register.text = "";
            NICKNAMEInput_Register.text = "";
            LoginManager.Instance.lock_image_register.gameObject.SetActive(true);
            //SetValue();
        }


        if(GD.order == "login")
        {
            if(GD.msg == "�г��� ����")
            {
                sign_obj.SetActive(false);
            }
            else// �г��� ����
            {
                sign_obj.SetActive(false);

                Utils.nickname = GD.msg;
                SceneFunction.Fade_Out();
               
            }
        }

        if (GD.order == "getValue")
        {
            info_canvas.SetActive(true);
            text_message.text = "������ ������ �غ� �Ϸ�Ǿ����ϴ�!";
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