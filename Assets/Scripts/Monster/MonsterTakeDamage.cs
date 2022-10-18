using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterTakeDamage : MonoBehaviour
{
    public MonsterBase monbase;
    public CloseMonster closemonster;

    private void Start()
    {
        monbase = GetComponent<MonsterBase>();
    }
    private void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
           
        }
    }
}
