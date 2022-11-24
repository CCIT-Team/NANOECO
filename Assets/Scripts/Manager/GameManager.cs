using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public Character_Info[] cc;
    public List<GameObject> testtest;

    public int player_count;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

}

[System.Serializable]
public class Character_Info
{
    public Character player;
}

