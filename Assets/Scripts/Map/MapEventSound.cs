using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapEventSound : MonoBehaviour
{
    public GameObject sound;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 6)
        {
            sound.SetActive(true);
        }
    }
}
