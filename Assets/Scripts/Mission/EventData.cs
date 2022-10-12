using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Event Data", menuName = "Scriptable Object/Event tData", order = int.MaxValue)]
public class EventData : ScriptableObject
{
    public enum E_map
    {
        MAP_0,
        MAP_1,
        MAP_2
    }
    public E_map map;
    public int event_num;
    public List<GameObject> event_object;

    void Set_Map(int i)
    {
        switch(map)
        {
            case E_map.MAP_0:
                Map_0 map_0 = new Map_0(event_object);
                map_0.Set_Event(i);
                break;
            case E_map.MAP_1:
                break;
            case E_map.MAP_2:
                break;
        }
    }

    public void Send_Event()
    {
        Set_Map(event_num);
    }
}

abstract class ABMap : MonoBehaviour
{
    protected List<GameObject> event_object;

    public abstract void Set_Event(int i);
}

class Map_0 : ABMap
{
    public Map_0(List<GameObject> event_object)
    {
        this.event_object = event_object;
    }

    public override void Set_Event(int i)
    {
        switch (i)
        {
            case 0:
                test_event();
                break;
            case 1:
                break;
            case 2:
                break;
            case 3:
                break;
        }
    }

    void test_event()
    {
        Debug.Log(123);
        Instantiate(event_object[0]);
    }
}