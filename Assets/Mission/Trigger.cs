using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    public Scenario scenario;
    public EventData event_data;

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(""))
        {
            scenario.On_Trigger();              //수정해야됌
        }
    }
}
