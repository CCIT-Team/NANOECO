using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InGameUI : MonoBehaviour
{
    public List<MissionBox> mission_box_list;
    public MissionSystem ms;

    // Start is called before the first frame update
    void Awake()
    {
        ms.Mission_Box_Update(mission_box_list);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad0)) { ms.Mission_Clear(0); }
        if (Input.GetKeyDown(KeyCode.Keypad1)) { ms.Mission_Clear(1); }
        if (Input.GetKeyDown(KeyCode.Keypad2)) { ms.Mission_Clear(2); }
        if (Input.GetKeyDown(KeyCode.Keypad3)) { ms.Mission_Clear(3); }
    }
}

[System.Serializable]
public class MissionBox
{
    public InGameUI ig;
    public TextMeshProUGUI mission_text;
    public Animation anime;
}