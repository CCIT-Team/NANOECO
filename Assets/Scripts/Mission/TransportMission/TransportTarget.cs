using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransportTarget : MonoBehaviour
{
    public TransportMission path;

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 6)
            path._active_count++;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 6)
            path._active_count--;
    }
}