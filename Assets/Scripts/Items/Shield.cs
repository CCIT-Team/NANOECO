using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Shield : MonoBehaviourPunCallbacks
{
    Material shieldM;
    NaNoPlayer player;
    float mHpRec;
    float hpRec;

    // Start is called before the first frame update
    void Start()
    {
        player = null;
        shieldM = GetComponent<Material>();
        StartCoroutine("DelayDestroy");
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            transform.position = player.transform.position;
            shieldM.color = new Color(0, 0, 0, 0);
            if (hpRec - 50 >= player.current_hp)
            {
                Destroy(this.gameObject);
            }
        }  
        else
            transform.Translate(Vector3.forward * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 6)
        {
            StopCoroutine("DelayDestroy");
            player = other.GetComponent<NaNoPlayer>();
            mHpRec = player.max_hp;
            player.max_hp += 50;
            player.current_hp += 50;
            hpRec = player.current_hp;
            StartCoroutine("OnShield");
        }
    }
    IEnumerator DelayDestroy()
    {
        yield return new WaitForSecondsRealtime(10);
        Destroy(this.gameObject);
    }

    IEnumerator OnShield()
    {
        yield return new WaitForSeconds(10);
        player.max_hp = mHpRec;
        if (player.current_hp > mHpRec)
            player.current_hp = player.max_hp;
        Destroy(this.gameObject);
    }
}
