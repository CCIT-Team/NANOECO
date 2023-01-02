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
    public Transform spawn_point;
    int current_item = 0;
    bool isdash = false;
    public bool isGrounded = true;
    public GameObject hand;//아이템 줍기용
    [Header("애니메이션 관련")]
    public Animator ani;
    public Animator helicopterAni;
    [Header("죽음 애니메이션")]
    public GameObject helicopter;
    public GameObject helicopterrope;
    public GameObject helicopterplayerbody;
    public GameObject originPlayer;


    Vector3 curPos;
    Quaternion curRot;
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
        }
        else
        {
            curPos = (Vector3)stream.ReceiveNext();
            curRot = (Quaternion)stream.ReceiveNext();
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
        spawn_point = GameObject.FindGameObjectWithTag("Spawn").transform;
    }

    void Start()
    {
        Skil();
        is_dead = false;
        item[0].SetActive(true);
        item[1].SetActive(false);
        item[2].SetActive(false);
        pv.RPC("ActiveRPC", RpcTarget.AllBuffered, current_item);
        helicopter.SetActive(false);
    }

    void Update()
    {
        if (pv.IsMine && PhotonNetwork.IsConnected && !is_dead) { Move(); }
        if (Input.GetKeyDown(KeyCode.Escape)) { Application.Quit(); }
        //pv.RPC("DestroyRPC", RpcTarget.AllBuffered);
        ItemChange();
        Dead();
        if (Input.GetKeyDown(KeyCode.G))
        {
            current_hp = -100;
        }
    }

    void Dead()
    {
        if (current_hp <= 0)
        {
            is_dead = true;
            helicopter.SetActive(true);
            helicopterplayerbody.transform.parent = helicopterrope.transform;
            helicopterplayerbody.transform.localPosition = new Vector3(0, 0, 0);
            if (helicopterAni.GetBool("Respawn"))
            {
                ReSpawn();
            }
        }
    }

    void ReSpawn()
    {
        if (is_dead)
        {
            respawn_time -= Time.deltaTime;
            if (respawn_time <= 0)
            {
                is_dead = false;
                helicopterAni.SetBool("Respawn", true);
                if (helicopterAni.GetBool("HliEnd"))
                {
                    transform.position = spawn_point.position;
                    helicopterplayerbody.transform.parent = originPlayer.transform;
                    helicopterplayerbody.transform.localPosition = new Vector3(0, 0, 0);
                    helicopter.SetActive(false);
                    current_hp = max_hp;
                    respawn_time = 3;
                }
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

    void Dash()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && isGrounded == true)//대쉬
        {
            Debug.Log("Dash!!!!!!!!");
            rigid.AddForce(Vector3.forward * dash_force, ForceMode.Acceleration);
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
    public void ItemChange()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            item[0].SetActive(true);//주무기
            item[1].SetActive(false);//아이템1
            item[2].SetActive(false);//아이템2
            current_item = 0;
            pv.RPC("ActiveRPC", RpcTarget.AllBuffered, current_item);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            item[0].SetActive(false);
            item[1].SetActive(true);
            item[2].SetActive(false);
            current_item = 1;
            pv.RPC("ActiveRPC", RpcTarget.AllBuffered, current_item);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            item[0].SetActive(false);
            item[1].SetActive(false);
            item[2].SetActive(true);
            current_item = 2;
            pv.RPC("ActiveRPC", RpcTarget.AllBuffered, current_item);
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

    //private void OnCollisionEnter(Collision col)
    //{
    //    if (col.gameObject.layer == 8) { current_hp -= col.gameObject.GetComponent<Character>().damage; }
    //}

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
