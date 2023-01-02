using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelicopterAniEvent : MonoBehaviour
{
    public HelicopterEvent he;

    void Event_0()
    {
        he.Ride_Player();
    }

    void Event_1()
    {
        he.Arrived_Player();
    }
}
