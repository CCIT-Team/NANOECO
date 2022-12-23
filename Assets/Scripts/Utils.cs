using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils 
{
    public static bool is_select_room; //맵을 선택 했는지?

    public static bool is_inRoom; // 룸에 진입 했는지?
     
    public static int room_number; // 방제

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
