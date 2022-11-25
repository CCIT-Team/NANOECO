using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Shield : ItemControler
{
    public Material a;
    PhotonTestPlayer player;
    float currenthp = 0;
    float stack = 0;
    bool shieldon = false;

    // Start is called before the first frame update
    void Start()
    {
        player = pv.GetComponent<PhotonTestPlayer>();
        currenthp = player.current_hp;
    }

    // Update is called once per frame
    void Update()
    {
        if(currenthp > player.current_hp && shieldon)
        {
            stack += (currenthp - player.current_hp);
            a.color += new Color(stack, -stack, 0, 0);
            currenthp = player.current_hp;
        }
        if (Input.GetMouseButtonDown(0) && pv.IsMine && !shieldon && !iscooldown)
        {
            currenthp = player.current_hp;
            shieldon = true;
            StartCoroutine("OnShield");
            StartCoroutine("AttackDelay");
        }
    }

    IEnumerator OnShield()
    {
        yield return new WaitForSeconds(10);
        shieldon = false;
        player.current_hp += stack;
    }

    /*private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 8 && isdelay)
        {
            other.gameObject.GetComponent<Rigidbody>().AddForce(knockback * Vector3.Normalize(other.transform.position - player.transform.position), ForceMode.Impulse);
            other.gameObject.GetComponent<Character>().current_hp -= stack;
        }
    }*/
}
