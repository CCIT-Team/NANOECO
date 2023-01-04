using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public Character_Info[] cc;
    public List<GameObject> testtest;
    public SpawnPoint sp;
    public Transform spawnPoint;
    public int player_count;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        sp = GameObject.FindGameObjectWithTag("Spawn").GetComponent<SpawnPoint>();
    }

    private void Update()
    {
        SpawnPointUpdate();
        if(sp == null || SceneManager.sceneCount == 2 && sp == null)
        {
            sp = GameObject.FindGameObjectWithTag("Spawn").GetComponent<SpawnPoint>();
        }
    }

    public void SpawnPointUpdate()
    {
        spawnPoint = sp.check_points[sp.current_spawn_point].transform;
    }

}

[System.Serializable]
public class Character_Info
{
    public Character player;
}

