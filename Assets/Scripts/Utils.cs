using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils 
{
    public static bool is_inRoom;

    public static int room_number;

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

    public static float Ran()
    {
        int num = Random.Range(10000000, 99999999);
        room_number = num;
        return 0;
    }
}
