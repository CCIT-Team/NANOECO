using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapEvent : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        //��ũ��, �⸧��, ����
        if(other.gameObject.layer == 6)
        {
            var player = other.GetComponent<NaNoPlayer>();
            player.is_dead = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        //�׸�
        if (other.gameObject.layer == 6)
        {
            var player = other.GetComponent<NaNoPlayer>();
            player.current_hp -= 1f;
        }
    }
}
