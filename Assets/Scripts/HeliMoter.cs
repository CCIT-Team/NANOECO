using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeliMoter : MonoBehaviour
{
    [Space]
    [Header("Çï±â")]
    public Transform rotor;
    public Transform tail_rotor;
    public float rotor_speed;
    public float tail_rotor_speed;

    void Update()
    {
        rotor.eulerAngles = new Vector3(rotor.eulerAngles.x, rotor.eulerAngles.y + Time.deltaTime * rotor_speed, rotor.eulerAngles.z);
        tail_rotor.eulerAngles = new Vector3(tail_rotor.eulerAngles.x, tail_rotor.eulerAngles.y, tail_rotor.eulerAngles.z + Time.deltaTime * tail_rotor_speed);
    }
}
