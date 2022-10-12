using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scenario : MonoBehaviour
{
    //테스트용
    void Awake()
    {
        Events_Setting();
    }

    //MAP
    public string mission_name;
    public List<Events> events_list;

    public void Events_Setting()
    {
        foreach (var item in events_list)
        {
            item.trigger.scenario = this;

            int i = Random.Range(0, item.event_data_list.Count);
            item.trigger.event_data = item.event_data_list[i];
        }
    }

    public delegate void Get_Trigger();
    public Get_Trigger On_Trigger;
}

[System.Serializable]
public class Mission
{
    public enum E_mission_type { MAIN, SUB }
    public E_mission_type mission_type;
}

[System.Serializable]
public class Events
{
    public string event_name;
    public List<EventData> event_data_list;
    public Trigger trigger;
}
