using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Event Data", menuName = "Scriptable Object/Even tData", order = int.MaxValue)]
public class EventData : ScriptableObject
{
    public enum E_map
    {

    }
    public E_map map;
    public List<GameObject> event_object;
}
