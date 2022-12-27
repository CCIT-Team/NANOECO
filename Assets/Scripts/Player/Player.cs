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
    public Animator ani;
    public PhotonView pv;
    public TextMeshProUGUI nickname;
    public CharacterController cc;
    EPlayer_Skil eps;
    public int camera_shaking_num;
    [Header("Status")]
    public float max_hp;
    public float current_hp;
    public float damage;
    public float defense;
    public float jump_force;
    public float dash_force;
    public float move_force;
    public bool _is_dead;
    public int skil_num;
    public GameObject[] item;
    int current_item = 0;
    bool isdash = false;


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
        if(pv.IsMine)
        {
            gameObject.name = nickname.text;
            //cam.gameObject.name = nickname.text + "cam";
            //cam = GameObject.Find(nickname.text + "cam").GetComponent<Camera>();
            Camera.main.GetComponent<PlayerCamera>().player = gameObject.transform;
        }
        instance = this;
    }

    void Start()
    {
        Skil();
        _is_dead = false;
        item[0].SetActive(true);
        item[1].SetActive(false);
        item[2].SetActive(false);
        pv.RPC("ActiveRPC", RpcTarget.AllBuffered, current_item);
    }

    void Update()
    {
        if (pv.IsMine && PhotonNetwork.IsConnected) { Move(); }
        if (Input.GetKeyDown(KeyCode.Escape)) { Application.Quit(); }
        //pv.RPC("DestroyRPC", RpcTarget.AllBuffered);
        ItemChange();
    }

    void Move()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 move = new(-Input.GetAxis("Horizontal"), 0, -Input.GetAxis("Vertical"));
        move *= move_force;
        if (!cc.isGrounded) { move.y -= 9.81f * Time.deltaTime; }
        if (horizontal > 0 || horizontal < 0 || vertical > 0 || vertical < 0)
        {
            ani.SetBool("Run", true);
        }
        else
        {
            ani.SetBool("Run", false);
        }

        if (Input.GetKeyDown(KeyCode.Space))//점프
        {
            //move. y = jump_force;
            rigid.AddForce(Vector3.up * jump_force, ForceMode.Impulse);
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))//대쉬
        {
            move = dash_force * move; ;
        }
        else { move = move_force * move; }
        cc.Move(move);
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
        switch(skil_num)
        {
            case 0:
                eps =  EPlayer_Skil.EAdd_Ammo;
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
        if (col.gameObject.layer == 8) { current_hp -= col.gameObject.GetComponent<Character>().damage; }
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
