using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scenario : MonoBehaviour
{
    //MAP
    public string mission_name;
    public List<Event> events_list;

    public void Events_Setting()
    {

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
public class Event
{
    public string event_name;
    public List<EventData> event_data;
    public Trigger trigger;
}
