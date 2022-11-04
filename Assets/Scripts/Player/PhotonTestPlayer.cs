using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;
using Photon.Realtime;
using Photon.Pun.Demo.PunBasics;

public class PhotonTestPlayer : MonoBehaviourPunCallbacks, IPunObservable
{
    public static PhotonTestPlayer instance;


    public Camera cam;
    int targetdisplay = 0;
    public Rigidbody rigid;
    public Animator ani;
    public PhotonView pv;
    public TextMeshProUGUI nickname;
    public CharacterController cc;

    Vector3 curPos;
    [Header("Status")]
    [SerializeField] private float max_hp;
    [SerializeField] private float current_hp;
    [SerializeField] private float damage;
    [SerializeField] private float defense;
    [SerializeField] private float jump_force;
    [SerializeField] private float dash_force;
    [SerializeField] private float move_force;
    [SerializeField] private bool _is_dead;

    public GameObject[] item;
    int current_item = 0;
    bool isdash = false;
    
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if(stream.IsWriting)
        {
            stream.SendNext(transform.position);
        }
        else
        {
            curPos = (Vector3)stream.ReceiveNext();
        }
    }

    void Awake()
    {
        nickname.text = pv.IsMine ? PhotonNetwork.NickName : pv.Owner.NickName;
        nickname.color = pv.IsMine ? Color.green : Color.red;
        cam.gameObject.name = nickname.text;

        if (pv.IsMine)
        {
            cam = GameObject.Find(nickname.text).GetComponent<Camera>();
            cam.targetDisplay = targetdisplay++;
        }
        instance = this;
    }

    void Start()
    {
        _is_dead = false;
        item[0].SetActive(true);
        item[1].SetActive(false);
        item[2].SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //enabled = true;
        if(pv.IsMine && PhotonNetwork.IsConnected){Move();}
        if(Input.GetKeyDown(KeyCode.Escape)){Application.Quit();}
        pv.RPC("DestroyRPC", RpcTarget.AllBuffered);
    }

    void Move()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 move = new Vector3(-Input.GetAxis("Horizontal"), 0, -Input.GetAxis("Vertical"));
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
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            item[0].SetActive(false);
            item[1].SetActive(true);
            item[2].SetActive(false);
            current_item = 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            item[0].SetActive(false);
            item[1].SetActive(false);
            item[2].SetActive(true);
            current_item = 2;
        }
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.layer == 8) { current_hp -= col.gameObject.GetComponent<Character>().damage; }  
    }

}
