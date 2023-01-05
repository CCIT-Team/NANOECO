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

    //public Camera cam;
    int targetdisplay = 0;
    public Rigidbody rigid;
    public PhotonView pv;
    public TextMeshProUGUI nickname;
    EPlayer_Skil eps;
    public int camera_shaking_num;
    [Header("Status")]
    public float max_hp;
    public float current_hp;
    public float jump_force;
    public float dash_force;
    public float move_force;
    public bool is_dead;
    public float respawn_time = 5;
    public int skil_num;
    public GameObject[] item;
    int current_item = 0;
    bool isdash = false;
    public bool isGrounded = true;
    bool isdontHit = false;
    [Header("아이템 줍기")]
    public GameObject hand;
    bool isusehand = false;
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
        }
        else
        {
            curPos = (Vector3)stream.ReceiveNext();
            curRot = (Quaternion)stream.ReceiveNext();
            current_hp = (float)stream.ReceiveNext();
            is_dead = (bool)stream.ReceiveNext();
            current_item = (int)stream.ReceiveNext();
        }
    }

    void Awake()
    {
        nickname.text = pv.IsMine ? PhotonNetwork.NickName : pv.Owner.NickName;
        nickname.color = pv.IsMine ? Color.green : Color.red;
        if (pv.IsMine)
        {
            gameObject.name = nickname.text;
            //cam.gameObject.name = nickname.text + "cam";
            //cam = GameObject.Find(nickname.text + "cam").GetComponent<Camera>();
            Camera.main.GetComponent<PlayerCamera>().player = gameObject.transform;
        }
        instance = this;
        firstSpawnPoint[0] = GameObject.FindGameObjectWithTag("FirstSpawnPoint").transform.Find("1").transform;
        firstSpawnPoint[1] = GameObject.FindGameObjectWithTag("FirstSpawnPoint").transform.Find("2").transform;
        firstSpawnPoint[2] = GameObject.FindGameObjectWithTag("FirstSpawnPoint").transform.Find("3").transform;
        firstSpawnPoint[3] = GameObject.FindGameObjectWithTag("FirstSpawnPoint").transform.Find("4").transform;
    }

    void Start()
    {
        PhotonNetwork.CurrentRoom.IsOpen = false;

        Skil();
        is_dead = false;
        item[0].SetActive(true);
        item[1].SetActive(false);
        item[2].SetActive(false);
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
        if (Input.GetKeyDown(KeyCode.Escape)) { Application.Quit(); }
        //pv.RPC("DestroyRPC", RpcTarget.AllBuffered);
        ItemChange();
        SpawnPointUpdate();
        Dead();
        if (Input.GetKeyDown(KeyCode.G))
        {
            current_hp = -100;
        }
        if (helicopterAni.GetBool("Respawn"))
        {
            ReSpawn();
        }
    }

    void Dead()
    {
        if (current_hp <= 0 || is_dead == true)
        {
            current_hp = 0;
            is_dead = true;
            helicopter.SetActive(true);
            helicopterplayerbody.transform.parent = helicopterrope.transform;
            // helicopterplayerbody.transform.localPosition = new Vector3(0, 0, 0);
        }

        if (spawn_point == null)
        {
            spawn_point = firstSpawnPoint[spawnNum];
        }

        if (spawnNum > 3)
        {
            spawnNum = 0;
        }
    }

    void ReSpawn()
    {
        if (is_dead)
        {
            respawn_time -= Time.deltaTime;
            if (respawn_time <= 0)
            {
                transform.position = spawn_point.position;
                if (isunrideheli == true)
                {
                    Debug.Log("내려 내려 내려");
                    helicopterplayerbody.SetActive(true);
                    helicopterrope.transform.DetachChildren();
                    helicopterplayerbody.transform.parent = originPlayer.transform;
                    current_hp = max_hp;
                    respawn_time = 3;
                    DontHitTime(3);
                    isunrideheli = false;
                    is_dead = false;
                }
            }
        }
    }

    void DontHitTime(float time)
    {
        isdontHit = true;
        if(isdontHit)
        {
            current_hp = max_hp;
            time -= Time.deltaTime;
            if (timer <= 0)
            {
                isdontHit = false;
            }
        }
    }

    void Move()
    {
        Debug.Log(isGrounded);
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
        Jump(); Dash();
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
        if (Input.GetKeyDown(KeyCode.LeftShift) && isGrounded == true && isdash == false)//대쉬
        {
            Debug.Log("대쉬");
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            Vector3 dash = new Vector3(-horizontal * dash_force * Time.deltaTime, 0, -vertical * dash_force * Time.deltaTime);
            transform.position += Vector3.Lerp(transform.position,dash,5);
            isdash = true;
            DontHitTime(1);
        }       
        if(isdash == true)
        {
            timer -= Time.deltaTime;
            Debug.Log(timer);
            if (timer <= 0)
            {
                timer = 5;
                isdash = false;
                Debug.Log("대쉬 초기화");
            }
        }
    }

    public void AttackAnimation()
    {
        if (current_item == 0 && item[0].name == "Melee1" && Input.GetMouseButtonDown(0))
        {
            ani.SetBool("Close Attack", true);
        }
        else { ani.SetBool("Close Attack", false); }

        if (current_item == 1 && Input.GetMouseButtonDown(0))
        {
            ani.SetBool("Bomb", true);
        }
        else { ani.SetBool("Bomb", false); }
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
        if (Input.GetKeyDown(KeyCode.Alpha1) && !isusehand)
        {
            current_item = 0;
            if (current_item == 0)
            {
                item[0].SetActive(true);//주무기
                item[1].SetActive(false);//아이템1
                item[2].SetActive(false);//아이템2
            }           
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && !isusehand)
        {
            current_item = 1;
            if (current_item == 1)
            {
                item[0].SetActive(false);
                item[1].SetActive(true);
                item[2].SetActive(false);
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) && !isusehand)
        {
            current_item = 2;
            if (current_item == 2)
            {
                item[0].SetActive(false);
                item[1].SetActive(false);
                item[2].SetActive(true);
            }
        }
        PickUpAndDropItem();
    }

    void PickUpAndDropItem()
    {
        if (isusehand && Input.GetKeyDown(KeyCode.E))
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
        if (col.gameObject.layer == 11 && Input.GetKeyDown(KeyCode.E))
        {
            isusehand = true;
            col.transform.parent = hand.transform;
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

    [PunRPC]
    void ActiveRPC(int a)
    {
        item[0].SetActive(false);
        item[1].SetActive(false);
        item[2].SetActive(false);
        item[a].SetActive(true);
    }
}
