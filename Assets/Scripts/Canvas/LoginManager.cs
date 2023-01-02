using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LoginManager : Singleton<LoginManager>
{
    [SerializeField]
    private GameObject loading_canvas;

    public bool login_check = true;

    void Start()
    {
        SceneFunction.loading_canvas = this.loading_canvas;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (login_check)
            {
                if (tmp_id_login.isFocused)
                {
                    //Tap to Write Focuse Change
                    tmp_id_login.Select();
                    tmp_pw_login.Select();
                }
                    
            }
            else if (!login_check)
            {
                for(int i = 0; i < re.Length; i++) 
                {
                    if (re[i].isFocused && i != 2) 
                    {
                        re[i].Select();
                        re[i + 1].Select();   
                    }
                }
            }
        }
    }


    public void Check(TMP_InputField text)
    {
        foreach (char a in text.text)
        {
            if (char.GetUnicodeCategory(a) == System.Globalization.UnicodeCategory.OtherLetter)
            {
                text.text = "";
                text.characterLimit = 8;
            }
        }
    }

    public TMP_InputField[] re;// for the Length Check

    public TMP_InputField tmp_nick_re;
    public TMP_InputField tmp_id_re;
    public TMP_InputField tmp_pw_re;
    public TMP_InputField tmp_id_login;
    public TMP_InputField tmp_pw_login;

    


    public Image lock_image_login;
    public Image lock_image_register;

    public void Value_Change_Check(bool is_login)
    {
        if (is_login)
        {
            if(tmp_id_login.text.Length == 8 && tmp_pw_login.text.Length >= 8)
            {
                lock_image_login.gameObject.SetActive(false);
            }
            else { lock_image_login.gameObject.SetActive(true); }
        }
        else
        {
            if(tmp_id_re.text.Length == 8 && tmp_pw_re.text.Length >= 8)
            {
                lock_image_register.gameObject.SetActive(false);
            }
            else { lock_image_register.gameObject.SetActive(true); }
        }
    }
}
