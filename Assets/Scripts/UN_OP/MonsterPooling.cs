using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
public class MonsterPooling : MonoBehaviour
{
    [SerializeField]
    private GameObject[] monters;
    private IObjectPool<MonsterBase> _Pool;
    [SerializeField]
    private Transform par;

    private void Awake()
    {
        _Pool = new ObjectPool<MonsterBase>(Create_Mons, Get_Mons, Release_Mons, Destroy_Mons, maxSize: 50 );
    }

    private MonsterBase Create_Mons()
    {
        MonsterBase Mons = Instantiate(monters[0],par).GetComponent<MonsterBase>();
        Mons.Set_Managed_Pool(_Pool);
        return Mons;
    }

    private void Get_Mons(MonsterBase mons)
    {
        mons.gameObject.SetActive(true);
    }

    private void Release_Mons(MonsterBase mons)
    {
        mons.gameObject.SetActive(false);
    }

    private void Destroy_Mons(MonsterBase mons)
    {
        Destroy(mons.gameObject);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            var simplemon = _Pool.Get();
        }
        if(Input.GetKeyDown(KeyCode.K))
        {
            
        }
    }
}
