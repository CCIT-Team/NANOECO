using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseFactory : MonoBehaviour
{
    MonsterCreater[] monstercreaters = null;

    private void Start()
    {
        monstercreaters = new MonsterCreater[2];
        monstercreaters[0] = new MonsterGenerator_A();
        monstercreaters[1] = new MonsterGenerator_B();
    }

    public void Make_TypeA()
    {
        monstercreaters[0].Create_Monster();

        List<Monster> monsters = monstercreaters[0].Get_Monster();
        foreach(Monster monster in monsters)
        {
            //monster.Chase();
        }
    }

    public void Make_TypeB()
    {
        monstercreaters[1].Create_Monster();

        List<Monster> monsters = monstercreaters[1].Get_Monster();
        foreach (Monster monster in monsters)
        {
           // monster.Chase();
        }
    }

    private void Update()
    {
        Make_TypeA();
        Make_TypeB();
        
    }
}
