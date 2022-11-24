using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class ItemControler : MonoBehaviourPunCallbacks
{
    public float cooldown = 0;
    public bool iscooldown = false;
    public int maxcount = 0;
    public int count = 0;
    protected Vector3 target;

    public GameObject itemprefab;
    public GameObject useditem;

    public PhotonView pv;

    void Start()
    {
        pv = PhotonTestPlayer.instance.pv;
        count = maxcount;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0)&&!iscooldown && pv.IsMine)
        {
            if (count == 0)
                return;
            else
            {
                iscooldown = true;
                Useitem();
                StartCoroutine("Cooling");
            }
        }
    }

    IEnumerator Cooling()
    {
        yield return new WaitForSecondsRealtime(cooldown);
        /*cooldowncounting = cooldown;
        for (; cooldowncounting > 0; cooldowncounting-- )
        {
            yield return new WaitForSecondsRealtime(1);
            Debug.Log("남은시간" + cooldowncounting + "초");
        }*/
        iscooldown = false;
    }

    public virtual void Useitem()
    {
        useditem = Instantiate(itemprefab);
        useditem.transform.position = this.transform.position;
        count--;
    }
}
