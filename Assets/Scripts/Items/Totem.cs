using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Totem : MonoBehaviour
{
    public int maxeffectcount = 5;
    public int effectcount = 0;
    public float effectamount = 10;
    SphereCollider effectaround;

    void Start()
    {
        effectaround = GetComponent<SphereCollider>();
        new WaitForSecondsRealtime(1);
        StartCoroutine("Effectdelay");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.gameObject.GetComponent<Character>().current_hp += effectamount;
        }
    }

    IEnumerator Effectdelay()
    {
        for (; effectcount < maxeffectcount; ++effectcount)
        {
            effectaround.enabled = true;
            yield return new WaitForSecondsRealtime(1);
            effectaround.enabled = false;
            yield return new WaitForSecondsRealtime(4.5f);
        }
        Destroy(this.gameObject);
    }
}
