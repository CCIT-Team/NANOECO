using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class ItemControler : MonoBehaviourPunCallbacks
{
    public WeaponeBase.ItemID IID = WeaponeBase.ItemID.ENONE;
    public float cooldown = 0;
    public bool iscooldown = false;
    public int maxcount = 0;
    public int count = 0;
    protected Vector3 target;

    public GameObject itemprefab;
    public GameObject useditem;

    public PhotonView pv;
    protected Player player;

    public virtual void Start()
    {
        GetPlayer();
        count = maxcount;
    }
    override public void OnEnable()
    {
        if(iscooldown)
        {
            PhotonNetwork.AddCallbackTarget(this);
            StartCoroutine("Cooling");
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (pv.IsMine)
        {
            if (Input.GetMouseButtonDown(0) && !player.is_dead && !player.is_usehand && !iscooldown)
            {
                if (count == 0)
                    return;
                else
                {
                    iscooldown = true;
                    Useitem(); 
                }
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
        PhotonNetwork.Instantiate(itemprefab.name, this.transform.position, itemprefab.transform.rotation);
        count--;
        StartCoroutine("Cooling");
    }

    void GetPlayer()
    {
        pv = GetComponent<PhotonView>();
        player = gameObject.transform.root.GetComponent<Player>();
    }
}
