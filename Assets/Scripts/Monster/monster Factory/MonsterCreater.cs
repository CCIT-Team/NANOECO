using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract class MonsterCreater
{
    public List<Monster> monsters = new List<Monster>();

    public List<Monster> Get_Monster()
    {
        return monsters;
    }

    public abstract void Create_Monster();
}
