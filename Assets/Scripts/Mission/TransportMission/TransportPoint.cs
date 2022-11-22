using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransportPoint : MonoBehaviour
{
    public Vector3 position { get { return transform.position; } }
    public Vector3 rotation { get { return transform.eulerAngles; } }
    public GameObject curve_point;
    public bool is_curve;
}
