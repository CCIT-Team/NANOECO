using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Event Data", menuName = "Scriptable Object/Event tData", order = int.MaxValue)]
public class EventData : ScriptableObject
{
    public enum E_map
    {
        KITCHEN,
        BATHROOM,
        OFFICE
    }
    public E_map map;
    public int event_num;
    public List<GameObject> event_object;

    void Set_Map(int i)
    {
        switch(map)
        {
            case E_map.KITCHEN:
                Map_Kitchen kitchen = new Map_Kitchen(event_object);
                kitchen.Set_Event(i);
                break;
            case E_map.BATHROOM:
                Map_Bathroom bathroom = new Map_Bathroom(event_object);
                bathroom.Set_Event(i);
                break;
            case E_map.OFFICE:
                Map_Office office = new Map_Office(event_object);
                office.Set_Event(i);
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

class Map_Kitchen : ABMap
{
    public Map_Kitchen(List<GameObject> event_object)
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
        Instantiate(event_object[0]);
    }
}

class Map_Bathroom : ABMap
{
    public Map_Bathroom(List<GameObject> event_object)
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
        Instantiate(event_object[0]);
    }
}

class Map_Office : ABMap
{
    public Map_Office(List<GameObject> event_object)
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
        Instantiate(event_object[0]);
    }
}