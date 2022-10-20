using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    public Scenario scenario;
    public EventData event_data;
    public List<GameObject> spawn_point;
    public bool is_run;

    void OnTriggerEnter(Collider other)
    {
        if(!is_run && other.CompareTag("Player"))
        {
            print(0);
            event_data.event_point = spawn_point;
            print(1);
            scenario.On_Trigger = event_data.Send_Event;
            scenario.On_Trigger();
            is_run = true;
        }
    }
}
