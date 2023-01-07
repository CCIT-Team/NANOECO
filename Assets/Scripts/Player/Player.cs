using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;
using Photon.Realtime;
using Photon.Pun.Demo.PunBasics;

public class Player : MonoBehaviourPunCallbacks, IPunObservable
{
    public static Player instance;
    public int player_actornum;
    int targetdisplay = 0;
    public Rigidbody rigid;
    public PhotonView pv;
    EPlayer_Skil eps;
    public int camera_shaking_num;
    [Header("UI")]
    public TextMeshProUGUI nickname;
    public Image playerIndicator;
    [Header("Status")]
    public float max_hp;
    public float current_hp;
    public float jump_force;
    public float dash_force;
    public float move_force;
    public bool is_dead;
    public float respawn_time = 5;
    public int skil_num;
    bool is_dash = false;
    public bool isGrounded = true;
    bool is_dontHit = false;
    [Header("아이템")]
    public GameObject[] weapons = new GameObject[3];
    //0 = Difuser
    //1 = Launcher
    //2 = Spray
    public GameObject[] gargets = new GameObject[4];
    //0 = Bomb
    //1 = Dummy
    //2 = Healing Bomb
    //3 = Heal Totam
    public GameObject hand;
    bool is_usehand = false;
    public GameObject[] inventory;
    int current_item = 0;
    public int current_Hand = -1;
    public int current_Garget;
    public int current_Weapon;
    string weapon_String;
    //0 = 기본총
    //1 = 런처
    //2 = 스프레이
    //3 = 근접무기
    //4 = item
    string garget_String;
    [Header("스폰포인트")]
    public Transform[] firstSpawnPoint = new Transform[4];
    int spawnNum = 0;
    public Transform spawn_point;
    [Header("애니메이션 관련")]
    public Animator ani;
    public Animator helicopterAni;
    [Header("죽음 애니메이션")]
    public GameObject helicopter;
    public GameObject helicopterrope;
    public GameObject helicopterplayerbody;
    public GameObject originPlayer;
    public bool isunrideheli = false;

    public float r;
    public float g;
    public float b;
    public float a;

