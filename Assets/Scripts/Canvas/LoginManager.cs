using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LoginManager : MonoBehaviour
{
    [SerializeField]
    private GameObject loading_canvas;

    public TMP_InputField[] tmp_if;

    void Start()
    {
        SceneFunction.loading_canvas = this.loading_canvas;

        for(int i=0;i < tmp_if.Length;i++) 
        {
            tmp_if[i].characterLimit = 8;
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
}
