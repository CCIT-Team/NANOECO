using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerMovement : Character
{
    public GameManager gm;
    public Rigidbody rigid;
    public Animator ani;
    public float dash_force;
    bool isdash = false;
    public float jump_force;
    bool isjump = false;
    public PlayerMouseRotate pmr;
    public GameObject[] item;
    // public Camera maincamera;

    void Start()
    {
        is_dead = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (is_dead == false)
        {
            Movement();
        }
        Dead();
        if (Input.GetKeyDown(KeyCode.P))
        {
            current_hp -= 10;
        }
    }

    void Movement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        if (horizontal > 0 || horizontal < 0 || vertical > 0 || vertical < 0) { ani.SetBool("Run", true); }
        else { ani.SetBool("Run", false); }
        Vector3 move = new Vector3(-horizontal * move_speed, 0f, -vertical * move_speed);
        rigid.velocity = move; Jump(); Dash();
    }
    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rigid.AddForce(Vector3.up * jump_force, ForceMode.Impulse);
            isjump = true;
        }
        else { isjump = false; rigid.}
    }
    void Dash()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            rigid.AddForce(Vector3.forward * dash_force);
            isdash = true;
        }
        else { isdash = false; }
    }
    void Dead()
    {
        if (current_hp <= 0)
        {
            is_dead = true;
            pmr.enabled = false;
            //죽는 애니메이션 추가
            Destroy(gameObject, 1f);//애니메이션 실행후 삭제
            Invoke("Respawn", 5);
        }
        else { pmr.enabled = true; }
    }
    void Respawn()
    {
        //부활 애니메이션 추가
        //transform.position = //스폰 위치

    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Monster") { current_hp -= col.gameObject.GetComponent<Character>().damage; }
    }
}

public interface IPlayer
{
    void Connect()
    {
        Debug.Log("Connect");
        SceneManager.LoadScene(1);//플레이어들이 연결되어 시작 버튼을 누르면 게임 씬으로 이동
    }
    void Disconnect()
    {
        Debug.Log("DisConnect");
        SceneManager.LoadScene(0);//연결이 끊기면 대기실로 돌아감
    }
}

