using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransportTarget : MonoBehaviour
{
    public TransportMission path;

    void OnTriggerEnter(Collider other)
    {
        path._active_count++;
    }

    void OnTriggerExit(Collider other)
    {
        path._active_count--;
    }
}