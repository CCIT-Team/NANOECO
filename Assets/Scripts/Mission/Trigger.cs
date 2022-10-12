using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    public Scenario scenario;
    public EventData event_data;

    void OnTriggerEnter(Collider other)
    {
        scenario.On_Trigger = event_data.Send_Event;
        scenario.On_Trigger();
    }
}
