using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public Character_Info[] cc;


    public int player_count;

}

[System.Serializable]
public class Character_Info
{
    public Character player;
}

