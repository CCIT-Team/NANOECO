using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonManager : MonoBehaviour
{
    [SerializeField]
    List<AbsMonsterBase> mon_data = new List<AbsMonsterBase>();

    NomalMonster nomalMonster;
    private void Start()
    {
        
    }
}
