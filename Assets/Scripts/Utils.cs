using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils 
{
    public static string nickname;//NetWorkManager NickName;

    public static Vector3 mouse_pos//Mouse Transform
    {
        get
        {
            Vector3 result = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            result.z = -10;
            return result;
        }
    }
}
