using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonManager : MonoBehaviour
{
    [SerializeField]
    List<NomalMonster> nomalMonsters = new List<NomalMonster>();
    NomalMonster nomalmonster;
    [SerializeField]
    List<NomalFarMonster> nomalFarMonsters = new List<NomalFarMonster>();
    NomalFarMonster NomalFarMonster;


    private void Start()
    {
        nomalMonsters.Add(nomalmonster);
        nomalFarMonsters.Add(NomalFarMonster);
    }
}
