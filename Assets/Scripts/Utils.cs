using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Utils 
{
    public static bool is_select_room; //���� ���� �ߴ���?

    public static bool is_findroom;// �� ã����?

    public static int room_number; // ����

    public static string nickname;//NetWorkManager NickName;

    public static int ready_complete_player_index_inroom; // �غ� �Ϸ��� �÷��̾� �ε���

    public static bool is_ready;//�÷��̾� �غ� �Ϸ�?

    public static GameObject info_canvas;
    public static TMP_Text info_message;

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
