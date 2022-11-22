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
            event_data.event_point = spawn_point;
            scenario.On_Trigger = event_data.Send_Event;
            scenario.On_Trigger();
            is_run = true;
        }
    }
}
