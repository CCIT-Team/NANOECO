using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;
using Photon.Realtime;

public class PhotonTestPlayer : MonoBehaviourPunCallbacks, IPunObservable
{
    public Rigidbody rigid;
    public Animator ani;
    public PhotonView pv;
    public TextMeshProUGUI nickname;
    public CharacterController cc;

    public float move_force;
    public float dash_force;
    public float jump_force;
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if(stream.IsWriting)
        {
            stream.SendNext(transform.position);
        }
        else
        {

        }
    }

    void Awake()
    {
        nickname.text = pv.IsMine ? PhotonNetwork.NickName : pv.Owner.NickName;
        nickname.color = pv.IsMine ? Color.green : Color.red;

        this.enabled = true;
    }

    void Start()
    {
        enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        enabled = true;
        if(pv.IsMine){Move();}
        if(Input.GetKeyDown(KeyCode.Alpha1)){Application.Quit();}
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


}