    Vector3 curPos;
    Quaternion curRot;
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
            stream.SendNext(current_hp);
            stream.SendNext(is_dead);
            stream.SendNext(current_item);
            stream.SendNext(r);
            stream.SendNext(g);
            stream.SendNext(b);
            stream.SendNext(a);
        }
        else
        {
            curPos = (Vector3)stream.ReceiveNext();
            curRot = (Quaternion)stream.ReceiveNext();
            current_hp = (float)stream.ReceiveNext();
            is_dead = (bool)stream.ReceiveNext();
            current_item = (int)stream.ReceiveNext();
            r = (float)stream.ReceiveNext();
            g = (float)stream.ReceiveNext();
            b = (float)stream.ReceiveNext();
            a = (float)stream.ReceiveNext();
        }
    }

    void Awake()
    {
        nickname.text = pv.IsMine ? PhotonNetwork.NickName : pv.Owner.NickName;
        if (pv.IsMine)
        {
            if (GameManager.Instance.players[GameManager.Instance.playersnum] == null)
            {
                GameManager.Instance.players[GameManager.Instance.playersnum] = this;
            }
            else { GameManager.Instance.playersnum += 1; }
        }
        nickname.color = pv.IsMine ? Color.green : Color.red;
        if (pv.IsMine)
        {
            gameObject.name = nickname.text;
            //cam.gameObject.name = nickname.text + "cam";
            //cam = GameObject.Find(nickname.text + "cam").GetComponent<Camera>();
            Camera.main.GetComponent<PlayerCamera>().player = gameObject.transform;
        }
        instance = this;

        for (int i = 0; i < 3; i++)
        {
            firstSpawnPoint[i] = GameObject.FindGameObjectWithTag("FirstSpawnPoint").transform.Find("1").transform;
            firstSpawnPoint[i] = GameObject.FindGameObjectWithTag("FirstSpawnPoint").transform.Find("2").transform;
            firstSpawnPoint[i] = GameObject.FindGameObjectWithTag("FirstSpawnPoint").transform.Find("3").transform;
            firstSpawnPoint[i] = GameObject.FindGameObjectWithTag("FirstSpawnPoint").transform.Find("4").transform;
        }

        for (int i = 0; i < 3; i++)
        {
            weapons[i].SetActive(false);
        }

        for (int i = 0; i < 4; i++)
        {
            gargets[i].SetActive(false);
        }

        switch (current_Weapon)
        {
            case 0:
                weapon_String = "Difuser";
                weapons[0].SetActive(true);
                inventory[0] = weapons[0];
                current_Hand = 0;
                break;
            case 1:
                weapon_String = "Launcher";
                weapons[1].SetActive(true);
                inventory[0] = weapons[1];
                current_Hand = 1;
                break;
            case 2:
                weapon_String = "Spray";
                weapons[2].SetActive(true);
                inventory[0] = weapons[2];
                current_Hand = 2;
                break;
        }
        switch (current_Garget)
        {    //0 = Bomb
             //1 = Dummy
             //2 = Healing Bomb
             //3 = Heal Totam
            case 0:
                garget_String = "Bomb";
                gargets[0].SetActive(true);
                current_Hand = 3;
                break;
            case 1:
                garget_String = "Dummy";
                gargets[1].SetActive(true);
                current_Hand = 4;
                break;
            case 2:
                garget_String = "Healing Bomb";
                gargets[2].SetActive(true);
                current_Hand = 5;
                break;
            case 3:
                garget_String = "Heal Totam";
                gargets[3].SetActive(true);
                current_Hand = 6;
                break;
        }
    }

    void Start()
    {
        PhotonNetwork.CurrentRoom.IsOpen = false;
        Point_Color();
        Skil();
        is_dead = false;
        inventory[0].SetActive(true);
        inventory[1].SetActive(false);
        inventory[2].SetActive(false);
        helicopter.SetActive(false);
        if (firstSpawnPoint[spawnNum] != null)
        {
            spawnNum += 1;
        }
        spawn_point = firstSpawnPoint[spawnNum];
    }

    void Update()
    {
        if (pv.IsMine && PhotonNetwork.IsConnected && !is_dead) { Move(); }
        if (pv.IsMine) { ItemChange(); }
        if (Input.GetKeyDown(KeyCode.Escape)) { Application.Quit(); }
        SpawnPointUpdate();
        Dead();
        if (helicopterAni.GetBool("Respawn"))
        {
            ReSpawn();
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            current_Weapon = 0;
            inventory[0] = weapons[0];
            inventory[1] = gargets[0];
            inventory[2] = gargets[3];
        }
    }

    void Dead()
    {
        if (current_hp <= 0 || is_dead == true)
        {
            current_hp = 0;
            is_dead = true;
            ani.SetTrigger("Dead");
            helicopter.SetActive(true);
            helicopterplayerbody.transform.parent = helicopterrope.transform;
            // helicopterplayerbody.transform.localPosition = new Vector3(0, 0, 0);
        }

        if (spawn_point == null) { spawn_point = firstSpawnPoint[spawnNum]; }
        if (spawnNum > 3) { spawnNum = 0; }
    }

    void ReSpawn()
    {
        if (is_dead)
        {
            respawn_time -= Time.deltaTime;
            if (respawn_time <= 0) { transform.position = spawn_point.position; }
            if (isunrideheli == true)
            {
                Debug.Log("내려 내려 내려");
                helicopterplayerbody.SetActive(true);
                helicopterrope.transform.DetachChildren();
                helicopterplayerbody.transform.parent = originPlayer.transform;
            }
            if (helicopterAni.GetBool("HliEnd"))
            {
                current_hp = max_hp;
                respawn_time = 3;
                DontHitTime(3);
                isunrideheli = false;
                is_dead = false;
                helicopterplayerbody.SetActive(true);
                helicopterrope.transform.DetachChildren();
                helicopterplayerbody.transform.parent = originPlayer.transform;
                helicopter.SetActive(false);
            }
        }
    }

    void DontHitTime(float time)
    {
        is_dontHit = true;
        if (is_dontHit)
        {
            current_hp = max_hp;
            time -= Time.deltaTime;
            if (timer <= 0)
            {
                is_dontHit = false;
            }
        }
    }

    void Move()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 move = new Vector3(-horizontal * move_force * Time.deltaTime, 0, -vertical * move_force * Time.deltaTime);

        transform.position += move;
        if (horizontal > 0 || horizontal < 0 || vertical > 0 || vertical < 0)
        {
            ani.SetBool("Run", true);
        }
        else
        {
            ani.SetBool("Run", false);
        }
        Jump();
        Dash();
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded == true)//점프
        {
            isGrounded = false;
            rigid.AddForce(Vector3.up * jump_force, ForceMode.Impulse);
        }
    }
    float timer = 5;
    void Dash()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && isGrounded == true && is_dash == false)//대쉬
        {
            Debug.Log("대쉬");
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            Vector3 dash = new Vector3(-horizontal * dash_force * Time.deltaTime, 0, -vertical * dash_force * Time.deltaTime);
            transform.position += Vector3.Lerp(transform.position, dash, 5);
            is_dash = true;
            DontHitTime(1);
        }
        if (is_dash == true)
        {
            timer -= Time.deltaTime;
            Debug.Log(timer);
            if (timer <= 0)
            {
                timer = 5;
                is_dash = false;
                Debug.Log("대쉬 초기화");
            }
        }
    }

    void SpawnPointUpdate()
    {
        spawn_point = GameManager.Instance.spawnPoint;
        if (GameManager.Instance.spawnPoint == null)
        {
            spawn_point = firstSpawnPoint[spawnNum];
        }
    }

    public void ItemChange()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && !is_usehand)
        {
            current_item = 0;
            if (current_item == 0)
            {
                ani.SetTrigger("Change");
                ani.SetTrigger(weapon_String);
                inventory[0].SetActive(true);//주무기
                inventory[1].SetActive(false);//아이템1
                inventory[2].SetActive(false);//아이템2
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && !is_usehand)
        {
            current_item = 1;
            if (current_item == 1)
            {
                ani.SetTrigger("Change");
                ani.SetTrigger("Item");
                inventory[0].SetActive(false);
                inventory[1].SetActive(true);
                inventory[2].SetActive(false);
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) && !is_usehand)
        {
            current_item = 2;
            if (current_item == 2)
            {
                ani.SetTrigger("Change");
                ani.SetTrigger("Item");
                inventory[0].SetActive(false);
                inventory[1].SetActive(false);
                inventory[2].SetActive(true);
            }
        }
        PickUpAndDropItem();
    }

    void PickUpAndDropItem()
    {
        if (is_usehand && Input.GetKeyDown(KeyCode.E))
        {
            hand.transform.DetachChildren();
        }
    }

    public void Skil()
    {
        switch (skil_num)
        {
            case 0:
                eps = EPlayer_Skil.EAdd_Ammo;
                break;
            case 1:
                eps = EPlayer_Skil.ETurbo_Pump;
                break;
            case 2:
                eps = EPlayer_Skil.EAdrenaline;
                break;
            case 3:
                eps = EPlayer_Skil.EAdd_AttackPoint;
                break;
            case 4:
                eps = EPlayer_Skil.EAdd_Vision;
                break;
            case 5:
                eps = EPlayer_Skil.EAdd_DashForce;
                break;
        }
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.layer == 12 && Input.GetKey(KeyCode.E))
        {
            Debug.Log("집어! 이것을!" + col.gameObject.layer);
            is_usehand = true;
            col.transform.parent = hand.transform;
            if (is_usehand && Input.GetKeyDown(KeyCode.E))
            {
                hand.transform.DetachChildren();
            }
        }
    }

    public enum EPlayer_Skil
    {
        EAdd_Ammo,
        ETurbo_Pump,
        EAdrenaline,
        EAdd_AttackPoint,
        EAdd_Vision,
        EAdd_DashForce
    }

    void Point_Color()
    {
        InGameUI.instace.GM_Color();
        GameManager.Instance.Player_List_Set();
        player_actornum = PhotonNetwork.LocalPlayer.ActorNumber;
        GameManager.Instance.player_list.Add(this);

        for (int j = 0; j < PhotonNetwork.PlayerList.Length; j++)
        {
            //if (PhotonNetwork.PlayerList[j].ActorNumber == 1007 && pv.IsMine)
            //{
            //    //r = GameManager.Instance.player_color[PhotonNetwork.LocalPlayer.ActorNumber].r;
            //    //g = GameManager.Instance.player_color[PhotonNetwork.LocalPlayer.ActorNumber].g;
            //    //b = GameManager.Instance.player_color[PhotonNetwork.LocalPlayer.ActorNumber].b;
            //    //a = GameManager.Instance.player_color[PhotonNetwork.LocalPlayer.ActorNumber].a;

            //    playerIndicator.color = GameManager.Instance.player_color[PhotonNetwork.LocalPlayer.ActorNumber];

            //}
            //else
            //{
            //    if(PhotonNetwork.PlayerList[j].ActorNumber == 2001)
            //    {
            //        playerIndicator.color = GameManager.Instance.player_color[1];
            //    }
            //    if(PhotonNetwork.PlayerList[j].ActorNumber == 3001)
            //    {
            //        playerIndicator.color = GameManager.Instance.player_color[2];
            //    }
            //    if (PhotonNetwork.PlayerList[j].ActorNumber == 4001)
            //    {
            //        playerIndicator.color = GameManager.Instance.player_color[4];
            //    }
            //}
            if (GameManager.Instance.player_list[j].pv.ViewID == 1001)
            {
                playerIndicator.color = GameManager.Instance.player_color[0];
            }
            if (GameManager.Instance.player_list[j].pv.ViewID == 2001)
            {
                playerIndicator.color = GameManager.Instance.player_color[1];
            }
            if (GameManager.Instance.player_list[j].pv.ViewID == 3001)
            {
                playerIndicator.color = GameManager.Instance.player_color[2];
            }
            if (GameManager.Instance.player_list[j].pv.ViewID == 4001)
            {
                playerIndicator.color = GameManager.Instance.player_color[4];
            }

        }

        //playerIndicator.color = GameManager.Instance.player_color[PhotonNetwork.LocalPlayer.ActorNumber];

        //InGameUI.instace.UI_Setting(PhotonNetwork.LocalPlayer.ActorNumber);
    }
}
