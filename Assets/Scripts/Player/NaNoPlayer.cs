using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;
using Photon.Realtime;

public class NaNoPlayer : MonoBehaviourPunCallbacks, IPunObservable
{
    public static NaNoPlayer instance;

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
    bool is_dash = false;
    public bool isGrounded = true;
    bool is_dontHit = false;
    [Header("������ �ݱ�")]
    public GameObject hand;
    bool is_usehand = false;
    [Header("��������Ʈ")]
    public Transform[] firstSpawnPoint = new Transform[4];
    int spawnNum = 0;
    public Transform spawn_point;
    [Header("�ִϸ��̼� ����")]
    public Animator ani;
    public Animator helicopterAni;
    [Header("���� �ִϸ��̼�")]
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
            try
            {
                curPos = (Vector3)stream.ReceiveNext();
                curRot = (Quaternion)stream.ReceiveNext();
                current_hp = (float)stream.ReceiveNext();
                is_dead = (bool)stream.ReceiveNext();
                current_item = (int)stream.ReceiveNext();
            }
            catch
            {

            }
        }
    }

    void Awake()
    {
        nickname.text = pv.IsMine ? PhotonNetwork.NickName : pv.Owner.NickName;
        //if(pv.IsMine)
        //{
        //    if(GameManager.Instance.players[GameManager.Instance.playersnum] == null)
        //    {
        //        GameManager.Instance.players[GameManager.Instance.playersnum] = this;
        //    }
        //    else { GameManager.Instance.playersnum += 1; }
        //}
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
        if (pv.IsMine) { ItemChange(); }
        if (Input.GetKeyDown(KeyCode.Escape)) { Application.Quit(); }
        SpawnPointUpdate();
        Dead();
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
                //isunrideheli = true;
            }
            if (isunrideheli == true)
            {
                Debug.Log("���� ���� ����");
                GameManager.Instance.player_count += 1;
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
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded == true)//����
        {
            isGrounded = false;
            rigid.AddForce(Vector3.up * jump_force, ForceMode.Impulse);
        }
    }
    float timer = 5;
    void Dash()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && isGrounded == true && is_dash == false)//�뽬
        {
            Debug.Log("�뽬");
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
                Debug.Log("�뽬 �ʱ�ȭ");
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
        if (Input.GetKeyDown(KeyCode.Alpha1) && !is_usehand)
        {
            current_item = 0;
            if (current_item == 0)
            {
                item[0].SetActive(true);//�ֹ���
                item[1].SetActive(false);//������1
                item[2].SetActive(false);//������2
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && !is_usehand)
        {
            current_item = 1;
            if (current_item == 1)
            {
                item[0].SetActive(false);
                item[1].SetActive(true);
                item[2].SetActive(false);
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) && !is_usehand)
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
        if (col.gameObject.layer == 11 && Input.GetKeyDown(KeyCode.E))
        {
            is_usehand = true;
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
}
