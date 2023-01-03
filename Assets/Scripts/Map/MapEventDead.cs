using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapEventDead : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        //��ũ��, �⸧��, ����
        if (other.gameObject.layer == 6)
        {
            var player = other.GetComponent<Player>();
            player.is_dead = true;
        }
    }
}
