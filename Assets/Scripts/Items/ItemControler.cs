using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemControler : MonoBehaviour
{
    public float cooldown = 0;
    public float cooldowncounting = 0;
    public bool iscooldown = false;
    public int maxcount = 0;
    public int count = 0;
    protected Vector3 target;

    public GameObject itemprefab;
    public GameObject useditem;

    void Start()
    {
        count = maxcount;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0)&&!iscooldown)
        {
            if (count == 0)
                Debug.Log("부족");
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
        cooldowncounting = cooldown;
        for (; cooldowncounting > 0; cooldowncounting-- )
        {
            yield return new WaitForSecondsRealtime(1);
            Debug.Log("남은시간" + cooldowncounting + "초");
        }
        iscooldown = false;
    }

    public virtual void Useitem()
    {

    }
}
