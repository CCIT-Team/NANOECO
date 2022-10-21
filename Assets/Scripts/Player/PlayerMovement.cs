using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerMovement : Character
{
    public PlayerMouseRotate pmr;
    public GameObject[] item;
    public Rigidbody rigid;
    public Animator ani;
    public CharacterController cc; 

    public float jump_force;
    public float dash_force;
    public float move_force;

    bool isdash = false;
    bool isjump = false;
    

    void Start()
    {
        is_dead = false;
    }

    // Update is called once per frame
    void Update()
    {
            Movement();
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

        Vector3 move = new Vector3(-Input.GetAxis("Horizontal"),0, -Input.GetAxis("Vertical"));
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
        //rigid.velocity = move * move_speed * Time.deltaTime; 

        if (Input.GetKeyDown(KeyCode.Space))//����
        { 
            move. y = jump_force;
            isjump = true;
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))//�뽬
        {
            move = Vector3.forward * dash_force;
            isdash = true;
        }
        else { isdash = false; }
        cc.Move(move);
    }
    
    void Dead()
    {
        if (current_hp <= 0)
        {
            is_dead = true;
            pmr.enabled = false;
            //�״� �ִϸ��̼� �߰�
            Destroy(gameObject, 1f);//�ִϸ��̼� ������ ����
            Invoke("Respawn", 5);
        }
        else { pmr.enabled = true; }
    }
    void Respawn()
    {
        //��Ȱ �ִϸ��̼� �߰�
        //transform.position = //���� ��ġ

    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.layer == 8) { current_hp -= col.gameObject.GetComponent<Character>().damage; }
        if (col.gameObject.layer == 7) { isjump = false; }
    }
}


